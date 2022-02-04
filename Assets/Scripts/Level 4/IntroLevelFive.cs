using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class IntroLevelFive : MonoBehaviour
{
    //public GameObject CompostingToilet;
    public GameObject TsuPointer;

    public GameObject Ahmad;
    public GameObject Maria;
    public GameObject Bruce;
    public GameObject BruceCont;
    public NavMeshAgent BruceNav;
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
        script = this.GetComponent<StartDialogue>();
        scriptAhmad = Ahmad.GetComponent<NPCWalking>();
        scriptBruce = Bruce.GetComponent<NPCWalking>();
        scriptMaria = Maria.GetComponent<NPCWalking>();
        animator1 = Ahmad.GetComponent<Animator>();

        //Video.GetComponent<VideoPlayer>().Prepare();
    }


    //check if the sanitation is built
    void Update()
    {

        if (!check)
        {
            StartCoroutine(nameof(StartCutScene));
            check = true; //used this bool so the coroutine is triggered only once
        }

    }
    
    // This method does not actually start the cutscene
    private IEnumerator StartCutScene()
    {
        //change banner to "Look for Tsu"
        _canvas.ChangeText("Look for Tsu");
        BruceCont.GetComponent<SphereCollider>().enabled = false;
        BruceCont.GetComponent<InteractWithObject>().enabled = false;
        Ahmad.GetComponent<SphereCollider>().enabled = false;
        _interact = Ahmad.GetComponent<InteractWithObject>();
        Destroy(_interact);
        Maria.GetComponent<SphereCollider>().enabled = false;
        Maria.GetComponent<InteractWithMaria>().enabled = false;
        yield return new WaitForSeconds(3f);
        
        //Tsu's dialogue appears
        script.Interact();
        bark.Play();
        
        //Tsu's dot appears
        // if (GameObject.Find("TsuPointer") != null) { GameObject.Find("TsuPointer").GetComponent<FlatFollow>().appear(); }

        GameObject.Find("TrePointer").GetComponent<FlatFollow>().appear();
        GameObject.Find("MoPointer").GetComponent<FlatFollow>().appear();


        ////Bruce starts walking to Tsu
        scriptBruce.enabled = true;
        BruceNav.enabled = true;/*
        if (GameObject.Find("BruceAlert").activeInHierarchy)
        {
            GameObject.Find("BruceAlert").SetActive(false);
        }*/
        
        
        //Ahmad starts walking to Tsu
        scriptAhmad.enabled = true;
        
        print("move the maria");

        ////Maria starts moving towards Tsu
        scriptMaria.enabled = true;
        /*if (GameObject.Find("MariaAlert").activeInHierarchy)
        {
            GameObject.Find("MariaAlert").SetActive(false);
        }*/
        
        //Maria.GetComponent<InteractWithMaria>().Kill();
        Maria.GetComponent<InteractWithMaria>().enabled = false;

        
        


    }
    
}
