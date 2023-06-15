using System.Collections.Generic;
using UnityEngine;
namespace oct.InventorySystem
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Inventable")]
    public class InventoryItems : ScriptableObject
    {
        private static List<InventoryItem> instance;
        [SerializeField]
        private List<InventoryItem> ObjectList;
        private void OnEnable()
        {
            instance = this.ObjectList;
        }
        public static List<InventoryItem> Items
        {
            get
            {
                if (instance == null)
                {
                    InventoryItems[] AllinventoryItems;
                    AllinventoryItems = Resources.FindObjectsOfTypeAll<InventoryItems>();
                    if (AllinventoryItems.Length != 1)
                    {
                        Debug.LogError("背包系统错误，实例数量为：" + AllinventoryItems.Length);
                    }
                    return AllinventoryItems[0].ObjectList;
                }
                return instance;
            }
        }

        public InventoryItems()
        {
            ObjectList=new List<InventoryItem>();
            ObjectList.Add(new InventoryItem());

        }
    }
    [System.Serializable]
    public class InventoryItem 
    {

        public InventoryItem()
        {
            name = "";
            description = "";
        }
        [SerializeField]
        private string name;

        public string Name
        {
            get { return name; }
        }
        [SerializeField]
        private string description;

        public string Description
        {
            get { return description; }
        }
        [SerializeField]
        private Sprite icon;

        public Sprite Icon
        {
            get { return icon; }
        }
    }



}
