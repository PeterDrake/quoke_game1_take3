using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExploreArea : MonoBehaviour
{

    //private GameObject TheObject;
    public UnityEvent OnEnter;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnEnter.Invoke();
            //TheObject.QuakeCommence.incrementExplored();
        }
    }

    
   
}
