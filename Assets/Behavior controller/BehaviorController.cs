//����ű�ֻ�����жϸ�ִ���ĸ�״̬���ҵ��ã�ʹ�÷�ʽӦ������һ���ű���ȡ���룬���ø���/���������͵�����������ֵ��Ȼ��ִ��״̬���߼���animator��࣬���Ǹ�Ϊ��
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using oct.generatedBehavior;
using System.Reflection;
using System;
namespace codeTesting
{
    public class BehaviorController : Controller
    {
        [SerializeField]
        float bufferedInputTime;
        public bool LockState {get{ return _LockState; } set { _LockState = value;if (!value) { stopLock(); } } }
        [SerializeField]
        bool _LockState;


        Coroutine bufferInputProcess;

        public int storedState { get { return _storedState; }set { if (_storedState!= storedState) { bufferInputProcess = StartCoroutine(bufferInputed()); } _storedState = value; } }
        [SerializeField]
       public int _storedState;


        public BehaviorObject behaviorObject;
        public int currentState;
        public delegate IEnumerator MyDelegate();

        [SerializeField]
        PlayerAttribute PlayerController;
        //���԰�����ĳ�string����deleagate����ʲô�ģ�������method�Ͷ�Ӧ��״̬�󶨾���
        public CharacterControlCoroutine targetCode;
        [HideInInspector]
        public List<string> CoroutineList = new List<string>();
        [HideInInspector]
        public List<float> varibleValues = new List<float>();

        //����������ʱ�����
        private void stopLock()
        {
            if (storedState == -1)
            {
                CheakCondition();
            }
            else
            {
                int stateBehavior = -1;
                for (int i = 0; i < behaviorObject.stateBehaviors.Count; i++)
                {
                    bool allConditionsMet = true;
                    foreach (var condition in behaviorObject.stateBehaviors[i].conditions)
                    {
                        if (!CheckCondition(condition))
                        {
                            allConditionsMet = false;
                            break;
                        }
                    }
                    if (allConditionsMet && (stateBehavior == -1 || behaviorObject.stateBehaviors[i].priority > behaviorObject.stateBehaviors[stateBehavior].priority))
                    {
                        stateBehavior = i;
                    }
                }
                if (behaviorObject.stateBehaviors[stateBehavior].priority< behaviorObject.stateBehaviors[storedState].priority)
                {
                    changtoState(storedState);
                }
                else
                {
                    changtoState(stateBehavior);
                }
                
                _storedState = -1;
            }
        }
        private void Awake()
        {
            while (varibleValues.Count< behaviorObject.ConditionVariables.Count)
            {
                varibleValues.Add(0);   

            }

        }
        private void changtoState(int index)
        {
            
            Func<IEnumerator> coroutineDelegate = (Func<IEnumerator>)Delegate.CreateDelegate(typeof(Func<IEnumerator>), targetCode, CoroutineList[index]);
            if (behaviorObject.stateBehaviors[index].AllowReEnterDuringProcess)
            {
                PlayerController.SetState(coroutineDelegate);
            }
            else
            {
                PlayerController.ChangeState(coroutineDelegate);
            }
        }

        private IEnumerator bufferInputed()
        {
            Debug.Log("called");
            if(bufferInputProcess!= null)
            {
                StopCoroutine(bufferInputProcess);
            }
            yield return new WaitForSecondsRealtime(bufferedInputTime);
            _storedState = -1;
        }

        private void Start()
        {
            targetCode.LockState += () => LockState=true;
            targetCode.UnLockState += () => LockState = false;
        }
        // ����������л�״̬
        public void CheakCondition()
        {
            // �ҵ����ȼ���ߵķ�������������״̬
            int stateBehavior = -1;
            for (int i = 0; i < behaviorObject.stateBehaviors.Count; i++)
            {
                bool allConditionsMet = true;
                foreach (var condition in behaviorObject.stateBehaviors[i].conditions)
                {
                    if (!CheckCondition(condition))
                    {
                        allConditionsMet = false;
                        break;
                    }
                }
                if (allConditionsMet && (stateBehavior == -1 || behaviorObject.stateBehaviors[i].priority > behaviorObject.stateBehaviors[stateBehavior].priority))
                {
                    stateBehavior = i;
                }
            }
            currentState = stateBehavior;
            // ����ҵ���״̬��ִ�����Ӧ����Ϊ
            //������һ���ʵ�ַ�ʽ��ƥ�����̵�addstate��������ģʽ
            if (stateBehavior != -1)
            {
                
                if (LockState) {
                    if (behaviorObject.stateBehaviors[stateBehavior].priority> behaviorObject.stateBehaviors[currentState].priority)
                    {
                        _LockState = false;
                        changtoState(stateBehavior);
                    }
                    else if (behaviorObject.stateBehaviors[stateBehavior].allowBufferedInput && storedState == -1)
                    {
                        storedState = stateBehavior;
                    }
                }
                else
                {
                    changtoState(stateBehavior);
                }
            }

        }

        // ���ø��������͵�����������ֵ
        public bool setFloatVariable(string VariableName, float result)
        {
            if (enabled) {
                ConditionVariable f = behaviorObject.ConditionVariables.FirstOrDefault(x => x.name == VariableName);
                if (f != null && f is FloatVariable)
                {
                    int Index = behaviorObject.ConditionVariables.IndexOf(f);
                    varibleValues[Index] = result;
                    CheakCondition();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        // ���ò������͵�����������ֵ
        public bool setBoolVariable(string VariableName, bool result)
        {
            if (enabled)
            {
                ConditionVariable f = behaviorObject.ConditionVariables.FirstOrDefault(x => x.name == VariableName);
                if (f != null && f is BoolVariable)
                {
                    int Index = behaviorObject.ConditionVariables.IndexOf(f);
                    //   ((BoolVariable)f).value = result;
                    if (result)
                    {
                        varibleValues[Index] = 1;
                    }
                    else
                    {
                        varibleValues[Index] = 0;
                    }

                    CheakCondition();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        // ��������Ƿ�����
        private bool CheckCondition(Condition condition)
        {
            if (condition is BoolCondition)
            {
                var boolCondition = (BoolCondition)condition;
                BoolVariable BoolParam = (BoolVariable)behaviorObject.ConditionVariables.FirstOrDefault(x => x.name == boolCondition.conditionName);
                int boolIndex = behaviorObject.ConditionVariables.IndexOf(behaviorObject.ConditionVariables.FirstOrDefault(x => x.name == boolCondition.conditionName));
                return boolCondition.CheckCondition(varibleValues[boolIndex]==1);
            }
            else if (condition is FloatCondition)
            {
                var floatCondition = (FloatCondition)condition;
                FloatVariable floatParam = (FloatVariable)behaviorObject.ConditionVariables.FirstOrDefault(x => x.name == floatCondition.conditionName);
                int boolIndex = behaviorObject.ConditionVariables.IndexOf(behaviorObject.ConditionVariables.FirstOrDefault(x => x.name == floatCondition.conditionName));
                return floatCondition.CheckCondition(varibleValues[boolIndex]);
            }
            else
            {
                return false;
            }
        }

    }
}