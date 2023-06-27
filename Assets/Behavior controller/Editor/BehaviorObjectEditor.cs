using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using BehaviorControlling;
using static FloatCondition;

[CustomEditor(typeof(BehaviorObject))]
public class BehaviorObjectEditor : Editor
{
    private BehaviorObject behaviorObject;

    private bool showStates = true;
    private bool Ok;
    public override void OnInspectorGUI()
    {
        Ok=true;
        behaviorObject = (BehaviorObject)target;
        DrawVariable();
        // Draw the state behaviors list.
        showStates = EditorGUILayout.Foldout(showStates, "State Behaviors");
        if (showStates)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < behaviorObject.stateBehaviors.Count; i++)
            {
                DrawStateBehavior(behaviorObject.stateBehaviors[i], i);
            }
            EditorGUI.indentLevel--;
        }
        if (GUILayout.Button("Add State Behavior"))
        {
            StateBehavior newStateBehavior = new StateBehavior();
            behaviorObject.stateBehaviors.Add(newStateBehavior);
        }
        // Save changes to the BehaviorObject.
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
        if (!Ok)
        {
            EditorGUILayout.LabelField("Error");
        }
    }



  private void DrawStateBehavior(StateBehavior stateBehavior, int index)
{
    EditorGUILayout.BeginVertical(GUI.skin.box);
        
        // Show the state behavior name and priority.
        stateBehavior.stateName = EditorGUILayout.TextField("名称", stateBehavior.stateName);
        stateBehavior.show = EditorGUILayout.Foldout(stateBehavior.show, "show");
        if (stateBehavior.show)
        {
            stateBehavior.priority = EditorGUILayout.IntField("优先级", stateBehavior.priority);
            stateBehavior.allowBufferedInput = EditorGUILayout.Toggle("允许预输入", stateBehavior.allowBufferedInput);
            stateBehavior.showTags = EditorGUILayout.Foldout(stateBehavior.showTags, "tag");
            if (stateBehavior.showTags)
            {
                EditorGUI.indentLevel++;
                for (int i = 0; i < stateBehavior.tags.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    stateBehavior.tags[i] = (characterState)EditorGUILayout.EnumPopup(stateBehavior.tags[i]);
                    if (GUILayout.Button("-"))
                    {
                        stateBehavior.tags.RemoveAt(index);
                        return;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                if (GUILayout.Button("+"))
                {
                    stateBehavior.tags.Add(characterState.fly);
                    return;
                }
                EditorGUI.indentLevel--;
            }
            stateBehavior.AllowReEnterDuringProcess = EditorGUILayout.Toggle("允许重新进入", stateBehavior.AllowReEnterDuringProcess);
            stateBehavior.effect = EditorGUILayout.Toggle("效果", stateBehavior.effect);
            // Show the state behavior's conditions.
            stateBehavior.showConditions = EditorGUILayout.Foldout(stateBehavior.showConditions, "条件");
            if (stateBehavior.showConditions)
            {
                EditorGUI.indentLevel++;
                for (int i = 0; i < stateBehavior.conditions.Count; i++)
                {
                    DrawCondition(stateBehavior.conditions[i], i);
                }
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("bool条件"))
                {
                    stateBehavior.conditions.Add(new BoolCondition());
                    return;
                }
                if (GUILayout.Button("float条件"))
                {
                    stateBehavior.conditions.Add(new FloatCondition());
                    return;
                }
                GUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }

            // Show the remove state behavior button.
            if (GUILayout.Button("删除"))
            {
                List<StateBehavior> list = new List<StateBehavior>(behaviorObject.stateBehaviors);
                list.RemoveAt(index);
                behaviorObject.stateBehaviors = list;
                return;
            }
        }
    EditorGUILayout.EndVertical();
}
    #region DrawCondition
    private void DrawCondition(Condition condition, int index)
    {
        EditorGUILayout.BeginHorizontal();

        // Show the condition type.
        //EditorGUILayout.LabelField(condition.GetType().Name);
        GUIStyle style = new GUIStyle(EditorStyles.textField);
        if (behaviorObject.ConditionVariables.Exists(x => x.name == condition.conditionName))
        { 
            condition.conditionName = EditorGUILayout.TextField(condition.conditionName);
        }
        else
        {
            style.normal.textColor = Color.red;
            condition.conditionName = EditorGUILayout.TextField(condition.conditionName, style);
            Ok=false;
        }
        
        if (condition is BoolCondition)
        {
            DrawBoolCondition((BoolCondition)condition);
        }
        else if (condition is FloatCondition)
        {
            DrawFloatCondition((FloatCondition)condition);
        }

        // Show the remove condition button.
        if (GUILayout.Button("Remove"))
        {
            StateBehavior stateBehavior = GetStateBehavior(condition);
            if (stateBehavior != null)
            {
                stateBehavior.conditions.RemoveAt(index);
            }
            return;
        }

        EditorGUILayout.EndHorizontal();
    }
    private void DrawBoolCondition(BoolCondition b )
    {
        b.conditionValue = GUILayout.Toggle(b.conditionValue,"");
    }

    private void DrawFloatCondition(FloatCondition f)
    {
        f.comparisonType = (ComparisonType)EditorGUILayout.EnumPopup("", f.comparisonType, GUILayout.Width(90));
        f.conditionValue = EditorGUILayout.FloatField(f.conditionValue);
       
    }

    #endregion
    #region DrawVariable
    
    private void DrawVariable()
    {
        EditorGUILayout.LabelField("变量");

        // Draw existing variables
        for (int i = 0; i < behaviorObject.ConditionVariables.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            
            

            if (behaviorObject.ConditionVariables[i].type == ConditionVariable.ConditionType.FloatVariable)
            {
                DrawFloatVariable(i);
            }
            else if (behaviorObject.ConditionVariables[i].type == ConditionVariable.ConditionType.BoolVariable)
            {
                DrawBoolVariable(i);
            }
            behaviorObject.ConditionVariables[i].name = EditorGUILayout.TextField(behaviorObject.ConditionVariables[i].name);
            if (GUILayout.Button("-"))
            {
                behaviorObject.ConditionVariables.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        
        // Add new variables
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("bool"))
        {
            behaviorObject.ConditionVariables.Add(new BoolVariable());
        }
        if (GUILayout.Button("float"))
        {
            behaviorObject.ConditionVariables.Add(new FloatVariable());
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawFloatVariable(int index)
    {
        EditorGUILayout.LabelField("float");
    }

    private void DrawBoolVariable(int index)
    {
        EditorGUILayout.LabelField("bool");
    }
    #endregion

    private StateBehavior GetStateBehavior(Condition condition)
    {
        for (int i = 0; i < behaviorObject.stateBehaviors.Count; i++)
        {
            if (behaviorObject.stateBehaviors[i].conditions.Contains(condition))
            {
                return behaviorObject.stateBehaviors[i];
            }
        }
        return null;
    }
}