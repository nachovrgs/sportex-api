using sportex.api.domain;
using sportex.api.persistence;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace sportex.api.logic
{
    public class StandardProfileManager
    {
        private IRepository<StandardProfile> repo;
        public StandardProfileManager()
        {
            repo = new Repository<StandardProfile>();
        }
        public List<StandardProfile> GetAllProfiles()
        {
            try
            {
                List<StandardProfile> profiles = new List<StandardProfile>();
                profiles = repo.GetAll();

                //IRepository<Account> repoAccount = new Repository<Account>();
                foreach (StandardProfile prof in profiles)
                {
                    //prof.Account = repoAccount.GetById(prof.AccountID);
                    AccountManager am = new AccountManager();
                    prof.Account = am.GetAccountById(prof.AccountID);
                }

                return profiles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public StandardProfile GetProfileById(int id)
        {
            try
            {
                StandardProfile profile = repo.GetById(id);
                //IRepository<Account> repoAccount = new Repository<Account>();
                //profile.Account = repoAccount.GetById(profile.AccountID);
                if (profile != null)
                {
                    AccountManager am = new AccountManager();
                    profile.Account = am.GetAccountById(profile.AccountID);
                }
                return profile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StandardProfile GetProfileByAccountId(int id)
        {
            try
            {
                StandardProfile profile = repo.SearchFor(prof => prof.AccountID == id, new string[] {
                    "Account"
                }).FirstOrDefault<StandardProfile>();
                return profile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StandardProfile GetProfileByUsername(string username)
        {
            try
            {
                StandardProfile profile = repo.SearchFor(prof => prof.Account.Username.ToUpper() == username.ToUpper(), new string[] {
                    "Account"
                }).FirstOrDefault<StandardProfile>();
                return profile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertProfile(StandardProfile profile)
        {
            try
            {
                profile.Status = 1;
                profile.CreatedOn = DateTime.Now;
                profile.LastUpdate = profile.CreatedOn;
                repo.Insert(profile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateProfile(StandardProfile profile)
        {
            try
            {
                if (profile != null)
                {
                    repo.Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateProfile(StandardProfile profileUpdated, StandardProfile newData)
        {
            try
            {
                if (profileUpdated != null && newData != null)
                {
                    profileUpdated.FirstName = newData.FirstName;
                    profileUpdated.LastName = newData.LastName;
                    profileUpdated.MailAddress = newData.MailAddress;
                    profileUpdated.PicturePath = newData.PicturePath;
                    profileUpdated.Sex = newData.Sex;
                    profileUpdated.Status = newData.Status;
                    profileUpdated.AccountID = newData.AccountID;
                    profileUpdated.LastUpdate = DateTime.Now;
                    repo.Update(profileUpdated);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteProfile(int id)
        {
            try
            {
                repo.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RateProfile(StandardProfile profile, int newRate)
        {
            try
            {
                if (profile != null)
                {
                    profile.CountReviews++;
                    profile.TotalRate += newRate;
                    UpdateProfile(profile);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ProcessImageAsync(StandardProfile profile, String image, int id)
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

                    string filename = id + Guid.NewGuid().ToString() + ".jpeg";
                    CloudBlobContainer container = cloudBlobClient.GetContainerReference("images");
                    await container.CreateIfNotExistsAsync();
                    byte[] imageBytes = Convert.FromBase64String(image);
                    CloudBlockBlob blob = container.GetBlockBlobReference(filename);
                    await blob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);
                    profile.PicturePath = blob.StorageUri.PrimaryUri.AbsoluteUri;
                    this.UpdateProfile(profile);

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
}
