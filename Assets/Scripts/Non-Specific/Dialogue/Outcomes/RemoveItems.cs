using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item Removal Outcome", menuName = "Dialogue/Outcomes/RemoveItem")]
public class RemoveItem : DialogueOutcome
{
    [Header("Amount of each item that will be removed")]
    public int[] Amounts;
    
    // Need a static reference to Inventory available before this can be implemented
    public override void DoOutcome(ref NPC n)
    {
        throw new System.NotImplementedException();
    }
}
