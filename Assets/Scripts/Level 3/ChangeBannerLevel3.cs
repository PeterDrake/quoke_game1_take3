using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBannerLevel3 : MonoBehaviour
{
    public InformationCanvas _canvas;

    public void UpdateBanner()
    {
        //When you have talked about water and latrine
        if (Systems.Objectives.Check("WaterTalk") && Systems.Objectives.Check("LatrineTalk"))
        {
            //has all the items
            if ((Systems.Inventory.HasItem(Resources.Load<Item>("Items/CleanMustardWater"), 1) 
            || Systems.Inventory.HasItem(Resources.Load<Item>("Items/DirtyMustardWater"), 1)) 
            && Systems.Inventory.HasItem(Resources.Load<Item>("Items/Shovel"), 1)
            && Systems.Inventory.HasItem(Resources.Load<Item>("Items/Rope"), 1))
            {
                _canvas.ChangeText("Find Frank's back yard");
            }
            // has a water and shovel but NO rope
            else if ((Systems.Inventory.HasItem(Resources.Load<Item>("Items/CleanMustardWater"), 1) 
            || Systems.Inventory.HasItem(Resources.Load<Item>("Items/DirtyMustardWater"), 1)) 
            && Systems.Inventory.HasItem(Resources.Load<Item>("Items/Shovel"), 1)
            && !Systems.Inventory.HasItem(Resources.Load<Item>("Items/Rope"), 1))
            {
                _canvas.ChangeText("Find some rope");
            }
            // has a water and rope but NO shovel
            else if ((Systems.Inventory.HasItem(Resources.Load<Item>("Items/CleanMustardWater"), 1) 
            || Systems.Inventory.HasItem(Resources.Load<Item>("Items/DirtyMustardWater"), 1)) 
            && !Systems.Inventory.HasItem(Resources.Load<Item>("Items/Shovel"), 1)
            && Systems.Inventory.HasItem(Resources.Load<Item>("Items/Rope"), 1))
            {
                _canvas.ChangeText("Find a shovel");
            }
            // has water but NO shovel or rope
            else if ((Systems.Inventory.HasItem(Resources.Load<Item>("Items/CleanMustardWater"), 1) 
            || Systems.Inventory.HasItem(Resources.Load<Item>("Items/DirtyMustardWater"), 1)) 
            && !Systems.Inventory.HasItem(Resources.Load<Item>("Items/Shovel"), 1)
            && !Systems.Inventory.HasItem(Resources.Load<Item>("Items/Rope"), 1))
            {
                _canvas.ChangeText("Find a shovel and some rope");
            }
            // has rope but no water or shovel
            else if (!(Systems.Inventory.HasItem(Resources.Load<Item>("Items/CleanMustardWater"), 1) 
            || Systems.Inventory.HasItem(Resources.Load<Item>("Items/DirtyMustardWater"), 1)) 
            && !Systems.Inventory.HasItem(Resources.Load<Item>("Items/Shovel"), 1)
            && Systems.Inventory.HasItem(Resources.Load<Item>("Items/Rope"), 1))
            {
                _canvas.ChangeText("Find clean water and a shovel");
            }       
            // has nothing
            else if (!(Systems.Inventory.HasItem(Resources.Load<Item>("Items/CleanMustardWater"), 1) 
            || Systems.Inventory.HasItem(Resources.Load<Item>("Items/DirtyMustardWater"), 1)) 
            && !Systems.Inventory.HasItem(Resources.Load<Item>("Items/Shovel"), 1)
            && !Systems.Inventory.HasItem(Resources.Load<Item>("Items/Rope"), 1))
            {
                _canvas.ChangeText("Find clean water, a shovel, and some rope");
            }
        }

        //When you have talked about water but not latrine
        else if (Systems.Objectives.Check("WaterTalk") && !Systems.Objectives.Check("LatrineTalk"))
        {
            if ((Systems.Inventory.HasItem(Resources.Load<Item>("Items/CleanMustardWater"), 1) 
            || Systems.Inventory.HasItem(Resources.Load<Item>("Items/DirtyMustardWater"), 1)))
            {
                _canvas.ChangeText("Talk to other survivors");
            }
            else 
            {
                _canvas.ChangeText("Find clean water");
            }

        }
        //When you have talked to Frank about latrine but not water
        else if (Systems.Objectives.Check("LatrineTalk") && !Systems.Objectives.Check("WaterTalk"))
        {
            //if you have a rope and shovel change text to TALK TO OTHER SURVIVORS
            if (Systems.Inventory.HasItem(Resources.Load<Item>("Items/Rope"), 1) 
            && Systems.Inventory.HasItem(Resources.Load<Item>("Items/Shovel"), 1))
            {
                _canvas.ChangeText("Talk to other survivors");
            }

            //if you have a rope and no shovel change text to FIND A SHOVEL
            else if (Systems.Inventory.HasItem(Resources.Load<Item>("Items/Rope"), 1) 
            && !Systems.Inventory.HasItem(Resources.Load<Item>("Items/Shovel"), 1))
            {
                _canvas.ChangeText("Find a shovel");
            }
            
            //if you have no rope and a shovel change text to FIND A ROPE
            else if (!Systems.Inventory.HasItem(Resources.Load<Item>("Items/Rope"), 1) 
            && Systems.Inventory.HasItem(Resources.Load<Item>("Items/Shovel"), 1))
            {
                _canvas.ChangeText("Find some rope");
            }

            //if you have no rope and no shovel change text to FIND A ROPE AND A SHOVEL
            else if (!Systems.Inventory.HasItem(Resources.Load<Item>("Items/Rope"), 1) 
            && !Systems.Inventory.HasItem(Resources.Load<Item>("Items/Shovel"), 1))
            {
                _canvas.ChangeText("Find some rope and a shovel");
            }
        }
    }
}
