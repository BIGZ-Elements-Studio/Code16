using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace oct.InventorySystem
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Inventable")]
    public class InventoryItem : ScriptableObject
    {
        [SerializeField]
        string itemName="Item";
        [SerializeField]
        string descripton;
        [SerializeField]
        Sprite icon;
        [SerializeField]
        GameObject prefeb;
        public string ItemName
        {
            get { return itemName; }
        }
        public string Descripton
        {
            get { return descripton; }
        }
        public Sprite Icon
        {
            get { return icon; }
        }
        public GameObject Prefeb
        {
            get { return prefeb; }
        }
    }




}
