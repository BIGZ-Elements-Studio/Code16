using System;
using System.Reflection;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected Action state;

    public virtual void AddState(string stateName)
    {
        var method = Delegate.CreateDelegate(typeof(Action), this, GetType().GetMethod(stateName, BindingFlags.NonPublic | BindingFlags.Instance));
        state += method as Action;
    }

    public virtual void RemoveState(string stateName)
    {
        var method = Delegate.CreateDelegate(typeof(Action), this, GetType().GetMethod(stateName, BindingFlags.NonPublic | BindingFlags.Instance));
        state -= method as Action;
    }
    protected virtual void Start()
    {
        state = () => { };
    }
    protected virtual void Update()
    {
        state();
    }

}
