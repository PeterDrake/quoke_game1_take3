using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Objective Outcome", menuName = "Dialogue/Outcomes/SlowDownHydrationMeter")]
public class SlowDownHydrationMeter : DialogueOutcome
{
    public override void DoOutcome(ref NPC n)
    {
        Systems.Status.SlowDownHydrationLoss();
    }
}
