using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Objective Outcome", menuName = "Dialogue/Outcomes/MoFollow")]
public class MoFollow : DialogueOutcome
{

    public override void DoOutcome(ref NPC n)
    {
        
        GameObject.Find("MoController").GetComponent<NavMeshAgent>().enabled = true;
        GameObject.Find("MoController").GetComponent<MoFollowingZelda>().enabled = true;
        GameObject.Find("MoController").GetComponent<MoFollowingZelda>().TurnOffBanner();
        GameObject.Find("MoController").GetComponent<MoFollowingZelda>().Follow();

    }


}
