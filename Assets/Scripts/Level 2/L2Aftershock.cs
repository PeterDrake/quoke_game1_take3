using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Aftershock : MonoBehaviour
{
    public ObjectDropper objectDropper;
    public void Start()
    {
        QuakeManager.Instance.OnQuake.AddListener(OnQuake);
    }

    private void OnQuake()
    {
        if (QuakeManager.Instance.quakes > 0)
        {
            //Logger.Instance.Log("Aftershock Started");
            LogToServer logger = GameObject.Find("Logger").GetComponent<LogToServer>();
            logger.sendToLog("Aftershock Started","EVENT");
            Systems.Status.PlayerDeath("Aftershock","The house collapsed in an aftershock!");
            objectDropper.Drop();
        }
    }
}
