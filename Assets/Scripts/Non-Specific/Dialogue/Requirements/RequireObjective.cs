using UnityEngine;

[CreateAssetMenu(fileName = "New Objective Requirement", menuName = "Dialogue/Requirements/Objective")]
public class RequireObjective : DialogueRequirement
{
    public string requiredEvent;
    
    [Header("Shouldnt be empty")]
    [SerializeField] private string failureMessage;
    
    public override bool TestSatisfaction()
    {
        return Systems.Objectives.Check(requiredEvent);
    }

    public override string GetFailureMessage() 
    {
        if (failureMessage == "")
            return base.GetFailureMessage();
        
        return failureMessage; 
    }
}
