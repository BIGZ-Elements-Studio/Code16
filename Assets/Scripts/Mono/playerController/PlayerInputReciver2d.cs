using BehaviorControlling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReciver2d : MonoBehaviour
{
    [SerializeField]
    BehaviorController controller2d;
    PlayerInput inputActions;
    
    void Start()
    {
        InputController.allow2dInputChanged?.AddListener(delegate { refreshInputaction(); });
        inputActions.In2d.Enable();
        inputActions.In2d.move.performed += ctx => { controller2d.setBoolVariable("ÒÆ¶¯", true); };
        inputActions.In2d.move.canceled += ctx => { controller2d.setBoolVariable("ÒÆ¶¯", false); };
        inputActions.In2d.jump.performed += ctx => { controller2d.setBoolVariable("ÌøÔ¾", true); };
        inputActions.In2d.jump.canceled += ctx => { controller2d.setBoolVariable("ÌøÔ¾", false); };
       
    }
    private void Awake()
    {
        inputActions = new PlayerInput();
        
    }
    private void OnEnable()
    {
        refreshInputaction();


    }

    bool allowInput { get {return InputController.allow2dInput && enabled; } }
    void refreshInputaction()
    {
        Debug.Log(allowInput);
        if (allowInput)
        {
            inputActions.Enable();
        }
        else
        {
            inputActions.Disable();
        }
    }
    private void OnDisable()
    {
        refreshInputaction();
    }
}
