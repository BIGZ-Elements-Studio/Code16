using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using oct.GuiCreateCode.datastructure;
using System;
using System.Security.Cryptography;
using System.Data.Common;
namespace oct.GuiCreateCode
{
    public class CoroutineTemplateEditorWindow : EditorWindow
    {
        private CoroutineClassTemplate coroutineTemplate;
        public bool canPrint;
        [MenuItem("Window/CoroutineTemplateEditorWindow")]
        public static void ShowWindow()
        {
            GetWindow(typeof(CoroutineTemplateEditorWindow));
        }

        private void OnGUI()
        {
            canPrint = true;
            GUILayout.Label("Coroutine Template Editor", EditorStyles.boldLabel);
            coroutineTemplate = EditorGUILayout.ObjectField("Coroutine Template", coroutineTemplate, typeof(CoroutineClassTemplate), false) as CoroutineClassTemplate;
            if (coroutineTemplate != null)
            {
                ShowTemplate(coroutineTemplate);
            }
            if (canPrint&&GUILayout.Button("print"))
            {
                CharacterCoroutineClassWriter.write(coroutineTemplate);
            }

        }

        void ShowTemplate(CoroutineClassTemplate coroutineTemplate)
        {
            coroutineTemplate.ClassName = EditorGUILayout.TextField("Class Name", coroutineTemplate.ClassName);
            if (coroutineTemplate.ClassName == "")
            {
                canPrint = false;
            }
            // Show all the fields using the showField function
            GUILayout.Space(10);
            GUILayout.Label("Fields",EditorStyles.boldLabel);
            EditorGUI.indentLevel+=2;
            EditorGUILayout.BeginVertical(GUI.skin.box);
            foreach (Field parameter in coroutineTemplate.Parameters)
            {
                showField(parameter);
            }
            GUILayout.BeginHorizontal();
            GUILayout.Space(EditorGUI.indentLevel * 20);
            
            if (GUILayout.Button("Add Parameter"))
            {
                coroutineTemplate.Parameters.Add(new Field());
            }
            EditorGUILayout.EndVertical();
            GUILayout.EndHorizontal();
            EditorGUI.indentLevel-=2;
            // Show all the methods using the showMethod function
            GUILayout.Space(10);
            EditorGUI.indentLevel++;
            GUILayout.Label("Methods", EditorStyles.boldLabel);
            foreach (CoroutineMethodTemplate singleCoroutineTemplate in coroutineTemplate.singleCoroutineTemplates)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                singleCoroutineTemplate.MethodName = EditorGUILayout.TextField("Method Name:", singleCoroutineTemplate.MethodName);
                if (singleCoroutineTemplate.MethodName == "")
                {
                    canPrint = false;
                }
                EditorGUI.indentLevel++;
                showMethod(singleCoroutineTemplate);
                EditorGUI.indentLevel--;
                GUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;
            void showMethod(CoroutineMethodTemplate singleCoroutineTemplate)
            {
                // Show the method name

                if (singleCoroutineTemplate.actionInIntervals.Count > 0)
                {
                    showAction(singleCoroutineTemplate.actionInIntervals[0]);
                }
                // Show all the actionInIntervals and intervals using the showAction and showInterval functions
                for (int i = 0; i < singleCoroutineTemplate.interval.Count; i++)
                {
                    // Show the Interval using the showInterval function
                    GUILayout.BeginHorizontal();
                    showInterval(singleCoroutineTemplate.interval[i]);
                    if ( GUILayout.Button("-"))
                    {
                        singleCoroutineTemplate.interval.RemoveAt(i);
                        singleCoroutineTemplate.actionInIntervals.RemoveAt(i+1);
                        GUILayout.EndHorizontal();
                        break;
                    }
                    GUILayout.EndHorizontal();
                    // Show the action in Interval using the showAction function
                    showAction(singleCoroutineTemplate.actionInIntervals[i + 1]);


                }

                // Add button to add one Interval and one action Interval
                GUILayout.BeginHorizontal();
                GUILayout.Space(EditorGUI.indentLevel * 20);
                if (GUILayout.Button("Add Interval!!!!!"))
                {
                    if (singleCoroutineTemplate.actionInIntervals.Count==0)
                    {
                        singleCoroutineTemplate.actionInIntervals.Add(new ActionInInterval());
                    }
                    singleCoroutineTemplate.interval.Add(new Interval());
                    singleCoroutineTemplate.actionInIntervals.Add(new ActionInInterval());
                }
                GUILayout.EndHorizontal();
            }
            void showAction(ActionInInterval actionInInterval)
            {

                // Show the type and the string field
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Action:");
                if (GUILayout.Button("+"))
                {
                    actionInInterval.actionForIntervals.Add(new ActionForInterval());
                }
                GUILayout.EndHorizontal();
                EditorGUI.indentLevel++;
                for (int i = 0; i < actionInInterval.actionForIntervals.Count; i++)
                {
                    ActionForInterval action = actionInInterval.actionForIntervals[i];
                    GUILayout.BeginHorizontal();
                    action.Type = (ActionForInterval.ActionType)EditorGUILayout.EnumPopup(action.Type);
                    action.content = EditorGUILayout.TextField(action.content);
                    if (action.content=="")
                    {
                        canPrint=false;
                    }
                    if (GUILayout.Button("-"))
                    {
                        actionInInterval.actionForIntervals.RemoveAt(i);
                        i--;
                    }
                    GUILayout.EndHorizontal();
                   
                }
                EditorGUI.indentLevel--;
            }

            void showInterval(Interval interval)
            {
                GUILayout.BeginHorizontal();
                interval.time = EditorGUILayout.FloatField("Time:", interval.time);
                GUILayout.Space(100);
                interval.useUnfixedTime = EditorGUILayout.Toggle("Unscaled Time:", interval.useUnfixedTime);
                GUILayout.EndHorizontal();
            }



            void showField(Field parameter)
            {
                EditorGUILayout.BeginHorizontal();
                // Show variable type
                parameter.Type = (Field.type)EditorGUILayout.EnumPopup(parameter.Type);
                parameter.Name = EditorGUILayout.TextField(parameter.Name);

                if (parameter.Name=="")
                {
                    canPrint = false;
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}

