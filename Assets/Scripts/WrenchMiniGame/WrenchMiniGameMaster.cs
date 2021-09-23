using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class WrenchMiniGameMaster : MonoBehaviour
{

    public GameObject WinScreen;
    
    public UnityAction OnWin;

    public UnityAction OnExit;

    
    public void CheckCorrect(bool Turned)
    {
        if (Turned)
        {
            //Debug.Log("Congratz, you have won!");
            //Debug.Log("Won Gas mini game");
            LogToServer logger = GameObject.Find("Logger").GetComponent<LogToServer>();
            //This doesn't seem to print
            logger.sendToLog("Won gas minigame","MINIGAME");
            WinScreen.SetActive(true);

           // SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        }
        
    }

    public void Leave()
    {
        OnExit.Invoke();
    }

    public void WinLeave()
    {
        OnWin.Invoke();
    }
}
