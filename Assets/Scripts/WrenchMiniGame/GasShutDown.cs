using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GasShutDown : MonoBehaviour
{
    private const string MiniGameSceneName = "Gas_Value_MiniGame";
    
    
    private InteractWithObject _interact;
    private InventoryHelper _inventory;

    public UIElement theGUI;
    public GameObject countdown;
    
    private Item Wrench;
    
    private bool hasWrench;
    
    private GameObject canvi;
    private GameObject camera;
    public GameObject levelAudio;
    public GameObject gasAudio;
    public GameObject levelWin;
    public GameObject miniWin;

    private LogToServer logger;
    void Start()
    {
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        _interact = GetComponent<InteractWithObject>();
        _inventory = Systems.Inventory;
        
       Wrench =  Resources.Load<Item>("Items/Wrench");
       _inventory.CheckOnAdd.AddListener(UpdateHasWrench);
        
    }
    
    public void Interaction()
    {
        if (hasWrench)
        {
            logger.sendToLog("Started gas mini game","MINIGAME");
            countdown.GetComponent<CountdownBoom>().StopGasCount();
            SceneManager.LoadScene(MiniGameSceneName, LoadSceneMode.Additive);
            SceneManager.sceneLoaded += StartMinigame;
            _interact.enabled = false;
        }
        else
        {
            _interact.SetInteractText("You need to get the wrench!");
            Debug.Log("Opened Gas Minigame without wrench");
            logger.sendToLog("Opened gas minigame without a wrench","MINIGAME");
        }
    }
    //CAMERA, ui, move stage up a lot
    private void UpdateHasWrench() //called every time an item is added to the inventory 
    {
        hasWrench = _inventory.HasItem(Wrench, 1);
    } 

    private void StartMinigame(Scene scn, LoadSceneMode lsm)
    {
        Systems.Status.Pause();
        SceneManager.sceneLoaded -= StartMinigame;


        (canvi = GameObject.Find("MiniGameClose")).SetActive(false);
        (camera = GameObject.Find("Main Camera")).SetActive(false);
        levelAudio.SetActive(false);
        gasAudio.SetActive(true);

        GameObject.Find("GasMaster").GetComponent<WrenchMiniGameMaster>().OnWin += MiniGameFinished;
        GameObject.Find("GasMaster").GetComponent<WrenchMiniGameMaster>().OnExit += MiniGameLeave;
    }

    private void MiniGameLeave()
    {
        SceneManager.UnloadSceneAsync(MiniGameSceneName);
        canvi.SetActive(true);
        camera.SetActive(true);
        levelAudio.SetActive(true);
        gasAudio.SetActive(false);
    }
    private void MiniGameFinished()
    {
        Systems.Status.UnPause();

        SceneManager.UnloadSceneAsync(MiniGameSceneName);
        
        camera.SetActive(true);
        canvi.SetActive(true);
        levelAudio.SetActive(true);
        gasAudio.SetActive(false);

        UIManager.Instance.ToggleActive(theGUI);
        //_inventory.RemoveItem(Wrench, 1);
       
    }

    public void GasMiniGameWon()
    {
        logger.sendToLog("Won gas mini game!","MINIGAME");
        gasAudio.SetActive(false);
        miniWin.SetActive(true);
        levelWin.SetActive(true);
    }
}
