using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BarrelVisit : MonoBehaviour
{
    private const string MiniGameSceneName = "WaterBucketMiniGame";

    private InteractWithObject _interact;
    private InventoryHelper _inventory;

    public InformationCanvas _canvas;
    private string BewareTheStorm;
    public GameObject BarrelEnd;
    public GameObject DrainPipe;
    public GameObject Particles;
    public GameObject Storm;
    public Collider SchoolEntrance;

    public GameObject MiniGameMusic;
    public GameObject LevelMusic;

    private GameObject canvi;
    private GameObject camera;
    private GameObject vcam;
    private GameObject sunlight;
    //private GameObject levelAudio;


    void Start()
    {
        _interact = GetComponent<InteractWithObject>();
        BewareTheStorm = "A storm is approaching. Go to the shelter.";
    }

    public void Interaction()
    {
        if (GameObject.Find("BarrelPointer") != null)
        {
            GameObject.Find("BarrelPointer").GetComponent<FlatFollow>().disappear();
        }
       

        SceneManager.LoadScene(MiniGameSceneName, LoadSceneMode.Additive);
        Debug.Log("Hello");
        SceneManager.sceneLoaded += StartMinigame;
        _interact.enabled = false;
    }

    private void StartMinigame(Scene scn, LoadSceneMode lsm)
    {
        Systems.Status.Pause();
        SceneManager.sceneLoaded -= StartMinigame;

        (canvi = GameObject.Find("MiniGameClose")).SetActive(false);
        (camera = GameObject.Find("Main Camera")).SetActive(false);
        (vcam = GameObject.Find("CM vcam1")).SetActive(false);
        (sunlight = GameObject.Find("Sunlight")).SetActive(false);
        //(levelAudio = GameObject.Find("Audio")).SetActive(false);
        
        LevelMusic.SetActive(false);
        MiniGameMusic.SetActive(true);

        GameObject.Find("MiniGameMain").GetComponent<MiniGameMain>().OnWin += MiniGameFinished;
        GameObject.Find("MiniGameMain").GetComponent<MiniGameMain>().OnExit += MiniGameLeave;
    }

    private void MiniGameLeave()
    {
        SceneManager.UnloadSceneAsync(MiniGameSceneName);
        canvi.SetActive(true);
        camera.SetActive(true);
        vcam.SetActive(true);
        sunlight.SetActive(true);
        MiniGameMusic.SetActive(false);
        LevelMusic.SetActive(true);
        //levelAudio.SetActive(true);
        _interact.Kill();
    }
    private void MiniGameFinished()
    {
        Systems.Status.UnPause();
        MiniGameMusic.SetActive(false);
        LevelMusic.SetActive(true);
        DrainPipe.SetActive(false);
        BarrelEnd.SetActive(true);
        Particles.SetActive(false);
        Storm.SetActive(true);
        _canvas.ChangeText(BewareTheStorm);
        SchoolEntrance.enabled = true;
        Systems.Status.SpeedUpWarmthLoss();

        SceneManager.UnloadSceneAsync(MiniGameSceneName);

        Systems.Objectives.Satisfy("BARRELSETUP");
        camera.SetActive(true);
        vcam.SetActive(true);
        canvi.SetActive(true);
        sunlight.SetActive(true);
        //levelAudio.SetActive(true);
    }
}
