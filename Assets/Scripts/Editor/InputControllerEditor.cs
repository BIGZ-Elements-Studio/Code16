using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputController))]
public class InputControllerEditor : Editor
{
    private Dictionary<string, SerializedProperty> _propertyDic = new Dictionary<string, SerializedProperty>();
    private Dictionary<string, string> _stringDic = new Dictionary<string, string>()
    {
        {"_directionAxis","����" },
        {"_space","��Ծ" },
        {"_Fire1","����1" },
        {"_Fire2","����2" },
        {"_Fire3","����3" }
    };

    void OnEnable()
    {
        foreach (var item in _stringDic)
        {
            var property = serializedObject.FindProperty(item.Key);
            if (property != null)
                _propertyDic.Add(item.Value, property);
        }
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        serializedObject.Update();
        foreach (var item in _propertyDic)
            EditorGUILayout.PropertyField(item.Value, new GUIContent(item.Key));
        serializedObject.ApplyModifiedProperties();
    }
}
