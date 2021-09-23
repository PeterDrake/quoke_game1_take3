using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameMaster : MonoBehaviour
{

    public bool PeeBucket;

    public bool PooBucket;

    public bool PlasticBag;

    public bool Poop;

    public bool ToiletPaper;

    public bool Sawdust;

    public bool Pee;
    private bool match = true;
    public Button submitButton;
    
    public GameObject Win;

    //public GameObject WinSound;

    public GameObject Wrong;
    
    public UnityAction OnWin;

    public UnityAction OnExit;

    private LogToServer logger;


    //begin with setting up logging information
    public void Start()
    {
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        logger.sendToLog("Began toilet minigame","MINIGAME");
    }

    //if all boxes are correct we will win the game
    //else show the try again pop up for a bit
    public void CheckCorrect()
    {
        if (PeeBucket && PooBucket && PlasticBag && Poop && ToiletPaper && Sawdust && Pee)
        {
            // Debug.Log("Won toilet mini game");
            logger.sendToLog("Won toilet mini game","MINIGAME");
            Win.SetActive(true);
            GameObject.Find("Sanitation Spot").GetComponent<SanitationBuilt>().MiniGameWon();
        }
        else
        {
            StartCoroutine(TryAgain());
        }
    }

    //Show the try again pop up for a bit
    public IEnumerator TryAgain()
    {
        logger.sendToLog("Attempted to sanitize hands", "MINIGAME");
        Wrong.SetActive(true);
        yield return new WaitForSeconds(3f);
        Wrong.SetActive(false);
    }

    public void WinLeave()
    {
        OnWin.Invoke();
    }

    void Update()
    {
        if (PeeBucket && Pee && PooBucket && PlasticBag && Poop && ToiletPaper && Sawdust && match)
        {
            StartCoroutine(BlinkText());
        }

    }
    //function will flash the sanitize hands & used once all items are placed correctly
    public IEnumerator BlinkText()
    {
        match = false;
        while (true)
        {
            submitButton.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            yield return new WaitForSeconds(.3f);
            submitButton.GetComponent<UnityEngine.UI.Image>().color = Color.white;
            yield return new WaitForSeconds(.3f);
        }
    }
    
    
}
