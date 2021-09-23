using System.Collections;
using System.Collections.Generic;
//using System.Media;
using System.Runtime.Versioning;
using UnityEngine;
using Debug = UnityEngine.Debug;

[CreateAssetMenu(fileName = "New Objective Outcome", menuName = "Dialogue/Outcomes/ChangeBanner")]
public class DialogueChangeBanner : DialogueOutcome
{
    public string ObjectiveName;

    private InformationCanvas _banner;
    private string current;
    public string words;
    public Item ItemToFind;
    private string shorten;
    public bool Find;
    private bool find;
    public bool Found;

    public bool completeReset;
    public string resetTo;

    public bool _satisfied;
    
    
    public override void DoOutcome(ref NPC n)
    {
        find = Find;

        _satisfied = false;

        if ((Systems.Objectives.Register(ObjectiveName, () => _satisfied = true))) 
        {
            //this is the offending statement
            Systems.Objectives.Satisfy(ObjectiveName);
        }

        _banner = GameObject.Find("GUI").GetComponent<GuiDisplayer>().GetBanner();
        current = _banner.info.text;
        shorten = words.Replace("Find ", "");

        //no repeats
        if (current.Contains(shorten) || (ItemToFind != null && Systems.Inventory.HasItem(ItemToFind, 1)))
        { 
            find = false;
            
        }
        //for any find task
        if (find)
        {
            // Find task
            if (words.Contains("Find") && current.Contains("Find"))
            {
                current = current.Replace(" and ", ", ");
                current = current + " and " + shorten;
                _banner.ChangeText(current);
            }
            // any task to replace entire banner
            else
            {
                _banner.ChangeText(words);
            }
        }
        //for any found task
        if (Found)
        {
            Debug.Log("Found item");




            //if there are three or more things
            if (current.Contains(" and " + shorten)) //if the found item is the last in the list
            {
                //remove the found item from the list
                current = current.Replace(" and " + shorten, "");
                //add "and" in place of commas
                current = current.Replace(", ", " and ");
            }
            //if the found item is not the last in the list
            else 
            { 
                current = current.Replace(shorten + ", ", "");  //if there are commas between the items
                current = current.Replace(shorten + " and ", "");   //if there is an and after the found item
            }
            //current = current.Replace(shorten, "");
            //current = current.Replace(" ,", "");
            current = current.Replace(", and ", " and ");
            current = current.Replace("Find and", "Find");

            //if there is nothing to find
            

                // found everything go to franks yard in L3
                if (Systems.Inventory.HasItem(Resources.Load<Item>("Items/CleanMustardWater"), 1)
                    && Systems.Inventory.HasItem(Resources.Load<Item>("Items/Rope"), 1)
                    && Systems.Inventory.HasItem(Resources.Load<Item>("Items/Shovel"), 1))
                {
                    _banner.ChangeText("Find Frank's back yard");
                }

            /*
            else if (Systems.Inventory.HasItem(Resources.Load<Item>("Items/Shovel"), 1))
            {
                _banner.ChangeText("Find a rope");
            }

            else if (Systems.Inventory.HasItem(Resources.Load<Item>("Items/Rope"), 1))
            {
                _banner.ChangeText("Find a shovel");
            }
            */

            //else if (Systems.Inventory.HasItem(Resources.Load<Item>("Items/DirtyMustardWater"), 1))
            //{
            //    _banner.ChangeText("Add tablets from inventory to clean water");
            //}
            //else if (Systems.Inventory.HasItem(Resources.Load<Item>("Items/Gun"), 1))
            //{
            //    _banner.ChangeText("Throw away the gun, then ask Zelda to enter the shelter");
            //}
            //else if (Systems.Inventory.HasItem(Resources.Load<Item>("Items/Gun"), 0))
            //{
            //    _banner.ChangeText("Enter the shelter");
            //}
            //else if (Systems.Inventory.HasItem(Resources.Load<Item>("Items/CleanMustardWater"), 1))
            //{
            //    _banner.ChangeText("Go talk to Zelda");
            //}

            //if (Systems.Inventory.HasItem(Resources.Load<Item>("Items/DirtyMustardWater"), 1))
            //{
            // _banner.ChangeText("Add tablets from inventory to clean water");
            //}
            //  if (Systems.Inventory.HasItem(Resources.Load<Item>("Items/CleanMustardWater"), 1) && Systems.Inventory.HasItem(Resources.Load<Item>("Items/Shovel"), 1))
            //  {
            //     _banner.ChangeText("Talk to Frank about building a pit latrine");
            // }
            //found everything for now but more things to find
            else if (current == "Find")
                {
                    _banner.ChangeText("Talk to survivors");
                }
                //things left on list to find
                else
                {
                    _banner.ChangeText(current);
                }
        }

        if (completeReset)
        {
            _banner.ChangeText(resetTo);
        }
    }
}
