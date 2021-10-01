using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LogToServer : Singleton<LogToServer>
{
    
    private String message = "Message for log";
    private String postURL = "/dbenter.php";

    public string loggername;
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        Debug.Log("Logger started: " + loggername);
    }

    // Update is called once per frame

    public void sendToLog(String message, String category)
    {
       
        StartCoroutine(PostRequest(message, category));
    }

    IEnumerator PostRequest(String message, String category)
    {
        //Debug.Log("Sending post request using logger: " + loggername);
        //Systems.Objectives.printObjectives();
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("message", message));
        wwwForm.Add(new MultipartFormDataSection("category", category));
        
        //Get time in game and add to post request
        int time = (int) Math.Round(Time.realtimeSinceStartup);
        String timestring = "" + time;
        wwwForm.Add(new MultipartFormDataSection("gametime", timestring));

        UnityWebRequest www = UnityWebRequest.Post(postURL, wwwForm);

        yield return www.SendWebRequest();

        if ((www.result == UnityWebRequest.Result.ConnectionError) || (www.result == UnityWebRequest.Result.ProtocolError))
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("Logged successfully");
        }
        
    }
    
}