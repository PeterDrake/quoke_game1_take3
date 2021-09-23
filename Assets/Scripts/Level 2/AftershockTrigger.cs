using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AftershockTrigger : MonoBehaviour
{
    private bool aftershock = false;
    public bool _satisfied;


    // Attached to a game object along with a collider to detect when the player last left the house
    public InformationCanvas _canvas;
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("CHANGE TO TWO");
        QuakeManager.Instance.TriggerCountdown(2f);
        aftershock = true;
        _canvas.ChangeText("Talk to survivors");
        Systems.Objectives.Satisfy("LEAVEHOUSE");
        //Systems.Objectives.Register("TOILETTALK", () => _satisfied = true); Redundant with MissionBucket
        if (GameObject.Find("TablePointer") != null)
        { GameObject.Find("TablePointer").GetComponent<FlatFollow>().disappear(); }
        //Logger.Instance.Log("Player has left the house");
        LogToServer logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        //this does not seem to work
        logger.sendToLog("Player left the house","LOCATION");
        Destroy(gameObject);
    }
}
