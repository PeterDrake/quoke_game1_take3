using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Image = UnityEngine.UIElements.Image;

public class BoxCheck : MonoBehaviour
{
    // public GameObject itemInBox;
    private MiniGameMaster MasterCheck;
    private FrameColorChange Frame;

    //use this funciton when an item collider is staying in this box collider
    // public void OnTriggerStay(Collider item)
    // {
    //     item.gameObject.GetComponent<DragObject>().PlaceItemInBox(this.gameObject);
    //     item.gameObject.GetComponent<BoxCollider>().enabled = false;
    //     item.gameObject.GetComponent<DragObject>().SetLastCollision(this.name);
    //     BlinkFrame = gameObject.GetComponent<BlinkFrame>();
    
    //     BlinkFrame.Blink();
    //     Debug.Log(item.name.ToString() + "enters " + this.name);
    // }
    //use this function when an item collider exits this box collider
    // public void OnTriggerExit(Collider item)
    // {
    //     item.gameObject.GetComponent<DragObject>().inBox = false;
    //     // print("exit");
    //     BlinkFrame.StopBlink();
    // }
    void Start()
    {
        Frame = gameObject.GetComponent<FrameColorChange>();
        MasterCheck = GameObject.Find("MinigameMaster").GetComponent<MiniGameMaster>();
    }

    public bool CheckBox(GameObject itemInBox)
    {
        if (this.CompareTag(itemInBox.tag))
        {
            Frame.Correct();
            SetMasterTrue();
            return true;
        }
        else 
        {
            Frame.Wrong();
            return false;
        }
        //if item is not null then update box color && update master && delete things
    }

    public void ResetBox()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        Frame.Empty();   
    }

    private void SetMasterTrue()
    {   
        Debug.Log("Completed" + this.name);
        if (this.name == "PeeBucketBox")
        {
            MasterCheck.PeeBucket = true;
        }
        else if (this.name == "PooBucketBox")
        {
            MasterCheck.PooBucket = true;
        }
        else if (this.name == "PlasticBagBox")
        {
            MasterCheck.PlasticBag = true;
        }
        else if (this.name == "PoopBox")
        {
            MasterCheck.Poop = true;
        }
        else if (this.name == "ToiletPaperBox")
        {
            MasterCheck.ToiletPaper = true;
        }
        else if (this.name == "SawdustBox")
        {
            MasterCheck.Sawdust = true;
        }
        else if (this.name == "PeeBox")
        {
            MasterCheck.Pee = true;
        }
    }
}
