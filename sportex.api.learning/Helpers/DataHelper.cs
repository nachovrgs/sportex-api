using CsvHelper;
using GeoCoordinatePortable;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using sportex.api.domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportex.api.learning.Helpers
{
    public static class DataHelper
    {

        public static async Task<List<Data>> LoadData()
        {
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;

            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=blobsportex1;AccountKey=qNJ/KYpTmeY/uwHp7/jA7SQOJsC2R+VpG1oRe7ILOfV2jznvceWwu0HA/hTrWw7r2fez3gWnfM21JRV3fSqqog==;EndpointSuffix=core.windows.net";

            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    string filename = Guid.NewGuid().ToString() + ".csv";
                    cloudBlobContainer = cloudBlobClient.GetContainerReference("reviewrecords");

                    //Create CSV
                    using (Stream memoryStream = new MemoryStream())
                    {
                        StreamWriter streamWriter = new StreamWriter(memoryStream)
                        {
                            AutoFlush = true
                        };
                        // List the blobs in the container.
                        int howMany = 0;
                        BlobContinuationToken blobContinuationToken = null;
                        do
                        {
                            var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                            // Get the value of the continuation token returned by the listing call.
                            blobContinuationToken = results.ContinuationToken;
                            List<IListBlobItem> blobs = new List<IListBlobItem>();
                            foreach (IListBlobItem item in results.Results)
                            {
                                if (item.GetType() == typeof(CloudBlockBlob))
                                {
                                    howMany++;
                                    CloudBlockBlob blob = (CloudBlockBlob)item;
                                    await blob.DownloadRangeToStreamAsync(memoryStream, null, null);
                                    await streamWriter.WriteAsync(Environment.NewLine);
                                }

                            }
                        } while (blobContinuationToken != null && howMany <= 1000); // Loop while the continuation token is not null.

                        //All files are in stream
                        memoryStream.Position = 0;
                        using (var streamReader = new StreamReader(memoryStream))
                        {
                            var csvReader = new CsvReader(streamReader);
                            csvReader.Configuration.RegisterClassMap<DataMap>();
                            csvReader.Configuration.Delimiter = ",";

                            csvReader.Configuration.HasHeaderRecord = false;
                            csvReader.Configuration.IgnoreBlankLines = false;

                            var records = csvReader.GetRecords<Data>();
                            return records.ToList();
                        }
                    }
                }
                catch (StorageException ex)
                {
                    throw (ex);
                }
            }
            else
            {
                Console.WriteLine(
                    "The connection string is wrong");
                return null;
            }
        }

        public static async Task ProccessReview(PlayerReview review, double longitude, double latitude)
        {
            //Calculate average age
            if (review.EventReviewed != null && review.EventReviewed.EventParticipates != null && review.EventReviewed.EventParticipates.Count > 0 && review.EventReviewed.Location != null)
            {
                CloudStorageAccount storageAccount = null;
                CloudBlobContainer cloudBlobContainer = null;

                string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=blobsportex1;AccountKey=qNJ/KYpTmeY/uwHp7/jA7SQOJsC2R+VpG1oRe7ILOfV2jznvceWwu0HA/hTrWw7r2fez3gWnfM21JRV3fSqqog==;EndpointSuffix=core.windows.net";

                // Check whether the connection string can be parsed.
                if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
                {
                    try
                    {
                        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                        string filename = Guid.NewGuid().ToString() + ".csv";
                        CloudBlobContainer container = cloudBlobClient.GetContainerReference("reviewrecords");
                        await container.CreateIfNotExistsAsync();
                       
                        //Create CSV
                        using (var memoryStream = new MemoryStream())
                        using (var streamWriter = new StreamWriter(memoryStream))
                        using (var csvWriter = new CsvWriter(streamWriter))
                        {
                            streamWriter.AutoFlush = true;
                            csvWriter.Configuration.RegisterClassMap<DataMap>();
                            csvWriter.Configuration.IncludePrivateMembers = true;
                            Data record = new Data();
                            record.setEventId(review.EventReviewed.ID);
                            record.setAge(GetAverageAge(review.EventReviewed));
                            record.setDistance(GetDistance(review.EventReviewed, latitude, longitude));
                            //Calculate if liked or not. > 3 Likes, < 3 disliked
                            record.setResponse(review.Rate > 3 ? 1 : 0);
                            //csvWriter.WriteHeader<Data>();
                            csvWriter.WriteRecord<Data>(record);
                            CloudBlockBlob blob = container.GetBlockBlobReference(filename);
                            csvWriter.Flush();
                            memoryStream.Seek(0, SeekOrigin.Begin);
                            await blob.UploadFromStreamAsync(memoryStream);
                        }
                    }
                    catch (StorageException ex)
                    {
                        Console.WriteLine("Error returned from the service: {0}", ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine(
                        "The connection string is wrong");
                }

            }
        }

        public static int GetAverageAge(Event eve)
        {
            int avgAge = 0;
            if(eve.EventParticipates != null && eve.EventParticipates.Count > 0)
            {
                var today = DateTime.Today;
                foreach (EventParticipant participant in eve.EventParticipates)
                {
                    // Calculate the age.
                    var age = today.Year - participant.ProfileParticipant.DateOfBirth.Year;
                    // Go back to the year the person was born in case of a leap year
                    if (participant.ProfileParticipant.DateOfBirth > today.AddYears(-age)) age--;

                    avgAge = avgAge + age;
                }
                avgAge = avgAge / eve.EventParticipates.Count;
            }           
            return avgAge;
        }

        public static double GetDistance(Event eve, double latitude, double longitude)
        {
            if(eve.Location != null)
            {
                var userCoord = new GeoCoordinate(latitude, longitude);
                var eventCoord = new GeoCoordinate(eve.Location.Latitude.Value, eve.Location.Longitude.Value);

                return userCoord.GetDistanceTo(eventCoord);
            }
            return 0.0;
        }

        public static List<Data>[] Partition(List<Data> list, int totalPartitions = 2)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (totalPartitions < 1)
                throw new ArgumentOutOfRangeException("totalPartitions");

            List<Data>[] partitions = new List<Data>[totalPartitions];

            int maxSize = (int)Math.Ceiling(list.Count / (double)totalPartitions);
            int k = 0;

            for (int i = 0; i < partitions.Length; i++)
            {
                partitions[i] = new List<Data>();
                for (int j = k; j < k + maxSize; j++)
                {
                    if (j >= list.Count)
                        break;
                    partitions[i].Add(list[j]);
                }
                k += maxSize;
            }

            return partitions;
        }
    }
}
