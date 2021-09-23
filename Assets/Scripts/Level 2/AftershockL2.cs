﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AftershockL2 : MonoBehaviour
{
    // This is added in the inspector to the QuakeManager's OnQuake event 
    public void TriggerOnQuake()
    {
        if (QuakeManager.Instance.quakes > 0)
        {
            Systems.Status.PlayerDeath("Aftershock","The house collapsed due to an after shock!");
        }
    }
}
