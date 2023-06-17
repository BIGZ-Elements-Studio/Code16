using oct.InventorySystem;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryItemButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    InventoryItem Bitem;

    public InventoryObjectInScene InventoryObjectInScene { get; internal set; }

    internal void Disappear()
    {
        Destroy(gameObject);
    }

    internal void setitem(InventoryItem item)
    {
        Bitem=item; 
        text.text = item.ItemName;
    }
    public void pickUp()
    {
        PlayerBackpack.AddItem(Bitem);
        InventoryObjectInScene.PickUped();
    }
}
