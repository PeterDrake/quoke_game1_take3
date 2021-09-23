using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewHangTag : MonoBehaviour
{
    public GameObject Hangtag;
    public Button Close;

    private bool isOpen;
    private LogToServer logger;

    private void Start()
    {
        //Hangtag.SetActive(false);
        //isOpen = false;
        //Debug.Log("Begin : is open = " + isOpen);
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
    }

    public void OnMouseDown()
    {
            //isOpen = true;
            Hangtag.SetActive(true);
            Debug.Log("Opened pamphlet");
            logger.sendToLog("Opened pamphlet","MINIGAME");
            //Debug.Log("is open should be true = " + isOpen);

    }

    public void closeHangTag()
    {
            //isOpen = false;
            Debug.Log("Closed pamphlet");
            logger.sendToLog("Closed pamphlet","MINIGAME");
            Hangtag.SetActive(false);
            //Debug.Log("isOpen is now false = " + isOpen);
    }
}
