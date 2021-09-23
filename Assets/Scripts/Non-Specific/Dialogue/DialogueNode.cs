using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueNode", menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    [TextArea]
    public string speech;

    [TextArea]
    [SerializeField] protected string optionOneText;
    [SerializeField] protected DialogueNode optionOne;
    [Space]

    [TextArea]
    [SerializeField] protected string optionTwoText;
    [SerializeField] protected DialogueNode optionTwo;
    [Space]

    [SerializeField] private DialogueRequirement[] Requirements;
    [SerializeField] private DialogueOutcome[] Outcomes;



    [Space]
    [Space]
    [Header("If set, will become the new head once traversed")]
    [SerializeField] private DialogueNode NewHead;

    public virtual DialogueNode GetNodeOne() { return optionOne; }
    public virtual DialogueNode GetNodeTwo() { return optionTwo; }

    public virtual string GetTextOne() { return optionOneText; }
    public virtual string GetTextTwo() { return optionTwoText; }


    public string CheckRequirements()
    {
        if (Requirements != null)
            return CheckRequirements(Requirements);

        return "";
    }

/*    protected string CheckEightRequirements(EightRequirements[] fields)
    {
        foreach (EightReuirements[] req in fields)
        {
            if (req == null) continue;
            if (!req.TestSatisfaction()) return req.GetFailureMessage();
        }

        return "";
    }*/

    protected string CheckRequirements(DialogueRequirement[] dr)
    {
        foreach (DialogueRequirement req in dr)
        {
            if (req == null) continue;
            if (!req.TestSatisfaction()) return req.GetFailureMessage();
        }

        return "";
    }


    public void DoOutcomes(ref NPC n)
    {
        foreach (DialogueOutcome oc in Outcomes)
        {
            oc.DoOutcome(ref n);
        }
    }
    public DialogueNode GetNewHead()
    {
        return NewHead;
    }
}
