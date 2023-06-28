using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace BehaviorControlling
{

    /// <remarks>控制基类（请勿修改），用来提供基本的控制成员<para>继承自MonoBehaviour，且继承该类的脚本需要挂载到相应的游戏对象上！</para></remarks>
    public abstract class Controller : MonoBehaviour
    {
        protected Coroutine stateCoroutine;
        protected Func<IEnumerator> currentIEnumerator;
        public virtual void AddState(Func<IEnumerator> routine)
        {
            currentIEnumerator = routine;
            if (gameObject.activeInHierarchy) {
                Debug.Log("stoped "+gameObject.name+"   "+ routine.Method.Name);
                stateCoroutine = StartCoroutine(routine());
            }
        }

        public virtual void RemoveState()
        {
            if (stateCoroutine!=null) {
                StopCoroutine(stateCoroutine); // Stop the current coroutine if running
            }
        }

        public virtual void ChangeState(Func<IEnumerator> routine)
        {
            if (routine !=null&& !routine.Equals(currentIEnumerator))
            {
                
                RemoveState();
                AddState(routine);
            }
            
        }
        public virtual void SetState(Func<IEnumerator> routine)
        {
            if (routine != null)
            {
                RemoveState();
                AddState(routine);
            }

        }
    }

}