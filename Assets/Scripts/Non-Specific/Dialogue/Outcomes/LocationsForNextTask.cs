using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Objective Outcome", menuName = "Dialogue/Outcomes/LocationsForNextTask")]
public class LocationsForNextTask : DialogueOutcome
{
    public GameObject taskCompleted;
    public string nextTask;
    public bool autoNext;

    public override void DoOutcome(ref NPC n)
    {
        HintController controller = GameObject.Find("LocationsOfInterest").GetComponent<HintController>();
        if (nextTask != null)
        {
            Debug.Log("Next task is === " + nextTask);
            controller.StartThisTask(nextTask);
        }
        if(autoNext)
        {
            controller.NextTask();
        }
    }
}
