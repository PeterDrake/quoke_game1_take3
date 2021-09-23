using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueNode", menuName = "Dialogue/IfElseNode")]
public class IfElseNode : DialogueNode
{
    [Header("If the related requirements are not satisfied, the alt will be used")]
    [Space]
    public DialogueRequirement[] IfRequirementsOne;
    [TextArea]
    [SerializeField] private string optionOneTextAlt;
    [SerializeField] private DialogueNode optionOneAlt;
    [Space]
    
    public DialogueRequirement[] IfRequirementsTwo;
    [TextArea]
    [SerializeField] private string optionTwoTextAlt;
    [SerializeField] private DialogueNode optionTwoAlt;

    public override DialogueNode GetNodeOne()
    {
        return (CheckRequirements(IfRequirementsOne) == "") ? optionOne : optionOneAlt;
    }
    public override DialogueNode GetNodeTwo()
    {
        return (CheckRequirements(IfRequirementsTwo) == "") ? optionTwo : optionTwoAlt;
    }

    public override string GetTextOne()
    {
        return (CheckRequirements(IfRequirementsOne) == "") ? optionOneText : optionOneTextAlt;
    }
    public override string GetTextTwo()
    {
        return (CheckRequirements(IfRequirementsTwo) == "") ? optionTwoText : optionTwoTextAlt;
    }
    
    
}
