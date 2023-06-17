using NUnit.Framework.Interfaces;
using oct.InventorySystem;
using UnityEditor;
using UnityEngine;

public class ItemDataWindow : EditorWindow
{
    private Vector2 scrollPosition;

    [MenuItem("Window/Item Data Window")]
    public static void ShowWindow()
    {
        GetWindow<ItemDataWindow>("Item Data");
    }

    private void OnGUI()
    {
        GUILayout.Label("Item Data", EditorStyles.boldLabel);
        // Begin the scroll view
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        // Find all ItemData assets in the project
        string[] itemDataGuids = AssetDatabase.FindAssets("t:InventoryItem");
        foreach (string guid in itemDataGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            InventoryItem itemData = AssetDatabase.LoadAssetAtPath<InventoryItem>(path);

            if (itemData != null)
            {
                // Display the item's name as a clickable label
                if (GUILayout.Button(itemData.ItemName, EditorStyles.label))
                {
                    // Open the corresponding ItemData asset file
                    Selection.activeObject = itemData;
                    EditorGUIUtility.PingObject(itemData);
                }
            }
        }
        EditorGUILayout.EndScrollView();
    }
}