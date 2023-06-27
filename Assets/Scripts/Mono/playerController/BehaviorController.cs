//����ű�ֻ�����жϸ�ִ���ĸ�״̬���ҵ��ã�ʹ�÷�ʽӦ������һ���ű���ȡ���룬���ø���/���������͵�����������ֵ��Ȼ��ִ��״̬���߼���animator��࣬���Ǹ�Ϊ��
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using oct.ObjectBehaviors;
using System.Reflection;
using System;
using Unity.VisualScripting;
using UnityEngine.Events;

namespace BehaviorControlling
{

    public class BehaviorController : Controller
    {
        public List<characterState> characterStates = new List<characterState>();

        [SerializeField]
        float bufferedInputTime;
        public bool LockState {get{ return _LockState; } set { _LockState = value;if (!value) { stopLock(); } } }
        [SerializeField]
        bool _LockState;
        public UnityEvent<List<characterState>> statechange;

        Coroutine bufferInputProcess;

        public int storedState { get { return _storedState; }set { if (_storedState!= storedState) { bufferInputProcess = StartCoroutine(bufferInputed()); } _storedState = value; } }
        [SerializeField]
       public int _storedState;


        public BehaviorObject behaviorObject;
        public int currentState;
        public delegate IEnumerator MyDelegate();

        [SerializeField]
     //   PlayerAttribute PlayerController;
        //���԰�����ĳ�string����deleagate����ʲô�ģ�������method�Ͷ�Ӧ��״̬�󶨾���
        public MoveableControlCoroutine targetCode;
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
        private void Start()
        {
            targetCode.LockState += () => LockState=true;
            targetCode.UnLockState += () => LockState = false;
            CheakCondition();
        }

        private void OnEnable()
        {
            CheakCondition();
        }
        private void changtoState(int index)
        {
            currentState = index;
            if (CoroutineList[index] != null && CoroutineList[index]!="") {
                Func<IEnumerator> coroutineDelegate = (Func<IEnumerator>)Delegate.CreateDelegate(typeof(Func<IEnumerator>), targetCode, CoroutineList[index]);
                characterStates = behaviorObject.stateBehaviors[index].tags;
                statechange.Invoke(behaviorObject.stateBehaviors[index].tags);
                if (behaviorObject.stateBehaviors[index].effect)
                {
                    StartCoroutine(coroutineDelegate());
                }
                else if (behaviorObject.stateBehaviors[index].AllowReEnterDuringProcess)
                {
                    SetState(coroutineDelegate);
                }
                else
                {
                    ChangeState(coroutineDelegate);
                }
            }
        }

        private IEnumerator bufferInputed()
        {
            if (bufferInputProcess!= null)
            {
                StopCoroutine(bufferInputProcess);
            }
            yield return new WaitForSecondsRealtime(bufferedInputTime);
            _storedState = -1;
        }

        #region ���ñ���
        // ���ø��������͵�����������ֵ
        public bool setFloatVariable(string VariableName, float result)
        {
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
        public void setFloatVariableNoChangeCondition(string VariableName, float result)
        {
            ConditionVariable f = behaviorObject.ConditionVariables.FirstOrDefault(x => x.name == VariableName);
            if (f != null && f is FloatVariable)
            {
                int Index = behaviorObject.ConditionVariables.IndexOf(f);
                varibleValues[Index] = result;
            }
        }
        // ���ò������͵�����������ֵ
        public bool setBoolVariable(string VariableName, bool result)
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
        public void setBoolVariablewNoReturnType(string VariableName, bool result)
        {
            setBoolVariable( VariableName,  result);
        }
        public void setFloatVariableNoReturnTyp(string VariableName, float result)
        {
            setFloatVariable(VariableName, result);
        }

        public void changeBoolForFrame(string target, bool result)
        {
            StartCoroutine(Process( target,  result));
        }
        IEnumerator Process(string target, bool result)
        {
            setBoolVariable(target, result);
            yield return null;
            setBoolVariable(target, !result);
        }
        #endregion
        // ��������Ƿ�����

        // ����������л�״̬
        #region �������
        public void CheakCondition()
        {
            if (!enabled)
            {
                return;
            }
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
                    if (behaviorObject.stateBehaviors[i].effect)
                    {
                        changtoState(stateBehavior);
                        return;
                    }
                }
            }

            // ����ҵ���״̬��ִ�����Ӧ����Ϊ
            //������һ���ʵ�ַ�ʽ��ƥ�����̵�addstate��������ģʽ
            if (stateBehavior != -1)
            {

                if (LockState)
                {
                    if (behaviorObject.stateBehaviors[stateBehavior].priority > behaviorObject.stateBehaviors[currentState].priority)
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
        #endregion
    }
}