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
            inputActions.In3d.run.performed += ctx => { controller3d.setBoolVariable("移动", true); };
            inputActions.In3d.run.canceled += ctx => { controller3d.setBoolVariable("移动", false); };
            inputActions.In3d.dash.performed += ctx => { controller3d.setBoolVariable("闪避", true); };
            inputActions.In3d.dash.canceled += ctx => { controller3d.setBoolVariable("闪避", false); };
            inputActions.In3d.atk.performed += ctx => { controller3d.setBoolVariable("攻击", true); };
            inputActions.In3d.atk.canceled += ctx => { controller3d.setBoolVariable("攻击", false); };
            inputActions.In3d.skill.performed += ctx => { controller3d.setBoolVariable("技能", true); };
            inputActions.In3d.skill.canceled += ctx => { controller3d.setBoolVariable("技能", false); };
            inputActions.In3d.ultraSkill.performed += ctx => { controller3d.setBoolVariable("大招", true); };
            inputActions.In3d.ultraSkill.canceled += ctx => { controller3d.setBoolVariable("大招", false); };

            inputActions.In2d.move.performed += ctx => { controller2d.setBoolVariable("移动", true); };
            inputActions.In2d.move.canceled += ctx => { controller2d.setBoolVariable("移动", false); };
            inputActions.In2d.jump.performed += ctx => { controller2d.setBoolVariable("跳跃", true);  };
            inputActions.In2d.jump.canceled += ctx => { controller2d.setBoolVariable("跳跃", false); };
        }
    }
}