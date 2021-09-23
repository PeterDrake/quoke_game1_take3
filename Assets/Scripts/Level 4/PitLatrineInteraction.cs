using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PitLatrineInteraction : MonoBehaviour
{
    private InteractWithObject _interact;
    private InventoryHelper _inventory;
    private Item MustardWater;


    void Start()
    {
        _interact = GetComponent<InteractWithObject>();
        _inventory = Systems.Inventory;
        MustardWater = Resources.Load<Item>("Items/Jug");
    }


    public void Interaction()
    {
        string[] occupiedToiletResponses = new string[] { " 'I shouldn't have drunk that dirty water' ", " 'This will take a while.. Can you pass me the sports section??' ", " 'Esta ocupada!!' ", " 'Can't a person get any privacy??' " };
        System.Random randomResponses = new System.Random();
        int useOccupiedToiletResponses = randomResponses.Next (occupiedToiletResponses.Length);
        string pickResponse = occupiedToiletResponses[useOccupiedToiletResponses];

        if (_inventory.HasItem(MustardWater, 1))
        {
      
            _interact.enabled = false;
        }
        else
        {
            _interact.SetInteractText(pickResponse);

        }

    }

}
