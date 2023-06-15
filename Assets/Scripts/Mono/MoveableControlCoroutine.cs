using System;
using UnityEngine;
namespace oct.ObjectBehaviors
{
    public abstract class MoveableControlCoroutine : MonoBehaviour
    {
        public event Action LockState;
        public event Action UnLockState;
        protected void lockState(bool lockstate)
        {
            if (lockstate)
            {
                LockState?.Invoke();
            }
            else
            {
                UnLockState?.Invoke();

            }
        }
    }
}