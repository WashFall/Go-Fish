using UnityEngine;
using System.Net;
using System.IO;
using System;

public class JsonSTest
{
    public void Load()
    {
        //Remember to run the jsonSlave program in the background for this to work
        var request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/" + GameManager.Instance.activePlayer.Name);
        var response = (HttpWebResponse)request.GetResponse();

        // Open a stream to the server so we can read the response data
        // it sent back from our GET request
        using (var stream = response.GetResponseStream())
        {
            using (var reader = new StreamReader(stream))
            {
                // Read the entire body as a string
                var jsonData = reader.ReadToEnd();

                // Convert the JSON body into something useful
                var playerInfo = JsonUtility.FromJson<Player>(jsonData);

                // Create the player and move it to the location stored in the JSON body
                Debug.Log(playerInfo);
            }
        }
    }

    //Saves the playerInfo string on the server.
    public void Save(string fileName, string saveData)
    {
        //url
        var request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/" + fileName);
        request.ContentType = "application/json";
        request.Method = "PUT";

        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            streamWriter.Write(saveData);
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)request.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            Debug.Log(result);
        }
    }
}