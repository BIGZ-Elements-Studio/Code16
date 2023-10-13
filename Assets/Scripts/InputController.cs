using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    static public UnityEvent<bool> allow3dInputChanged;
    public static bool allow3dInput { get { return _allow3dInput; } set { _allow3dInput = value; allow3dInputChanged?.Invoke(value); } }
    static bool _allow3dInput;


    static public UnityEvent<bool> allow2dInputChanged;

    public static bool allow2dInput { get { return _allow2dInput; } set { Debug.Log("called"); _allow2dInput = value; allow2dInputChanged?.Invoke(value); } }
    static bool _allow2dInput;
    private void Awake()
    {
        allow3dInputChanged = new UnityEvent<bool>();
        allow2dInputChanged = new UnityEvent<bool>();
    }
}
