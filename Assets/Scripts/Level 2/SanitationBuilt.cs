using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
 
public class SanitationBuilt : MonoBehaviour
{
    private const string MiniGameSceneName = "ToiletMiniGame";
    
    
    private InteractWithObject _interact;
    private InventoryHelper _inventory;
    private InformationCanvas _canvi;

    public UIElement theGUI;
    public GameObject Spot;

    public GameObject hint;
 
    private Item Bucket;
    private Item Bag;
    private Item Sawdust;
    private Item Sanitizer;
    private Item ToiletPaper;
    private Item Pamphlet;

    public GameObject Buckets;
 
    private bool HasSanitizer;
    private bool HasBuckets;
    private bool HasBag;
    private bool HasSawdust;

    private byte Conditions;
    
    
    private GameObject canvi;
    private GameObject camera;
    private GameObject vcam;
    private GameObject sunlight;
    public GameObject levelMusic;
    public GameObject toiletMusic;
    public GameObject miniWin;

    public GameObject eventSystem;
    
    private void Awake()
    {
        Buckets.SetActive(true);
    }

    void Start()
    {
        _interact = GetComponent<InteractWithObject>();
        _inventory = Systems.Inventory;
         
        Bucket =  Resources.Load<Item>("Items/Bucket");
        Bag =  Resources.Load<Item>("Items/Bag");
        Sawdust =  Resources.Load<Item>("Items/Sawdust");
        Sanitizer =  Resources.Load<Item>("Items/Sanitizer");
        ToiletPaper =  Resources.Load<Item>("Items/ToiletPaper");
        Pamphlet = Resources.Load<Item>("Items/SanitationPamphlet");
        _inventory.CheckOnAdd.AddListener(UpdateConditions);
        
        Buckets.SetActive(false);
    }
    
    public void Interaction()
    {
        if ((Conditions ^ 0x1F) == 0)
        {
            if (GameObject.Find("AhmadAlert") != null)
            { GameObject.Find("AhmadAlert").GetComponent<FlatFollow>().appear(); }
            hint.GetComponent<HintController>().NextTask();
            hint.GetComponent<HintController>().AllTaskCompleted(GameObject.Find("Post EQ"));
            eventSystem.SetActive(false);
            SceneManager.LoadScene(MiniGameSceneName, LoadSceneMode.Additive);
            SceneManager.sceneLoaded += StartMinigame;
            _interact.enabled = false;
        }
        else
        {
            _interact.SetInteractText("You need to gather more items");
        }
    }
    //CAMERA, ui, move stage up a lot
    private void UpdateConditions() //called every time an item is added to the inventory 
    {
        if ((Conditions ^ 0x1F) == 0) return;

        if ((Conditions & 0x1) > 0 || _inventory.HasItem(Bucket, 2)) //first condition not met
            Conditions |= 0x1;
        
        if ((Conditions & 0x2) > 0 || _inventory.HasItem(Bag, 1)) //second condition not met
            Conditions |= 0x2;
        
        if ((Conditions & 0x4) > 0 || _inventory.HasItem(Sawdust, 1)) //third condition not met
            Conditions |= 0x4;

        if ((Conditions & 0x8) > 0 || _inventory.HasItem(Sanitizer, 1)) //fourth condition not met
            Conditions |= 0x8;

        if ((Conditions & 0x10) > 0 || _inventory.HasItem(Pamphlet, 1)) // fifth condition not met
            Conditions |= 0x10;
    }


    private void StartMinigame(Scene scn, LoadSceneMode lsm)
    {
        Systems.Status.Pause();
        SceneManager.sceneLoaded -= StartMinigame;


        (canvi = GameObject.Find("MiniGameClose")).SetActive(false);
        (camera = GameObject.Find("Main Camera")).SetActive(false);
        (vcam = GameObject.Find("CM vcam1")).SetActive(false);
        // (sunlight = GameObject.Find("Sunlight")).SetActive(false);
        levelMusic.SetActive(false);
        toiletMusic.SetActive(true);

        GameObject.Find("MinigameMaster").GetComponent<MiniGameMaster>().OnWin += MiniGameFinished;
        GameObject.Find("MinigameMaster").GetComponent<MiniGameMaster>().OnExit += MiniGameLeave;
    }

    private void MiniGameLeave()
    {
        SceneManager.UnloadSceneAsync(MiniGameSceneName);
        canvi.SetActive(true);
        camera.SetActive(true);
        vcam.SetActive(true);
        // sunlight.SetActive(true);
        levelMusic.SetActive(true);
        toiletMusic.SetActive(false);
        _interact.enabled = true;
    }
    private void MiniGameFinished()
    {
        Systems.Status.UnPause();

        SceneManager.UnloadSceneAsync(MiniGameSceneName);
        
        Systems.Objectives.Satisfy("TOILETEVENT");
        camera.SetActive(true);
        vcam.SetActive(true);
        canvi.SetActive(true);
        // sunlight.SetActive(true);
        levelMusic.SetActive(true);
        toiletMusic.SetActive(false);

        _inventory.RemoveItem(Bucket, 2);
        _inventory.RemoveItem(Bag, 1);
        _inventory.RemoveItem( Sawdust, 1);
        // _inventory.RemoveItem( Sanitizer, 1);
        // _inventory.RemoveItem( ToiletPaper, 1);
        Debug.Log("Reactivating event system");
        eventSystem.SetActive(true);
        UIManager.Instance.ToggleActive(theGUI);

        _canvi = GameObject.Find("GUI").GetComponent<GuiDisplayer>().GetBanner();
        _canvi.ChangeText("Talk to Ahmad");

        Buckets.SetActive(true);
        Spot.SetActive(false);
        //Destroy(gameObject);
        //Destroy(this);

        Systems.Status.AffectRelief(100);
        GameObject.Find("MeterDing").GetComponent<AudioSource>().Play();

        Systems.Status.SpeedUpWarmthLoss();
    }

    public void MiniGameWon()
    {
        miniWin.SetActive(true);
    }
}
