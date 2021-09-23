using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Water;

public class PitLatrineVisit : MonoBehaviour
{
    private const string MiniGameSceneName = "PitLatrineMiniGame";

    public InformationCanvas _canvas;
    
    private InteractWithObject _interact;
    private InventoryHelper _inventory;

    public UIElement theGUI;
    public GameObject Spot;
    public GameObject hint;


    public float SituationNumber;

    public GameObject House;
    public GameObject Player;
    public GameObject Place1;
    public GameObject Place2;
    public GameObject WaterPond;
    public GameObject PitLatrine;
    private Item Shovel;
    private Item Rope;

    private GameObject S1;
    private GameObject S2;

    private GameObject canvi;
    private GameObject camera;
    private GameObject sunlight;
    public GameObject levelMusic;
    public GameObject latrineMusic;

    void Start()
    {
        _interact = GetComponent<InteractWithObject>();
        _inventory = Systems.Inventory;
        
        Shovel = Resources.Load<Item>("Items/Shovel");
        Rope = Resources.Load<Item>("Items/Rope");
    }

    public void Interaction()
    {
        if (_inventory.HasItem(Shovel, 1) && _inventory.HasItem(Rope, 1))
        {
            if (GameObject.Find("LatrineLowPointer") != null)
            { GameObject.Find("LatrineLowPointer").GetComponent<FlatFollow>().disappear(); }
            if (SituationNumber == 2) 
            {
                if (GameObject.Find("LatrineHighPointer") != null)
                { GameObject.Find("LatrineHighPointer").GetComponent<FlatFollow>().disappear(); }
                if (GameObject.Find("FrankAlert") != null)
                { GameObject.Find("FrankAlert").GetComponent<FlatFollow>().appear(); }
                if (GameObject.Find("ZeldaAlert") != null)
                { GameObject.Find("ZeldaAlert").GetComponent<FlatFollow>().appear(); }
                hint.GetComponent<HintController>().AllTaskCompleted(GameObject.Find("AfterLatrineTalk"));
                hint.GetComponent<HintController>().StartThisTask("Toilet");


            }
            SceneManager.LoadScene(MiniGameSceneName, LoadSceneMode.Additive);
            SceneManager.sceneLoaded += StartMinigame;
            _interact.enabled = false;
            House.SetActive(false);
            Player.SetActive(false);
        }
        else
        {
            _interact.SetInteractText("Go talk to Frank about sanitation, get a rope and a shovel");

        }

    }

    private void StartMinigame(Scene scn, LoadSceneMode lsm)
    {
        Systems.Status.Pause();
        SceneManager.sceneLoaded -= StartMinigame;


        (canvi = GameObject.Find("MiniGameClose")).SetActive(false);
        (camera = GameObject.Find("Main Camera")).SetActive(false);
        (sunlight = GameObject.Find("Sunlight")).SetActive(false);
        levelMusic.SetActive(false);
        latrineMusic.SetActive(true);

        S2 = GameObject.FindGameObjectWithTag("HighGround").gameObject;
        S1 = GameObject.FindGameObjectWithTag("LowGround").gameObject;

        if (SituationNumber == 1)
        {
            S1.SetActive(true);
            S2.SetActive(false);
        }

        else
        {
            S1.SetActive(false);
            S2.SetActive(true);
            Place2.SetActive(false);
        }
        
        
        
        GameObject.Find("MiniGameMasterPitLatrine").GetComponent<MiniGameMasterPitLatrine>().OnWin += MiniGameFinished;
        GameObject.Find("MiniGameMasterPitLatrine").GetComponent<MiniGameMasterPitLatrine>().OnExit += MiniGameLeave;
    }

    private void MiniGameLeave()
    {
        SceneManager.UnloadSceneAsync(MiniGameSceneName);
        canvi.SetActive(true);
        Place1.SetActive(false);
        camera.SetActive(true);
        sunlight.SetActive(true);
        levelMusic.SetActive(true);
        latrineMusic.SetActive(false);

        House.SetActive(true);
        WaterPond.SetActive(true);
        Player.SetActive(true);
        UIManager.Instance.ToggleActive(theGUI);
        _interact.Kill();
        Spot.SetActive(false);
        //Destroy(Place1);
    }
    private void MiniGameFinished()
    {
        //Place1.SetActive(false);
        
        PitLatrine.SetActive(true);
        House.SetActive(true);
        Player.SetActive(true);
        Systems.Status.UnPause();

        SceneManager.UnloadSceneAsync(MiniGameSceneName);
        

        Systems.Objectives.Satisfy("BuiltLatrine");
        camera.SetActive(true);
        canvi.SetActive(true);
        sunlight.SetActive(true);
        levelMusic.SetActive(true);
        latrineMusic.SetActive(false);

        _inventory.RemoveItem(Rope, 1);
        _inventory.RemoveItem(Shovel, 1);
        
        _interact.Kill();
        _canvas.ChangeText("Find shelter");

        Systems.Status.AffectRelief(100);
        GameObject.Find("MeterDing").GetComponent<AudioSource>().Play();

        Systems.Status.SpeedUpWarmthLoss();

        //Place2.SetActive(false);
        UIManager.Instance.ToggleActive(theGUI);
        Destroy(Place1);
        Destroy(Place2);
        Destroy(this);
    }
}
