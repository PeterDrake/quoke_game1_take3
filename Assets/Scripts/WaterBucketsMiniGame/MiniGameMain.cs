using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameMain : MonoBehaviour
{

    public bool Barrel;

    public GameObject Win;

    public GameObject Wrong;

    public UnityAction OnWin;

    public UnityAction OnExit;
    
    private LogToServer logger;

    public void Start()
    {
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        logger.sendToLog("Began water barrel minigame","MINIGAME");
        //OnWin= new UnityAction();
    }

    public void CheckCorrect()
    {
        if (Barrel)
        {
            logger.sendToLog("Won water barrel minigame","MINIGAME");
            Win.SetActive(true);
        }
        else
        {
            StartCoroutine(TryAgain());
        }
    }

    public void Leave()
    {
        OnExit.Invoke();
    }

    public IEnumerator TryAgain()
    {
        logger.sendToLog("Wrong answer try again","MINIGAME");
        Wrong.SetActive(true);
        yield return new WaitForSeconds(3f);
        Wrong.SetActive(false);
    }

    public void WinLeave()
    {
        OnWin.Invoke();
    }



}
