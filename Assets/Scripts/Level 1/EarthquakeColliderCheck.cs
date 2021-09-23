using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EarthquakeColliderCheck : MonoBehaviour
{
    public UnityEvent OnEnter;

    //public UnityEvent OnExit;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnEnter.Invoke();
            // if other two are triggered
                    //call for an earthquake
        }
    }

    /*
    public void OnTriggerExit(Collider other)    //Destroy earthquake script and only leave aftershock???
    {
        if (other.CompareTag("Player"))
        {
            //if earthquake happened, destroy script
            Logger.Instance.Log("Player exited area of: "+name);
            OnExit.Invoke();
        }

    }*/
}
