using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  BehaviorControlling
{
    public class PlayerInputReciver : MonoBehaviour
    {
        [SerializeField]
        BehaviorController controller3d;
        [SerializeField]
        BehaviorController controller2d;
        PlayerInput inputActions;
        void Start()
        {
            inputActions=new PlayerInput();
            inputActions.Enable();
            inputActions.In3d.run.performed += ctx => { controller3d.setBoolVariable("ÒÆ¶¯", true); };
            inputActions.In3d.run.canceled += ctx => { controller3d.setBoolVariable("ÒÆ¶¯", false); };
            inputActions.In3d.dash.performed += ctx => { controller3d.setBoolVariable("ÉÁ±Ü", true); };
            inputActions.In3d.dash.canceled += ctx => { controller3d.setBoolVariable("ÉÁ±Ü", false); };
            inputActions.In3d.atk.performed += ctx => { controller3d.setBoolVariable("¹¥»÷", true); };
            inputActions.In3d.atk.canceled += ctx => { controller3d.setBoolVariable("¹¥»÷", false); };

            inputActions.In2d.move.performed += ctx => { controller2d.setBoolVariable("ÒÆ¶¯", true); };
            inputActions.In2d.move.canceled += ctx => { controller2d.setBoolVariable("ÒÆ¶¯", false); };
            inputActions.In2d.jump.performed += ctx => { controller2d.setBoolVariable("ÌøÔ¾", true);  };
            inputActions.In2d.jump.canceled += ctx => { controller2d.setBoolVariable("ÌøÔ¾", false); };
        }
    }
}