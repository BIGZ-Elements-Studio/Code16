using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//对Input的进一步封装
public class InputController : Controller
{
    static InputController _instance;
    [SerializeField]
    Vector2 _directionAxis;
    [SerializeField]
    float _space;
    [SerializeField]
    float _Fire1;
    [SerializeField]
    float _Fire2;
    [SerializeField]
    float _Fire3;

    public static Vector2 DirectionAxis { get => _instance._directionAxis; }
    public static float Space { get => _instance._space; }
    public static float Fire1 { get => _instance._Fire1; }
    public static float Fire2 { get => _instance._Fire2; }
    public static float Fire3 { get => _instance._Fire3; }

    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(this);
        _instance = this;
        AddState("DefaultState");
    }
    protected override void Update()
    {
        base.Update();
    }

    //默认的输入控制
    void DefaultState()
    {
        _directionAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _space = Input.GetAxis("Jump");
        _Fire1 = Input.GetAxis("Fire1");
        _Fire2 = Input.GetAxis("Fire2");
        _Fire3 = Input.GetAxis("Fire3");
    }
}
