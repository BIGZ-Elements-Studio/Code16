using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

//玩家配置信息
[Serializable]
public class PlayerConfig
{
    public float WalkSpeed = 3;
    public float RunSpeed = 6;
    public float JumpVelocity = 3;
}

///<remarks>配置类，用来将一些成员公开序列化在unity面板上，有利于策划调节参数<para>继承自MonoBehaviour，且继承该类的脚本需要挂载到相应的游戏对象上！</para></remarks>

public class Config : MonoBehaviour
{
    public static PlayerConfig PlayerConfig { get => _instance._playerConfig; }
    static Config _instance;
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    [FormerlySerializedAs("playerConfig")]
    [SerializeField]
    PlayerConfig _playerConfig = new PlayerConfig();

}