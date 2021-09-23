using UnityEngine;
using Debug = UnityEngine.Debug;

[CreateAssetMenu(fileName = "New Bleach Action", menuName = "Items/Actions/Tablet")]
public class AddTablet : ItemAction
{
    private InformationCanvas _banner;

    public override bool Use(ref Item i)
    {
        Item item;
        if (Systems.Inventory.HasItem((item = Resources.Load<Item>("Items/DirtyMustardWater")), 1))
        {
            Systems.Inventory.RemoveItem(Resources.Load<Item>("Items/PurificationTablet"), 4);
            Systems.Inventory.RemoveItem(item, 1);
            Systems.Inventory.AddItem(Resources.Load<Item>("Items/CleanMustardWater"), 1);

            _banner = GameObject.Find("GUI").GetComponent<GuiDisplayer>().GetBanner();
            string current = _banner.info.text;

            if (current.Contains(", and clean water"))
            {
                current = current.Replace(", and clean water", "");
                current = current.Replace(", ", ", and ");
            }
            current = current.Replace(", clean water", "");
            current = current.Replace(" clean water,", "");
            current = current.Replace("Find and", "Find");
            
            if (current == "Find ")
            {
                _banner.ChangeText("Talk to survivors");
            }
            //things left on list to find
            else
            {
                _banner.ChangeText(current);
            }
        }

        return false;
    }
}
