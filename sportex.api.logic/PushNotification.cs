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

public class PushNotification
{
    public PushNotification()
    {

        //

        // TODO: Add constructor logic here

        //

    }

    public string SendPushNotification(string deviceId, string message)

    {

        string GoogleAppID = "AIzaSyAHEnHGKhkH4NulKLPs4PFZtJl_jgn5M0w"; //Enter google application id.

        var SENDER_ID = "AIzaSyAHEnHGKhkH4NulKLPs4PFZtJl_jgn5M0w"; //Enter Sender id.

        var value = message;

        WebRequest tRequest;

        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");

        tRequest.Method = "post";

        tRequest.ContentType = " application / x - www - form - urlencoded; charset = UTF - 8";

        tRequest.Headers.Add(string.Format("Authorization: key ={0}", GoogleAppID));

        tRequest.Headers.Add(string.Format("Sender: id ={0}", SENDER_ID));

        string postData = "collapse_key = score_update & time_to_live = 108 & delay_while_idle = 1 & data.message =" +value + "&data.time =" +System.DateTime.Now.ToString() + "®istration_id =" +deviceId + "";

        Console.WriteLine(postData);

        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        tRequest.ContentLength = byteArray.Length;
        Stream dataStream = tRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
        WebResponse tResponse = tRequest.GetResponse();
        dataStream = tResponse.GetResponseStream();
        StreamReader tReader = new StreamReader(dataStream);
        String sResponseFromServer = tReader.ReadToEnd();

        tReader.Close();

        dataStream.Close();

        tResponse.Close();

        return sResponseFromServer;

    }

}

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
