using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Calls functions when player exits or leaves the area denoted by a trigger box collider on this object
/// </summary>
public class PlayerInArea : MonoBehaviour
{
    public UnityEvent OnEnter;

    public UnityEvent OnExit;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Logger.Instance.Log("Player entered area of: "+name);
            LogToServer logger = GameObject.Find("Logger").GetComponent<LogToServer>();
            logger.sendToLog("Player entered area of: " + name,"LOCATION");
            OnEnter.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Logger.Instance.Log("Player exited area of: "+name);
            LogToServer logger = GameObject.Find("Logger").GetComponent<LogToServer>();
            logger.sendToLog("Player exited area of: " + name,"LOCATION");
            OnExit.Invoke();
        }

    }
}
