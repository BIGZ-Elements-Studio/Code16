using System;
using UnityEngine;
namespace oct.generatedBehavior
{
    public abstract class CharacterControlCoroutine : MonoBehaviour
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