using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorgiSwapText : MonoBehaviour
{
    
    private InteractWithObject _interact;
    
    // Start is called before the first frame update
    void Start()
    {
        _interact = GetComponent<InteractWithObject>();
    }

    // Update is called once per frame
    public void Interaction()
    {
        _interact.SetInteractText("Press 'E' to save Tsu");
    }
}
