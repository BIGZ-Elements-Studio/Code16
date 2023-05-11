using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace codeTesting
{
    public class PlayerInputReciver : MonoBehaviour
    {
        [SerializeField]
        BehaviorController controller;  
        PlayerInput inputActions;
        void Start()
        {
            inputActions=new PlayerInput();
            inputActions.Enable();
            inputActions.In3d.run.performed += ctx => { controller.setBoolVariable("ÒÆ¶¯", true); };
            inputActions.In3d.run.canceled += ctx => { controller.setBoolVariable("ÒÆ¶¯", false); };
            inputActions.In3d.atk.performed += ctx => { controller.setBoolVariable("¹¥»÷", true);Debug.Log("µã»÷"); };
            inputActions.In3d.atk.canceled += ctx => { controller.setBoolVariable("¹¥»÷", false); };
        }
    }
}