using System;
using System.Reflection;
using UnityEngine;

/// <remarks>���ƻ��ࣨ���������޸ģ��������ṩ�����Ŀ��Ƴ�Ա���磺<para>��ǰ״̬Action state</para><para>���״̬�鷽��void AddState(string stateName)</para><para>�Ƴ�״̬�鷽�� void RemoveState(string stateName)��</para></remarks>
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
