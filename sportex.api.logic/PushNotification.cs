using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections.Specialized;
using PushSharp.Apple;
using Newtonsoft.Json.Linq;

public class PushNotification
{
    public PushNotification()
    {
    }

    public void SendPushNotification(string deviceToken,string message)
    {
        Exception excep = null;
        try
        {
            string status = "";

            //para obtener el certificado desde azure
            X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certCollection = certStore.Certificates.Find(
                                       X509FindType.FindByThumbprint,
                                 "84CE8142DBD5DA1B582A633E3E61385E7539B65B",
                                       false);
            X509Certificate2 cert = null;

            // Get the first cert with the thumbprint
            if (certCollection.Count > 0)
            {
                cert = certCollection[0];
            }
            certStore.Close();

            var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Production, cert, true);
            //


            //para obtener el certificado desde local
            //var appleCert = Path.Combine(Directory.GetCurrentDirectory(), @"Certificates\SportexPushProd.p12");
            // Configuration (NOTE: .pfx can also be used here)
            //var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Production, appleCert, "sportex1234");



            // Create a new broker
            var apnsBroker = new ApnsServiceBroker(config);

            // Wire up events
            apnsBroker.OnNotificationFailed += (notification, aggregateEx) =>
            {

                aggregateEx.Handle(ex =>
                {

                    // See what kind of exception it was to further diagnose
                    if (ex is ApnsNotificationException)
                    {
                        var notificationException = (ApnsNotificationException)ex;

                        // Deal with the failed notification
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;
                        string desc = $"Apple Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}";
                        Console.WriteLine(desc);
                        status = desc;
                        excep = ex;
                    }
                    else
                    {
                        string desc = $"Apple Notification Failed for some unknown reason : {ex.InnerException}";
                        // Inner exception might hold more useful information like an ApnsConnectionException			
                        Console.WriteLine(desc);
                        status = desc;
                    }

                    // Mark it as handled
                    return true;
                });
            };

            apnsBroker.OnNotificationSucceeded += (notification) =>
            {
                status = "Apple Notification Sent successfully!";
            };

            var fbs = new FeedbackService(config);
            fbs.FeedbackReceived += (string devicToken, DateTime timestamp) =>
            {
                // Remove the deviceToken from your database
                // timestamp is the time the token was reported as expired
                status = "Apple token has expired!";
            };

            // Start Proccess 
            apnsBroker.Start();

            if (deviceToken != "")
            {
                apnsBroker.QueueNotification(new ApnsNotification
                {
                    DeviceToken = deviceToken,
                    Payload = JObject.Parse(("{\"aps\":{\"sound\":\"default\",\"alert\":\"" + (message + "\"}}")))
                });
            }

            apnsBroker.Stop();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}


    //VIEJO
    //    public string SendPushNotification(string deviceId, string message)

    //    {

    //        string GoogleAppID = "AIzaSyAHEnHGKhkH4NulKLPs4PFZtJl_jgn5M0w"; //Enter google application id.

    //        var SENDER_ID = "AIzaSyAHEnHGKhkH4NulKLPs4PFZtJl_jgn5M0w"; //Enter Sender id.

    //        var value = message;

    //        WebRequest tRequest;

    //        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");

    //        tRequest.Method = "post";

    //        tRequest.ContentType = " application / x - www - form - urlencoded; charset = UTF - 8";

    //        tRequest.Headers.Add(string.Format("Authorization: key ={0}", GoogleAppID));

    //        tRequest.Headers.Add(string.Format("Sender: id ={0}", SENDER_ID));

    //        string postData = "collapse_key = score_update & time_to_live = 108 & delay_while_idle = 1 & data.message =" +value + "&data.time =" +System.DateTime.Now.ToString() + "®istration_id =" +deviceId + "";

    //        Console.WriteLine(postData);

    //        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);

    //        tRequest.ContentLength = byteArray.Length;
    //        Stream dataStream = tRequest.GetRequestStream();
    //        dataStream.Write(byteArray, 0, byteArray.Length);
    //        dataStream.Close();
    //        WebResponse tResponse = tRequest.GetResponse();
    //        dataStream = tResponse.GetResponseStream();
    //        StreamReader tReader = new StreamReader(dataStream);
    //        String sResponseFromServer = tReader.ReadToEnd();

    //        tReader.Close();

    //        dataStream.Close();

    //        tResponse.Close();

    //        return sResponseFromServer;

    //    }

    //}

    //Note:-Push notifications are sent through "https://android.googleapis.com/gcm/send" web request.
    //Call above "SendPushNotification" method from your business logic where the user needs a notification.Use below source code to call above method.

    //public class TestPushNotification

    //{

    //    public TestPushNotification()
    //    {

    //    }

    //    public string SentMessage()

    //    {

    //        PushNotification Obj = new PushNotification();
    //        string result = Obj.SendPushNotification("17BA0791499DB908433B80F37C5FBC89B870084B", "Hello Manoranjan");

    //        return result;

    //    }

    //}
