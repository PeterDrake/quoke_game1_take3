using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using MoreMountains.FeedbacksForThirdParty;
// using MoreMountains.InventoryEngine;
using UnityEngine.SceneManagement;

public class ToiletCheck : MonoBehaviour
{
    
    private InteractWithObject _interact;
    // private MoreMountains.InventoryEngine.Inventory _inventory;

    // public BaseItem Bucket;
    // public BaseItem Bag;
    // public BaseItem Sawdust;
    // public BaseItem Sanitizer;

    private bool HasSanitizer;
    private bool HasBuckets;
    private bool HasBag;
    private bool HasSawdust;
    
    void Start()
    {
        // _interact = GetComponent<InteractWithObject>();
        // _inventory = GameObject.FindWithTag("MainInventory").GetComponent<MoreMountains.InventoryEngine.Inventory>();
        //
        // Bucket =  Resources.Load<BaseItem>("Items/Bucket");
        // Bag =  Resources.Load<BaseItem>("Items/Bag");
        // Sawdust =  Resources.Load<BaseItem>("Items/Sawdust");
        // Sanitizer =  Resources.Load<BaseItem>("Items/Sanitizer");
    }

    void OnTriggerEnter(Collider other)
    {
        // if (_inventory.InventoryContains(Bucket.name).Count > 1 && _inventory.InventoryContains(Bag.name).Count > 0 && 
        //     _inventory.InventoryContains(Sawdust.name).Count > 0 && _inventory.InventoryContains(Sanitizer.name).Count > 0)
        // {
        //     Debug.Log("Toilet Time!");
        //     SceneManager.LoadScene("MiniGame", LoadSceneMode.Single);
        // }
    }
}
