using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



/// <summary>
/// Class is used for adding behavior to inventory manipulations (such as logging or checking conditions)
/// All inventory manipulations that need to be logged should go through this 
/// </summary>
public class InventoryHelper : MonoBehaviour
{
    public UnityEvent CheckOnAdd;
    [SerializeField] private OurInventory _inventory;
    
    public Item[] holderItems;
    public int[] holderAmts;
    public InvButtonFlash flash;

    private LogToServer logger; 
    public void Start()
    {
        for (int i = 0; i < holderItems.Length; i++)
            AddItem(holderItems[i], holderAmts[i]);
        if (holderItems.Length > 0) { SavedData.addInv = true; }
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
    }
    
    public void AddItem(Item item, int amt)
    {
        //Logger.Instance.Log("Picked up: "+item.name);
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        logger.sendToLog("Picked up " + item.name,"ACTION");
        _inventory.AddItem(item, (byte)amt);
        flash.ThrobOn();
        CheckOnAdd.Invoke();
    }

    public bool HasItem(Item item, int amt)
    {
        return _inventory.GetAmount(item) >= amt;
    }

    public bool HasItem(Item[] items, int[] amts)
    {
        for (int i = 0; i < items.Length; i++)
            if (!HasItem(items[i], amts[i]))
                return false;
        return true;
    }

    public void RemoveItem(Item item)
    {
        _inventory.RemoveItem(item, 1);
    }

    public void RemoveItem(Item item, int amt)
    {
        _inventory.RemoveItem(item, (byte)amt);
    }
    
    public void RemoveItem(Item[] items, int[] amts)
    {
        for (int i = 0; i < items.Length; i++)
            RemoveItem(items[i], amts[i]);
    }

    public byte GetCapacity()
    {
        return _inventory.GetCapacity();
    }

    public Item[] GetItems()
    {
        return _inventory.GetItems();
    }

    public byte[] GetAmounts()
    {
        return _inventory.GetAmounts();
    }

    public bool CheckEmpty()
    {
        return _inventory.IsEmpty();
    }
}
