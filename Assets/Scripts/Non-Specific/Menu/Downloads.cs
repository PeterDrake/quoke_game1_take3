using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Downloads : Singleton<LogToServer>
{
    private String postURL = "/downloadRecord.php";

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void sendToLog(String docname)
    {
        StartCoroutine(PostRequest(docname));
    }

    IEnumerator PostRequest(String docname)
    {
        Debug.Log("Sending post request using logger: " + docname);
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("docname", docname));

       

        UnityWebRequest www = UnityWebRequest.Post(postURL, wwwForm);

        yield return www.SendWebRequest();

        if ((www.result == UnityWebRequest.Result.ConnectionError) || (www.result == UnityWebRequest.Result.ProtocolError))
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("Downloaded " + docname + " Successfully");
        }

    }
}
