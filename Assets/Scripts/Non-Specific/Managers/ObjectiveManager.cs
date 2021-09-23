using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allows keeping track of in game events, such as TOILETEVENT, and LEVELFINISHED
/// Also allows registration of callback functions to be invoked when an event is satisfied
/// </summary>
public class ObjectiveManager : MonoBehaviour
{
    public delegate void Callback();

    [SerializeField] private Dictionary<String, bool> events;
    private Dictionary<String, LinkedList<Callback>> callbacks;


    private void Awake()
    {
        callbacks = new Dictionary<string, LinkedList<Callback>>();
        events = new Dictionary<string, bool>();
    }

    public bool Register(String key, Callback cb=null)
    {
        printObjectives();
        if (events.ContainsKey(key))
        {
            callbacks[key].AddFirst(cb);
            Debug.Log("Registered objective "+ key + "- returned true");
            return true;
        }
        
        events.Add(key, false);
        Debug.Log("Registered objective "+key+"- returned false");
        LogToServer logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        logger.sendToLog("Objective added: " + key,"OBJECTIVE");
        var ll = new LinkedList<Callback>();
        ll.AddFirst(cb);
        callbacks.Add(key, ll);
        return false;
    }

    public void Satisfy(String key, bool destroyCallbacks = true)
    {
        
        Debug.Log("Objective Satisfied: "+ key);
        LogToServer logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        logger.sendToLog("Objective satisfied: " + key,"OBJECTIVE");
        if (!events.ContainsKey(key))
        {
            events.Add(key,true);
        }
        else if(callbacks.ContainsKey(key) && !events[key])
        {
            foreach (Callback cb in callbacks[key])
            {
                cb();
            }

            if (destroyCallbacks) callbacks[key] = null;
            events[key] = true;
        }
        printObjectives();
    }

    public bool Check(String key)
    {
        return events.ContainsKey(key) && events[key];
    }
    
    //For debugging only
    public void printObjectives()
    {
        Debug.Log("printObjectives called");
        foreach (var VARIABLE in events)
        {
            Debug.Log(VARIABLE.ToString());
        }

        /*
        foreach (var VARIABLE in callbacks)
        {
            Debug.Log(VARIABLE.ToString());
        }
        Debug.Log(events.ToString());
        Debug.Log(callbacks.ToString());
        */
    }
}
