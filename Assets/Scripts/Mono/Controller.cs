using System;
using System.Reflection;
using UnityEngine;

/// <remarks>控制基类（请勿随意修改），用来提供基本的控制成员，如：<para>当前状态Action state</para><para>添加状态虚方法void AddState(string stateName)</para><para>移除状态虚方法 void RemoveState(string stateName)等</para></remarks>
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
