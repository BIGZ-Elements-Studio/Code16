using oct.InventorySystem;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InventoryItem))]
public class InventoryItemPropertyDrawer : PropertyDrawer
{
    private Vector2 scrollPosition = Vector2.zero;
    private const float windowWidth = 200f;
    private const float windowHeight = 300f;
    bool useDefaultDrawer = false;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        useDefaultDrawer = property.serializedObject.targetObject.GetType() == typeof(InventoryItems);

        if (useDefaultDrawer)
        {
            // Apply default serialization
            EditorGUI.PropertyField(position, property, label, true);
        }
        else
        {
            customDisplay(position, property, label);
        }
    }
    string searchQuery;
    void customDisplay(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Retrieve the target object
        var targetObject = property.serializedObject.targetObject as InventoryItems;

        SerializedProperty nameProperty = property.FindPropertyRelative("name");
        SerializedProperty descriptionProperty = property.FindPropertyRelative("description");

        // Display the current name of the item
        Rect nameRect = new Rect(position.x, position.y, position.width, 20f);
        EditorGUI.LabelField(nameRect, "当前物品:  " + nameProperty.stringValue);

        // Create a search bar to filter the items by name
        Rect searchRect = new Rect(position.x, position.y + 25f, position.width, 20f);
        searchQuery = EditorGUI.TextField(searchRect, searchQuery);
        // Create a scroll view for the inventory item buttons
        Rect scrollViewRect = new Rect(position.x, position.y + 50f, position.width, position.height - 50f);
        List<InventoryItem> filteredItems;
        if (searchQuery != null && searchQuery != "")
        {
            filteredItems = InventoryItems.Items.Where(item => item.Name.Contains(searchQuery)).ToList();
        }
        else
        {
            filteredItems = InventoryItems.Items;
        }
        Rect viewRect = new Rect(0f, 0f, scrollViewRect.width - 20f, filteredItems.Count * 20f);


        scrollPosition = GUI.BeginScrollView(scrollViewRect, scrollPosition, viewRect);


        // Iterate through the items and display buttons for each
        for (int i = 0; i < filteredItems.Count; i++)
        {
            InventoryItem item = filteredItems[i];

            // Filter the items based on the search query
            if (!string.IsNullOrEmpty(searchQuery) && !item.Name.Contains(searchQuery))
                continue;

            // Calculate the position for the current button
            Rect buttonRect = new Rect(viewRect.x, i * 20f, viewRect.width, 20f);

            // Draw a button for the item's name
            if (GUI.Button(buttonRect, item.Name))
            {
                // When the button is pressed, set the value of the properties
                nameProperty.stringValue = item.Name;
                descriptionProperty.stringValue = item.Description;
            }
        }

        GUI.EndScrollView();

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (useDefaultDrawer)
        {
            return EditorGUI.GetPropertyHeight(property);
        }
        else
        {
            float baseHeight = EditorGUI.GetPropertyHeight(property, label);

            // Calculate the additional height for the scroll view and search bar
            float additionalHeight = 50f;

            return baseHeight + additionalHeight;
        }
    }
}
