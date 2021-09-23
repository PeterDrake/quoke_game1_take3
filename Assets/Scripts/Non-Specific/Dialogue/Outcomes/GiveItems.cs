using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Items Outcome", menuName = "Dialogue/Outcomes/GiveItem")]
public class GiveItems : DialogueOutcome
{
    public Item[] Items;

    [Header("Amount of each item that will be added")]
    public int[] Amounts;

    // Need a static reference to Inventory available before this can be implemented
    public override void DoOutcome(ref NPC n)
    {
        int i = 0;
        foreach (Item item in Items)
        {
            Systems.Inventory.AddItem(item, Amounts[i]);
            i++;
        }
        GameObject.Find("InventoryZip").GetComponent<AudioSource>().Play();
        //SavedData.addInv = true;
    }
}
