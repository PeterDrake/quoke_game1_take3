using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitLatrineSwapText : MonoBehaviour
{
    private InteractWithObject _interact;
    private void Start()
    {
        _interact = GetComponent<InteractWithObject>();
    }

    public void Interaction()
    {
        _interact.SetInteractText("Press 'E' to dig");
    }
}
