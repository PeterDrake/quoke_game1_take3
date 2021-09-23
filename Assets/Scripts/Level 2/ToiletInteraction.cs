using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletInteraction : MonoBehaviour
{    
    private InteractWithObject _interact;
    private void Start()
    {
        _interact = GetComponent<InteractWithObject>();
    }

    public void Interaction()
    {
        Systems.Status.AffectRelief(100);
        GameObject.Find("MeterDing").GetComponent<AudioSource>().Play();
        //_interact.SetInteractText("Press 'E' to Use Toilet");
        _interact.DeleteItems();
        //_interact = null;
        }
    }

