using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanitationSwapText : MonoBehaviour
{
    private InteractWithObject _interact;
    private void Start()
    {
        _interact = GetComponent<InteractWithObject>();
    }

    public void Interaction()
    {
        _interact.SetInteractText("Press 'E' to build sanitation system");
    }
        
        
        
        
}
