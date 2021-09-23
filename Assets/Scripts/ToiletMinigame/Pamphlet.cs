using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pamphlet : MonoBehaviour
{
    public GameObject pamphlet;
    public Text buttonText;
    public GameObject items = null;

    private string openText = "Open Pamphlet";
    private string closeText = "Close Pamphlet";

    public bool invopen;
    private bool open;

    private LogToServer logger;

    private void Awake()
    {
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        pamphlet.SetActive(false);
        buttonText.text = openText;
        if (GameObject.Find("Items") != null)
        {
            items = GameObject.Find("Items").gameObject;
        }
    }

    public void toggle()
    {
        if (invopen)
        {
            Debug.Log("Closed pamphlet");
            logger.sendToLog("Closed pamphlet", "PAMPHLET");
            pamphlet.SetActive(false);
            buttonText.text = openText;
        }
        else
        {
            Debug.Log("Opened pamphlet");
            logger.sendToLog("Opened pamphlet", "PAMPHLET");
            pamphlet.SetActive(true);
            buttonText.text = closeText;
        }
        invopen = !invopen;
    }

    public void miniToggle()
    {
        if (open)
        {
            Debug.Log("Closed pamphlet");
            items.SetActive(true);
            logger.sendToLog("Closed pamphlet", "MINIGAME");
            pamphlet.SetActive(false);
            buttonText.text = openText;
        }
        else
        {
            Debug.Log("Opened pamphlet");
            items.SetActive(false);
            logger.sendToLog("Opened pamphlet", "MINIGAME");
            pamphlet.SetActive(true);
            buttonText.text = closeText;
        }
        open = !open;
    }

}

