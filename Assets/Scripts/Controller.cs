using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
namespace BehaviorControlling
{

    /// <remarks>���ƻ��ࣨ�����޸ģ��������ṩ�����Ŀ��Ƴ�Ա<para>�̳���MonoBehaviour���Ҽ̳и���Ľű���Ҫ���ص���Ӧ����Ϸ�����ϣ�</para></remarks>
    public abstract class Controller : MonoBehaviour
    {
        protected Coroutine stateCoroutine;
        protected Func<IEnumerator> currentIEnumerator;
        public virtual bool AddState(Func<IEnumerator> routine)
        {

            if (gameObject.activeInHierarchy) {
                currentIEnumerator = routine;
                stateCoroutine = StartCoroutine(routine());
                return true;
            }
            else
            {
                return gameObject.activeInHierarchy;
            }
        }
        public virtual void RemoveState()
        {
            if (stateCoroutine!=null) {
                StopCoroutine(stateCoroutine); // Stop the current coroutine if running
            }
        }
        private void OnDisable()
        {
            currentIEnumerator = null;
        }

        public virtual bool ChangeState(Func<IEnumerator> routine)
        {
            if (routine !=null&& !routine.Equals(currentIEnumerator))
            {
                
                RemoveState();
               AddState(routine);
            }
            return gameObject.activeInHierarchy;
        }
        public virtual bool SetState(Func<IEnumerator> routine)
        {
            if (routine != null)
            {
                RemoveState();
                AddState(routine);
            }
            return gameObject.activeInHierarchy;
        }
    }

}