using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

//Íæ¼ÒÅäÖÃÐÅÏ¢
[Serializable]
public class PlayerConfig
{
    public float WalkSpeed = 3;
    public float RunSpeed = 6;
    public float JumpVelocity = 3;
}
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