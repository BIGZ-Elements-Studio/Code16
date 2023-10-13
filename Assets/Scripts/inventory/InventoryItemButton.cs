using oct.InventorySystem;
using TMPro;
using UnityEngine;

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
