using oct.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ PlayerBack")]
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
                PlayerBackpack[] BackPackData = Resources.FindObjectsOfTypeAll<PlayerBackpack>();
                if (BackPackData.Length != 1)
                {
                    Debug.LogError("背包系统错误，实例数量为：" + BackPackData.Length);
                }
                return BackPackData[0];
            }
            return instance;
        }
    }


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
                ItemQuantities.Remove(item);
            }
        }
    }
}