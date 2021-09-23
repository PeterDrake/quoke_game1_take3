using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsuBark : MonoBehaviour
{
    private const string EventKey = "LEVELFINISHED";
    private const string SATISFIED = "Press 'E' to interact with car";
    private const string NOT_SATISFIED = "";
    //public GameObject theBark;
    public InformationCanvas _canvas;
    private InteractWithObject _interact;
    private bool _satisfied;
    public AudioSource Bark;
    public StartDialogue Talk;

    private void Start()
    {
        _interact = GetComponent<InteractWithObject>();
        Systems.Objectives.Register(EventKey, () => _satisfied = true);
    }

    public void OnEnter()
    {
        _interact.SetInteractText(_satisfied ? SATISFIED : NOT_SATISFIED);
        _interact.BlinkWhenPlayerNear = _satisfied;
    }
    public void Interact()
    {
        if (_satisfied)
        {
            Bark.Play();
            Talk.Interact();
            //theBark.SetActive(true); //changed from winCanvas
        }
    }

    public void Update()
    {
        if (_satisfied)
        {
            _canvas.ChangeText("Find Ahmad's vehicle");

        }
    }
}
