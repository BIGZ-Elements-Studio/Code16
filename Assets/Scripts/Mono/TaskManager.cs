using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    static TaskManager _instance;

    List<Task> _runingTaskPool = new List<Task>();
    List<Task> _removeTaskPool = new List<Task>();
    List<Task> _pauseTaskPool = new List<Task>();
    public static Task AddTask(Action loopFun, int times, Action callBack)
    {
        var task = new Task(loopFun, times, callBack);
        _instance._runingTaskPool.Add(task);
        return task;
    }
    private void Start()
    {
        DontDestroyOnLoad(this);
        _instance = this;
    }
    private void Update()
    {
        foreach (var item in _runingTaskPool)
        {
            item.times--;
            if (item.times <= 0)
            {
                item.callBack();
                _removeTaskPool.Add(item);
            }
            else
            {
                item.loopFun();
            }
        }
        if (_removeTaskPool.Count > 0)
        {
            foreach (var item in _removeTaskPool)
            {
                if (_runingTaskPool.Contains(item))
                    _runingTaskPool.Remove(item);
                if (_pauseTaskPool.Contains(item))
                    _runingTaskPool.Remove(item);
            }
            _removeTaskPool.Clear();
        }
    }
    public class Task
    {
        internal Action loopFun;
        internal int times;
        internal Action callBack;

        internal Task(Action loopFun, int times, Action callBack)
        {
            if (loopFun == null)
                loopFun = () => { };
            if (callBack == null)
                callBack = () => { };
            this.loopFun = loopFun;
            this.times = times;
            this.callBack = callBack;
        }
        public void Stop()
        {
            _instance._removeTaskPool.Add(this);
        }

        public void Pause()
        {
            _instance._pauseTaskPool.Add(this);
        }
    }

}

