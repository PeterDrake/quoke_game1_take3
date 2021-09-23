using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Objective Outcome", menuName = "Dialogue/Outcomes/ChangePointersOrAlerts")]
public class ChangePointersOrAlerts : DialogueOutcome
{
    public string ObjectiveName;

    public string targetGameObject;

    public bool makeAppear;

    private GameObject target;

    public override void DoOutcome(ref NPC n)
    {
        target = GameObject.Find(targetGameObject);
        if (target == null) return;

        if (makeAppear)
        {
            GameObject.Find(targetGameObject).GetComponent<FlatFollow>().appear();
        }
        else 
        { 
            GameObject.Find(targetGameObject).GetComponent<FlatFollow>().disappear(); 
        }
    }
}
