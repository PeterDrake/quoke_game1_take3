using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroToCorgiSaveMiniGame : MonoBehaviour
{
    public GameObject CompostingToilet;
    public GameObject InTheMeantimeCanvas;
    public GameObject MiniGameClose;
    public GameObject VideoDisplayer;
    public GameObject VideoBackground;
    public GameObject Video;
    public GameObject TsuPointer;

    public GameObject Ahmad;
    public GameObject Maria;
    public GameObject Bruce;
    public NavMeshAgent BruceNav;
    public GameObject BruceCont;

    public InformationCanvas _canvas;

    public AudioSource bark;

    private bool check;
    private StartDialogue script;
    public  NPCWalking scriptBruce;
    private NPCWalking scriptAhmad;
    public NPCWalking scriptMaria;
    private Animator animator1;
    private InteractWithObject _interact;

    
    

    void Start()
    {
        Debug.Log("starting");
        script = this.GetComponent<StartDialogue>();
        scriptAhmad = Ahmad.GetComponent<NPCWalking>();
        //scriptBruce = Bruce.GetComponent<NPCWalking>();
        //scriptMaria = Maria.GetComponent<NPCWalking>();
        //Bruce1 = Bruce.GetComponent<NavMeshAgent>();
        animator1 = Ahmad.GetComponent<Animator>();

        Debug.Log("SaveCorgiIntro script started");

        Video.GetComponent<VideoPlayer>().source = VideoSource.Url;
        string filepath = System.IO.Path.Combine(Application.streamingAssetsPath, "CorgiFINALEsmall.mp4");
        Video.GetComponent<VideoPlayer>().url = filepath;

        //Video.GetComponent<VideoPlayer>().Prepare();
    }


    //check if the sanitation is built
    void Update()
    {
        if (CompostingToilet.activeSelf && !check)
        {
            SceneManager.LoadSceneAsync("Scenes/Levels/CorgiCutScene");
            StartCoroutine(nameof(StartCutScene));
            check = true; //used this bool so the coroutine is triggered only once
        }

    }
    
    private IEnumerator StartCutScene()
    {
        Debug.Log("aa");
        //if the sanitation is built, wait for four seconds and trigger "In the meantime..." slide
        yield return new WaitForSeconds(1.5f);
        Debug.Log("a");
        MiniGameClose.SetActive(false);
        if (GameObject.Find("Music") != null) {
            GameObject.Find("Music").GetComponent<AudioSource>().Pause();
        }
        InTheMeantimeCanvas.SetActive(true);
        
        //then trigger the video
        yield return new WaitForSeconds(3f);
        Debug.Log("b");
        Systems.Status.Pause();
        
        VideoBackground.SetActive(true);
        VideoDisplayer.SetActive(true);
        Video.SetActive(true);
        Video.GetComponent<VideoPlayer>().Play();
        
        InTheMeantimeCanvas.SetActive(false);
        yield return new WaitForSeconds(62f); // Was 18 when it was just the first piece
        
        // string filepath = System.IO.Path.Combine(Application.streamingAssetsPath, "Sad2.mp4");
        // Video.GetComponent<VideoPlayer>().url = filepath;
        // Video.GetComponent<VideoPlayer>().Play();
        // yield return new WaitForSeconds(27f);
        //
        // filepath = System.IO.Path.Combine(Application.streamingAssetsPath, "Sad3.mp4");
        // Video.GetComponent<VideoPlayer>().url = filepath;
        // Video.GetComponent<VideoPlayer>().Play();
        // yield return new WaitForSeconds(17f);
        
        //yield return new WaitForSeconds(63f);
        

        //turn off the video
        
        Systems.Status.UnPause();
        VideoBackground.SetActive(false);
        VideoDisplayer.SetActive(false);
        Video.SetActive(false);
        if (GameObject.Find("Music") != null) { GameObject.Find("Music").GetComponent<AudioSource>().Play(); }
        MiniGameClose.SetActive(true);
        
        //change banner to "Look for Tsu"
        _canvas. ChangeText("Look for Tsu");
        
        yield return new WaitForSeconds(3f);
        
        //Tsu's dialogue appears
        script.Interact();
        bark.Play();
        
        //Tsu's dot appears
        // if (GameObject.Find("TsuPointer") != null) { GameObject.Find("TsuPointer").GetComponent<FlatFollow>().appear(); }

        GameObject.Find("TrePointer").GetComponent<FlatFollow>().appear();
        GameObject.Find("MoPointer").GetComponent<FlatFollow>().appear();

        //Ahmad starts walking to Tsu
        scriptAhmad.enabled = true;
        Ahmad.GetComponent<SphereCollider>().isTrigger = false;
        _interact = Ahmad.GetComponent<InteractWithObject>();
        Destroy(_interact);
        print("move the maira");

        ////Maria starts moving towards Tsu
        scriptMaria.enabled = true;
        if (GameObject.Find("MariaAlert").activeInHierarchy)
        {
            GameObject.Find("MariaAlert").SetActive(false);
        }
        Maria.GetComponent<SphereCollider>().enabled = false;
        ////Maria.GetComponent<InteractWithMaria>().Kill();
        Maria.GetComponent<InteractWithMaria>().enabled = false;

        ////Bruce starts walking to Tsu
        scriptBruce.enabled = true;
        BruceNav.enabled = true;
        if (GameObject.Find("BruceAlert").activeInHierarchy)
        {
            GameObject.Find("BruceAlert").SetActive(false);
        }
        BruceCont.GetComponent<SphereCollider>().enabled = false;
        BruceCont.GetComponent<InteractWithObject>().enabled = false;


    }
    
}
