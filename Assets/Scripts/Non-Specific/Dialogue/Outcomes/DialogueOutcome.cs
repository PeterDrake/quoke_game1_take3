using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An outcome that will be triggered upon reaching a dialogue node.
/// Must implement DoOutcome,
/// inherited members are:
/// * Trigger - should dialogue manager trigger this outcome?
/// </summary>
public abstract class DialogueOutcome : ScriptableObject
{
    public abstract void DoOutcome(ref NPC n);
}
