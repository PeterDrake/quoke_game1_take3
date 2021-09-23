using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarrelWithWaterVisit : MonoBehaviour
{
    public Item PotWithWater;

    public GameObject BoilWater;
   

    private InteractWithObject _interact;
    private InventoryHelper _inventory;



    private Item Pot;
    private Item Wood;



    void Start()
    {
        _interact = GetComponent<InteractWithObject>();
        _inventory = Systems.Inventory;


        Pot = Resources.Load<Item>("Items/Pot");
        Wood = Resources.Load<Item>("Items/Wood");
    }

    public void Interaction()
    {
        
        if (_inventory.HasItem(Pot, 1) && _inventory.HasItem(Wood, 1))
        {
            _interact.enabled = false;
            BoilWater.SetActive(true);
            //BarrelWithWater.GetComponent<InteractWithObject>().disabled;
            GameObject.Find("BarrelWithWater").GetComponent<InteractWithObject>().OnTriggerExit(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>());
            GameObject.Find("BarrelWithWater").GetComponent<InteractWithObject>().enabled = false;
            GameObject.Find("BarrelWithWater").GetComponent<SphereCollider>().enabled = false;
            Systems.Inventory.AddItem(PotWithWater, 1);
            GameObject.Find("BoilWaterMaster").GetComponent<BoilWaterMaster>().FillPotWithWater();
        }
        else
        {
            _interact.SetInteractText("Go talk to Ahmad about water");
            //BoilWater.SetActive(false);
            

        }

    }
}