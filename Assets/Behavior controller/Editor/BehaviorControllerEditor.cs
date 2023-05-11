using oct.generatedBehavior;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System.Collections;
using System;
using codeTesting;

[CustomEditor(typeof(BehaviorController))]
public class BehaviorControllerEditor : Editor
{
    bool showBehaviorList;
    bool showConditionVariable;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        var controller = (BehaviorController)target;
        var behaviorObject = controller.behaviorObject;
        
        if (behaviorObject != null)
        {
            while (controller.varibleValues.Count < behaviorObject.ConditionVariables.Count)
            {
                controller.varibleValues.Add(0);
            }
            EditorGUI.indentLevel++;
            showBehaviorList = EditorGUILayout.Foldout(showBehaviorList, "BehaviorList");
            
                while (controller.CoroutineList.Count != controller.behaviorObject.stateBehaviors.Count)
                {
                    if (controller.CoroutineList.Count < controller.behaviorObject.stateBehaviors.Count)
                    {
                        controller.CoroutineList.Add(null);
                    }
                    else
                    {
                        controller.CoroutineList.RemoveAt(controller.CoroutineList.Count - 1);
                    }
                }
            if (showBehaviorList)
            {
                MethodInfo[] coroutineMethods = controller.targetCode.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                  .Where(m => m.ReturnType == typeof(IEnumerator)).ToArray();

                // Loop over the coroutineList and display a dropdown list of coroutine methods for each item
                for (int i = 0; i < controller.CoroutineList.Count; i++)
                {

                    int selectedIndex;
                    if (controller.CoroutineList[i] != null)
                    {
                        selectedIndex = Array.FindIndex(coroutineMethods, m => m.Name == controller.CoroutineList[i]);
                    }
                    else
                    {
                        selectedIndex = 0;
                    }

                    // Display a dropdown list of coroutine methods, and update the selected index if changed
                    //   selectedIndex = EditorGUILayout.Popup("Coroutine Method", selectedIndex, coroutineMethods.Select(m => m.Name).ToArray());
                    selectedIndex = EditorGUILayout.Popup(i+" "+controller.behaviorObject.stateBehaviors[i].stateName, selectedIndex, coroutineMethods.Select(m => m.Name).ToArray());

                    if (selectedIndex >= 0 && selectedIndex < coroutineMethods.Length)
                    {
                        // Create a delegate for the selected coroutine method and add it to the coroutineList
                        var method = coroutineMethods[selectedIndex];
                        controller.CoroutineList[i] = method.Name;
                    }
                }
                EditorGUI.indentLevel--;
            }
            showConditionVariable = EditorGUILayout.Foldout(showConditionVariable, "ConditionVariable");
            if (showConditionVariable)
            {
                for (int i = 0; i < behaviorObject.ConditionVariables.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    // Display the behavior field as an object field where we can drag a MonoBehaviour
                    if (behaviorObject.ConditionVariables[i] is FloatVariable)
                    {
                        float j = EditorGUILayout.FloatField(behaviorObject.ConditionVariables[i].name, controller.varibleValues[i]);
                        controller.setFloatVariable(behaviorObject.ConditionVariables[i].name, j);
                    }
                    else if (behaviorObject.ConditionVariables[i] is BoolVariable)
                    {
                        bool j = EditorGUILayout.Toggle(behaviorObject.ConditionVariables[i].name, controller.varibleValues[i]==1);
                        controller.setBoolVariable(behaviorObject.ConditionVariables[i].name, j);
                    }

                    EditorGUILayout.EndHorizontal();



                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}