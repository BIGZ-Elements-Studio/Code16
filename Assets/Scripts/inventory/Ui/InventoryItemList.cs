using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace oct.InventorySystem
{
    public class InventoryItemList : MonoBehaviour
    {
        public static InventoryItemList Instance
        {
            get { return FindObjectOfType<InventoryItemList>(); }
        }
        [SerializeField]
        private List<int> assignedId = new List<int>();

        [SerializeField]
        private Dictionary<int, GameObject> items = new Dictionary<int, GameObject>();

        [SerializeField]
        private GameObject prefab;

        public static int ShowItem(InventoryItem item, InventoryObjectInScene caller)
        {
            return Instance.Show(item, caller);
        }
        int Show(InventoryItem item, InventoryObjectInScene caller)
        {
            // Instantiate a prefab under this gameObject
            GameObject newItemObject = Instantiate(prefab, transform);

            // Set the InventoryItemButton.item in that GameObject to item
            InventoryItemButton itemButton = newItemObject.GetComponent<InventoryItemButton>();
            itemButton.setitem(item);
            itemButton.InventoryObjectInScene = caller;
            // Find an empty smallest id, put the id and GameObject to items
            int newId = FindSmallestEmptyId();
            items.Add(newId, newItemObject);

            // Add that id to the assignedId list
            assignedId.Add(newId);

            // Return the id
            return newId;
        }
        public static void RemoveItem(int id)
        {
            if (id!=-1) {
                Instance.remove(id);
            }
        }
        private void remove(int id)
        {
            // Find the GameObject with that id
            if (items.TryGetValue(id, out GameObject itemObject))
            {
                // Call InventoryItemButton.Disappear
                InventoryItemButton itemButton = itemObject.GetComponent<InventoryItemButton>();
                itemButton.Disappear();

                // Remove that pair from items
                items.Remove(id);

                // Remove id from assignedId
                assignedId.Remove(id);
            }
            else
            {
                Debug.LogError("Item with id " + id + " does not exist in the inventory.");
            }
        }
        private int FindSmallestEmptyId()
        {
            int newId = 1;
            while (assignedId.Contains(newId))
            {
                newId++;
            }
            return newId;
        }
    }
}