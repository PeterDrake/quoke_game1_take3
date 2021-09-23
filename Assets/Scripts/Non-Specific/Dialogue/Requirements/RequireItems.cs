using UnityEngine;

[CreateAssetMenu(fileName = "New Item Requirement", menuName = "Dialogue/Requirements/Items")]
public class RequireItems : DialogueRequirement
{
    public Item[] RequiredItems;
    
    [Header("Amount of each item that is required")]
    public int[] Amounts;
    
    // Need a static reference to Inventory available before this can be implemented
    public override bool TestSatisfaction()
    {
        return Systems.Inventory.HasItem(RequiredItems, Amounts);
    }

    public override string GetFailureMessage()
    {
        return "You don't have all the required items!";
    }
}
