//这个脚本只负责判断该执行哪个状态并且调用，使用方式应该是另一个脚本读取输入，设置浮点/布尔数类型的条件变量的值，然后执行状态，逻辑和animator差不多，但是更为简化
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
        public bool log;
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
        //可以把这个改成string还是deleagate还是什么的，反正把method和对应的状态绑定就行
        public MoveableControlCoroutine targetCode;
        [HideInInspector]
        public List<string> CoroutineList = new List<string>();
        [HideInInspector]
        public List<float> varibleValues = new List<float>();

        //解除锁定后的时候调用
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
            targetCode.LockState += () => { LockState = true; };
            targetCode.UnLockState += () => { LockState = false; };
            CheakCondition();
        }

        private void OnEnable()
        {
            CheakCondition();
        }
        private void changtoState(int index)
        {
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
                    if (SetState(coroutineDelegate)){
                        Debug.Log(1);
                        currentState = index;
                    }
                }
                else
                {
                    if (ChangeState(coroutineDelegate))
                    {
                        currentState = index;
                    }
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

        #region 设置变量
        // 设置浮点数类型的条件变量的值
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
        // 设置布尔类型的条件变量的值
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
          
            setBoolVariable(target, !result);  yield return null;
        }
        #endregion
        // 检查条件是否满足

        // 检查条件并切换状态
        #region 检查条件
        public void CheakCondition()
        {
            if (!enabled)
            {
                return;
            }
            // 找到优先级最高的符合所有条件的状态
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

            // 如果找到了状态，执行其对应的行为
            //更改这一块的实现方式来匹配主程的addstate（“”）模式
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