using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enable a Script", menuName = "Dialogue/Outcomes/EnablingComponents")]
public class EnablingComponents : DialogueOutcome
{
    public string WhatGameObject;

    public override void DoOutcome(ref NPC n)
    {
        GameObject.Find(WhatGameObject).GetComponent<InteractWithObject>().enabled = true;
        GameObject.Find(WhatGameObject).GetComponent<SphereCollider>().enabled = true;
    }
}