using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Action", menuName = "Items/Actions/Gun")]
public class ThrowAwayGun : ItemAction
{
    public override bool Use(ref Item i)
    {
        Item item;
        if (Systems.Inventory.HasItem((item = Resources.Load<Item>("items/Gun")),1))
        {
            Systems.Inventory.RemoveItem(item,1);
        }
        
        return false;
    }
}
