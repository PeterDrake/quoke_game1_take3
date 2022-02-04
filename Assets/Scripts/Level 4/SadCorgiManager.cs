using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SadCorgiManager : MonoBehaviour
{
    public GameObject InTheMeantimeCanvas;
    public GameObject MiniGameClose;
    public GameObject VideoDisplayer;
    public GameObject VideoBackground;
    public GameObject Video;

    private bool check;
    private float timer;

    private void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(nameof(StartCutScene));
    }

    void Update()
    {
        if (check)
        {
            Destroy(this);
        }
    }

    private IEnumerator StartCutScene()
    {
        Time.timeScale = 1;
        Video.SetActive(true);
        // string filepath = "http://3.128.90.245/testbuild/StreamingAssets/CorgiSadScene.webm";
        string filepath = System.IO.Path.Combine(Application.streamingAssetsPath, "CorgiSadSmall.mp4");

        Video.GetComponent<VideoPlayer>().url = filepath;
        Video.GetComponent<VideoPlayer>().Prepare();
        //if the sanitation is built, wait for four seconds and trigger "In the meantime..." slide
        Debug.Log("Waiting for Button Click");
        //WaitUntil needs to cast a standard boolean to a System.Func<bool>, this is accomplished by ()=>BooleanHere
        // Found on https://answers.unity.com/questions/1265462/how-to-pass-predicate-to-a-coroutine.html
        yield return new WaitUntil(()=>!MiniGameClose.activeSelf);
        GameObject.Find("Black Background").SetActive(false);
        if (GameObject.Find("Music") != null)
        {
            GameObject.Find("Music").GetComponent<AudioSource>().Pause();
        }

        InTheMeantimeCanvas.SetActive(true);

        //then trigger the video
        Debug.Log("Waiting for 3 seconds");
        yield return new WaitForSeconds(3f);
        Systems.Status.Pause();

        VideoBackground.SetActive(true);
        VideoDisplayer.SetActive(true);
        Debug.Log("About to play the movie.");
        Video.GetComponent<VideoPlayer>().Play();

        InTheMeantimeCanvas.SetActive(false);
        Debug.Log("Waiting for Video 1");
        yield return new WaitForSeconds(62f);
        /*
        filepath = System.IO.Path.Combine(Application.streamingAssetsPath, "Sad2.mp4");
        Video.GetComponent<VideoPlayer>().url = filepath;
        Video.GetComponent<VideoPlayer>().Play();
        Debug.Log("Waiting for Video 2");
        yield return new WaitForSeconds(27f);

        
        filepath = System.IO.Path.Combine(Application.streamingAssetsPath, "Sad3.mp4");
        Video.GetComponent<VideoPlayer>().url = filepath;
        Video.GetComponent<VideoPlayer>().Play();
        Debug.Log("Waiting for Video 3");
        yield return new WaitForSeconds(13f);
        */
        SceneManager.LoadSceneAsync("Scenes/Levels/Level 5");
        yield return new WaitForSeconds(3f);
        //yield return new WaitForSeconds(63f);

        
        //turn off the video

        //Systems.Status.UnPause();
        //SceneManager.LoadSceneAsync("Scenes/Levels/Level 5");
        VideoBackground.SetActive(false);
        VideoDisplayer.SetActive(false);
        Video.SetActive(false);
        if (GameObject.Find("Music") != null)
        {
            GameObject.Find("Music").GetComponent<AudioSource>().Play();
        }

        MiniGameClose.SetActive(true);
        check = true;
    }
}
