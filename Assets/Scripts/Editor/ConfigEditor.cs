using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Config))]
public class ConfigEditor : Editor
{
    List<SerializedProperty> m_Configs;
    private void OnEnable()
    {
        m_Configs = new List<SerializedProperty>();
        var fields = typeof(Config).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        foreach (var item in fields)
        {
            var s = serializedObject.FindProperty(item.Name);
            if (s != null)
                m_Configs.Add(s);
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        foreach (var item in m_Configs)
            EditorGUILayout.PropertyField(item);
        serializedObject.ApplyModifiedProperties();
    }

}

[CustomPropertyDrawer(typeof(PlayerConfig))]
public class PlayerConfigDrawer : PropertyDrawer
{
    Dictionary<string, SerializedProperty> _propertyDic;
    Dictionary<string, string> _stringDic = new Dictionary<string, string>()
    {
        {"WalkSpeed","移动速度" },
        {"RunSpeed","跑步速度" },
        {"JumpVelocity","跳跃速度" }
    };

    bool exp = true;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 0;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        exp = EditorGUILayout.Foldout(exp, "玩家数据");
        if (exp)
        {
            if (_propertyDic == null)
            {
                _propertyDic = new Dictionary<string, SerializedProperty>();
                foreach (var item in _stringDic)
                {
                    var sp = property.FindPropertyRelative(item.Key);
                    if (sp != null)
                        _propertyDic.Add(item.Value, sp);
                }
            }
            EditorGUILayout.BeginVertical("Box");
            foreach (var item in _propertyDic)
                EditorGUILayout.PropertyField(item.Value, new GUIContent(item.Key));
            EditorGUILayout.EndVertical();
        }
    }
}