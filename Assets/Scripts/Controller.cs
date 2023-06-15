using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace BehaviorControlling
{

    /// <remarks>���ƻ��ࣨ�����޸ģ��������ṩ�����Ŀ��Ƴ�Ա<para>�̳���MonoBehaviour���Ҽ̳и���Ľű���Ҫ���ص���Ӧ����Ϸ�����ϣ�</para></remarks>
    public abstract class Controller : MonoBehaviour
    {
        protected Coroutine stateCoroutine;
        protected Func<IEnumerator> currentIEnumerator;
        public virtual void AddState(Func<IEnumerator> routine)
        {
            currentIEnumerator = routine;
            stateCoroutine = StartCoroutine(routine());
            
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