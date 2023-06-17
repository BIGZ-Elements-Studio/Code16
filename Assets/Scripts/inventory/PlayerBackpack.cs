using UnityEngine;
using System.Collections.Generic;
using oct.InventorySystem;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerBack")]
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

    public static PlayerBackpack Backpack
    {
        get
        {
            if (instance == null)
            {
                PlayerBackpack[] backpackData = Resources.FindObjectsOfTypeAll<PlayerBackpack>();
                if (backpackData.Length != 1)
                {
                    Debug.LogError("背包系统错误，实例数量为：" + backpackData.Length);
                }
                return backpackData[0];
            }
            return instance;
        }
    }

    public Dictionary<InventoryItem, int> ItemQuantities=new Dictionary<InventoryItem, int>();

    private void OnEnable()
    {
        instance = this;
    }

    public static void AddItem(InventoryItem item)
    {
        if (Backpack.ItemQuantities.ContainsKey(item))
        {
            Backpack.ItemQuantities[item]++;
        }
        else
        {
            Backpack.ItemQuantities.Add(item, 1);
        }
    }

    public static void RemoveItem(InventoryItem item)
    {
        if (Backpack.ItemQuantities.ContainsKey(item))
        {
            Backpack.ItemQuantities[item]--;
            if (Backpack.ItemQuantities[item] <= 0)
            {
                Backpack.ItemQuantities.Remove(item);
            }
        }
    }
}