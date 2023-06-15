using oct.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackpack : ScriptableObject
{
    private static PlayerBackpack instance;

    public static PlayerBackpack Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<PlayerBackpack>("BackpackData");
            }
            return instance;
        }
    }

    public List<InventoryItem> Items;
    public Dictionary<InventoryItem, int> ItemQuantities;

    private void OnEnable()
    {
        instance = this;
    }

    public void AddItem(InventoryItem item)
    {
        if (ItemQuantities.ContainsKey(item))
        {
            ItemQuantities[item]++;
        }
        else
        {
            Items.Add(item);
            ItemQuantities.Add(item, 1);
        }
    }

    public void RemoveItem(InventoryItem item)
    {
        if (ItemQuantities.ContainsKey(item))
        {
            ItemQuantities[item]--;
            if (ItemQuantities[item] <= 0)
            {
                Items.Remove(item);
                ItemQuantities.Remove(item);
            }
        }
    }
}