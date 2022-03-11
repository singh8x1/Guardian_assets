using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using System.Net;
using System.IO;

using System.Text;

public class APIHandler
{
    /// <summary>
    /// to store result of the Get or Post methods
    /// </summary>
    public JSONNode result;
    /// <summary>
    /// to handle login credentials
    /// </summary>
    public string[] LoginCredentials;
    /// <summary>
    /// the url to make the get and post calls
    /// </summary>
    public string uri = "https://guardiantd.azurewebsites.net/api/User";

    /// <summary>
    /// Get the user's login credentials
    /// </summary>
    /// <param name="emailID"> email of the user </param>
    /// <param name="password">password of the user </param>
    /// <returns>returns the user's login credentials</returns>
    public string[] GetLoginCredentials(string emailID, string password)
    {
        LoginCredentials = new string[] { emailID, password };
        return LoginCredentials;
    }

    /// <summary>
    /// makes the get call from the db
    /// </summary>
    public void Get()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            result = JSON.Parse(reader.ReadToEnd());
        }
    }
    /// <summary>
    /// makes get call by using user's id
    /// </summary>
    /// <param name="userID">user's id to fetch their info</param>
    public void GetByID(string userID)
    {
        string newURL = uri + "/" + userID;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(newURL);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            result = JSON.Parse(reader.ReadToEnd());
        }
    }
    /// <summary>
    /// to make the post call to the db
    /// </summary>
    /// <param name="data">the content of the body for posting to db</param>
    /// <param name="contentType">the type of the body</param>
    /// <param name="method">Post method</param>
    public void Post(string data, string contentType, string method = "POST")
    {
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        request.ContentLength = dataBytes.Length;
        request.ContentType = contentType;
        request.Method = method;

        using (Stream requestBody = request.GetRequestStream())
        {
            requestBody.Write(dataBytes, 0, dataBytes.Length);
        }

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            result = JSON.Parse(reader.ReadToEnd());
        }
    }
}
