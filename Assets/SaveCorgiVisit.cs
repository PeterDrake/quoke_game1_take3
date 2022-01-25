using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SaveCorgiVisit : MonoBehaviour
{
    //public InformationCanvas _canvas;
    //public string words;
    private const string MiniGameSceneName = "SaveCorgi";


    private InteractWithObject _interact;
    private InventoryHelper _inventory;

    //public UIElement theGUI;
    //public GameObject Spot;

    private GameObject canvi;
    private GameObject camera;
    private GameObject sunlight;
    public GameObject levelMusic;
    public GameObject miniMusic;
    public GameObject winMusic;
    private LogToServer logger;

    void Start()
    {
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        _interact = GetComponent<InteractWithObject>();
        _inventory = Systems.Inventory;
    }

    public void Interaction()
    {
        // SceneManager.LoadScene(MiniGameSceneName, LoadSceneMode.Additive);
        SceneManager.LoadScene(MiniGameSceneName);
        GameObject bruce = GameObject.Find("Bruce");
        GameObject maria = GameObject.Find("Maria");
        GameObject ahmad = GameObject.Find("Ahmad");
        bruce.SetActive(false);
        maria.SetActive(false);
        ahmad.SetActive(false);
        
        SceneManager.sceneLoaded += StartMinigame;
        _interact.enabled = false;
    }

    private void StartMinigame(Scene scn, LoadSceneMode lsm)
    {
        Systems.Status.Pause();
        SceneManager.sceneLoaded -= StartMinigame;
        
        (canvi = GameObject.Find("MiniGameClose")).SetActive(false);
        (camera = GameObject.Find("Main Camera")).SetActive(false);
        (sunlight = GameObject.Find("Sunlight")).SetActive(false);
        levelMusic.SetActive(false);
        miniMusic.SetActive(true);
    }

    public void CorgiRescue()
    {
        logger.sendToLog("Rescued Tsu!", "MINIGAME");
        winMusic.SetActive(true);

    }
}
