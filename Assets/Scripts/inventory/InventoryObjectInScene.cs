using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace oct.InventorySystem {
    public class InventoryObjectInScene : MonoBehaviour
    {
        [SerializeField]
        MeshRenderer graphic;
        public InventoryItem type;
        int displayedId = -1;

        [SerializeField]
        GameObject Obj;
        private void Start()
        {
            graphic.material = type.material;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerPickUp>() != null)
            {
                displayedId=InventoryItemList.ShowItem(type,this);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerPickUp>() != null)
            {
                InventoryItemList.RemoveItem(displayedId);
                displayedId = -1;
            }
        }

        internal void PickUped()
        {
            InventoryItemList.RemoveItem(displayedId);
            Destroy(Obj);
        }
    }
}