using UnityEngine;
using UnityEditor;
using oct.ObjectBehaviors;

[CustomEditor(typeof(PlayerAttribute))]
public class PlayerAttributeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("change"))
        {
            ((PlayerAttribute)target).in2d = !((PlayerAttribute)target).in2d;
        }
    }
}