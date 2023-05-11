using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
namespace oct.GuiCreateCode.datastructure
{
    [CreateAssetMenu(menuName = "CharacterBehaviorClass")]
    [System.Serializable]
    public class CoroutineClassTemplate : ScriptableObject
    {
        public string ClassName;
        public List<Field> Parameters;
        public List<CoroutineMethodTemplate> singleCoroutineTemplates;
    }
    [System.Serializable]
    public class Field
    {
        public string Name;
        public type Type;
       public enum type
        {
            GameObject,intVariable,floatVariable,BoolVariable
        }
    }
    [System.Serializable]
    public class CoroutineMethodTemplate
    {
        bool shown;
        public string MethodName;
        public List<Interval> interval;
        public List<ActionInInterval> actionInIntervals;

    }
    [System.Serializable]
    public class Interval
    {
        public float time;
        public bool useUnfixedTime;
    }
    [System.Serializable]
    public class ActionInInterval
    {
        public List<ActionForInterval> actionForIntervals;
        public ActionInInterval()
        {
            actionForIntervals=new List<ActionForInterval>();
        }
    }
    [System.Serializable]
    public class ActionForInterval
    {
        public string content;
        public ActionType Type;
        public enum ActionType
        {
            changeAnimation, changeValue, Instantiate
        }
    }
}
namespace oct.GuiCreateCode
{
    using oct.GuiCreateCode.datastructure;
    using static oct.GuiCreateCode.datastructure.ActionForInterval;

    public class CharacterCoroutineClassWriter
    {
      static  string classConetent;
        public static void write(CoroutineClassTemplate template)
        {
            
            classConetent += writeStart(template.ClassName);
            foreach (Field f in template.Parameters)
            {
                classConetent += writeField(f);
            }
            foreach (CoroutineMethodTemplate CoroutineTemplate in template.singleCoroutineTemplates)
            {
                classConetent += writeMethod(CoroutineTemplate);
            }

            classConetent += writeEnd();
            Debug.Log(classConetent);
            classConetent = "";
        }
        
        private static string writeField(Field f)
        {
            string s;
            switch (f.Type)
            {
                case Field.type.BoolVariable:
                    s = "bool";
                    break;
                case Field.type.intVariable:
                    s = "int";
                    break;
                case Field.type.floatVariable:
                    s = "float";

                    break;
                case Field.type.GameObject:
                    s = "GameObject";
                    break;
                default:
                    s = "";
                    break;
            }

            return $"[SerializeField]\r\n {s}  {f.Name};\n";
        }

        private static string writeMethod(CoroutineMethodTemplate coroutineTemplate)
        {
            string MethodContent = "public IEnumerator " + coroutineTemplate.MethodName + "(){\n";
            MethodContent += WriteCommand(coroutineTemplate.actionInIntervals[0]);
            for (int i = 0; i < coroutineTemplate.interval.Count; i++)
            {
                MethodContent += WriteInterval(coroutineTemplate.interval[i]);
                MethodContent += WriteCommand(coroutineTemplate.actionInIntervals[i+1]);
            }
           // MethodContent += WriteCommand(coroutineTemplate.actionInIntervals[coroutineTemplate.interval.Count + 1]);
            return MethodContent+ "\n}\n\n";
        }

        private static string WriteInterval(Interval interval)
        {
            if (interval.time!=0) {
                if (interval.useUnfixedTime)
                {
                    return "\tyield return new WaitForSecondsRealtime(" + interval.time + "f);\n";

                }
                else
                {
                    return "\tyield return new WaitForSeconds(" + interval.time + "f);\n";
                }
            }
            return "";
        }

        private static string WriteCommand(ActionInInterval ActionForInterval)
        {
            foreach(ActionForInterval Action in ActionForInterval.actionForIntervals)
            {
                switch (Action.Type)
                {
                    case ActionType.changeAnimation:
                        return "//changeAnimation("+ Action.content+")\n";
                    case ActionType.changeValue:
                        return  Action.content + ";\n";
                    case ActionType.Instantiate:
                        return "//Instantiate(" + Action.content + ")\n";
                }
            }
            return "";
        }

        private static string writeEnd()
        {
            return "\n}\n}";
        }

        private static string writeStart(string className)
        {
            string startContent = "";
            startContent += "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\nnamespace oct.generatedBehavior\n{\n\n";
            startContent += $"public class {className} : MonoBehaviour\n";
            startContent += "{\n\n";
            startContent += "[SerializeField]\r\n    PlayerController playerController;\n ";
            return startContent;
        }
    }

}



