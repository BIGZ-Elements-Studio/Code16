using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <remarks>控制基类（请勿修改），用来提供基本的控制成员<para>继承自MonoBehaviour，且继承该类的脚本需要挂载到相应的游戏对象上！</para></remarks>
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
