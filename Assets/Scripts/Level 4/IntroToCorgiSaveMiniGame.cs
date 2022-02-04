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
    private bool check;

    //check if the sanitation is built
    void Update()
    {
        if (CompostingToilet.activeSelf && !check)
        {
            SceneManager.LoadSceneAsync("Scenes/Levels/CorgiCutScene"); // This activates SadCorgiManager
            // StartCoroutine(nameof(StartCutScene));
            check = true; //used this bool so the coroutine is triggered only once
        }
    }
    
}
