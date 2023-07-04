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
        
        inputActions =new PlayerInput();inputActions.In2d.Enable();
        inputActions.In2d.move.performed += ctx => { controller2d.setBoolVariable("ÒÆ¶¯", true); };
        inputActions.In2d.move.canceled += ctx => { controller2d.setBoolVariable("ÒÆ¶¯", false); };
        inputActions.In2d.jump.performed += ctx => { controller2d.setBoolVariable("ÌøÔ¾", true); };
        inputActions.In2d.jump.canceled += ctx => { controller2d.setBoolVariable("ÌøÔ¾", false); };
    }

    private void OnEnable()
    {
        if (inputActions!=null)
        { inputActions.Enable();
            return;
        }
       
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
