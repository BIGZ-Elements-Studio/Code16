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
            inputActions.In3d.run.performed += ctx => { controller.setBoolVariable("�ƶ�", true); };
            inputActions.In3d.run.canceled += ctx => { controller.setBoolVariable("�ƶ�", false); };
            inputActions.In3d.atk.performed += ctx => { controller.setBoolVariable("����", true);Debug.Log("���"); };
            inputActions.In3d.atk.canceled += ctx => { controller.setBoolVariable("����", false); };
        }
    }
}