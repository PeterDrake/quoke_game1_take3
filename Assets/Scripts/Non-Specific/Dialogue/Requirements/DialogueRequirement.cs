using UnityEngine;

/// <summary>
/// A requirement that must be met in order for a dialogue node to be accessible 
/// Must implement TestSatisfacation and GetFailureMessage,
/// inherited members are:
/// * Satisfied - is this requirement satisfied?
/// * failureMessage - the message that will be displayed to the player if the requirement isn't met
/// </summary>
public abstract class DialogueRequirement : ScriptableObject
{
    public abstract bool TestSatisfaction();

    public virtual string GetFailureMessage()
    {
        return "Requirement Not Satisfied";
    }
}
