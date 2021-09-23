using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// holds an array of items and an associated array of amounts
/// </summary>
public class OurInventory : MonoBehaviour
{
    [SerializeField] private byte capacity = 9;
    private byte heldAmount;
    [SerializeField] private Item[] items;
    [SerializeField] private byte[] amounts;

    private void Awake()
    {
        heldAmount = 0;
        items = new Item[capacity];
        amounts = new byte[capacity];
        for (int i = 0; i < capacity; i+=1)
        {
            amounts[i] = 0;
        }
    }

    public bool AddItem(Item i, byte amt)
    {
        int k = HasItem(i,1);
        if (k != -1)
        {
            amounts[k] += amt;
        }
        else if (heldAmount == capacity)
        {
            return false;
        }
        else
        {
            k = 0;
            while (amounts[k] != 0) k+=1;
            items[k] = i;
            amounts[k] = amt;
        }

        return true;
    }

    public int HasItem(Item i, int amt)
    {
        for (int j = 0; j < capacity; j+=1)
            if (amounts[j] != 0)
                if (i.ID == items[j].ID)
                    if(amounts[j] >= amt) return j;
        
        return -1;
    }

    public byte GetAmount(Item i)
    {
        int k = HasItem(i,1);
        if (k != -1) return amounts[k];
        
        return 0;
    }

    public Item RemoveItem(Item i, byte amt)
    {
        int k = HasItem(i, amt);
        if (k != -1)
        {
            amounts[k] -= amt;
            if (amounts[k] <= 0)
            {
                amounts[k] = 0;
                items[k] = null;
            }
        }

        return i;
    }
    
    public Item RemoveAll(Item i)
    {
        return RemoveItem(i, GetAmount(i));
    }
        
    public byte GetCapacity()
    {
        return capacity;
    }

    public Item[] GetItems()
    {
        return items;
    }

    public byte[] GetAmounts()
    {
        return amounts;
    }

    public bool IsEmpty()
    {
        for (int j = 0; j < capacity; j += 1)
        {
            if (amounts[j] != 0) return false;
        }
        return true;
    }
    

}
