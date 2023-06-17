using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackpackDisplay : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMeshProUGUI;
    public void showItems()
    {
        textMeshProUGUI.text = "";

        foreach (var itemQuantity in PlayerBackpack.Backpack.ItemQuantities)
        {
            string itemName = itemQuantity.Key.ItemName;
            int quantity = itemQuantity.Value;
            textMeshProUGUI.text += itemName + ": " + quantity + "\n";
        }
    }
}
