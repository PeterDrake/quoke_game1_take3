using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostVisit : MonoBehaviour
{
    CompostingMaster master;
    InteractWithObject _interact;

    // Start is called before the first frame update
    void Start()
    {
        master = GameObject.Find("CompostingMaster").GetComponent<CompostingMaster>();
        _interact = GetComponent<InteractWithObject>();
    }

   public void Interaction()
    {
        if (Systems.Objectives.Check("CompostInstructions"))
        {
            master.AddCarbon();
            master.BuildBox();
            GameObject.Find("LocationsOfInterest").GetComponent<HintController>().AllTaskCompleted(GameObject.Find("Compost"));

        }
        else
        {
            _interact.SetInteractText("Go talk to Bruce");

        }
    }
}
