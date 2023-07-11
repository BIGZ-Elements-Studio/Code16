using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  BehaviorControlling
{
    public class PlayerInputReciver : MonoBehaviour
    {
        [SerializeField]
        BehaviorController controller3d;
        PlayerInput inputActions;
        bool first = true;
        void Awake()
        {
            inputActions=new PlayerInput();
            inputActions.Enable();
            /*    inputActions.In3d.run.performed += ctx => { controller3d.setBoolVariable("移动", true); };
                inputActions.In3d.run.canceled += ctx => { controller3d.setBoolVariable("移动", false); };
                inputActions.In3d.dash.performed += ctx => { controller3d.setBoolVariable("闪避", true); };
                inputActions.In3d.dash.canceled += ctx => { controller3d.setBoolVariable("闪避", false); };
                inputActions.In3d.atk.performed += ctx => { controller3d.setBoolVariable("攻击", true); };
                inputActions.In3d.atk.canceled += ctx => { controller3d.setBoolVariable("攻击", false); };
                inputActions.In3d.skill.performed += ctx => { controller3d.setBoolVariable("技能", true); };
                inputActions.In3d.skill.canceled += ctx => { controller3d.setBoolVariable("技能", false); };
                inputActions.In3d.ultraSkill.performed += ctx => { controller3d.setBoolVariable("大招", true); };
                inputActions.In3d.ultraSkill.canceled += ctx => { controller3d.setBoolVariable("大招", false); };*/
            inputActions.In3d.run.performed += ctx => { controller3d.setBoolVariable("移动", true); };
            inputActions.In3d.run.canceled += ctx => { controller3d.setBoolVariable("移动", false); };
            inputActions.In3d.dash.started += ctx => { controller3d.changeBoolForFrame("闪避", true); };
            inputActions.In3d.atk.performed += ctx => { controller3d.changeBoolForFrame("攻击", true);};
            inputActions.In3d.skill.performed += ctx => { controller3d.changeBoolForFrame("技能", true); };
            inputActions.In3d.ultraSkill.performed += ctx => { controller3d.setBoolVariable("大招", true); };
            inputActions.In3d.holdatk.performed += ctx => { currentProcess= StartCoroutine(process()); };
            inputActions.In3d.holdatk.canceled += ctx => {if (currentProcess != null) {StopCoroutine(currentProcess);}if (time>=1) { time = -1 ; controller3d.setFloatVariable("按住攻击", (float)time / 10);}  };
        }
        public int time;
        Coroutine currentProcess;
        IEnumerator process()
        {
            time = 1;
            controller3d.setFloatVariable("按住攻击", (float)time / 10);
            while (true)
            {
                yield return new WaitForSecondsRealtime(0.1f);
                time+=1;
                controller3d.setFloatVariable("按住攻击", (float)time / 10);
            }
        }
        private void OnEnable()
        {
            if (first)
            {
                first = false;
                return;
            }
            inputActions.In3d.run.performed += ctx => { controller3d.setBoolVariable("移动", true); };
            inputActions.In3d.run.canceled += ctx => { controller3d.setBoolVariable("移动", false); };
            inputActions.Enable();
        }
        private void OnDisable()
        {
            inputActions.In3d.run.performed -= ctx => { controller3d.setBoolVariable("移动", true); };
            inputActions.In3d.run.canceled -= ctx => { controller3d.setBoolVariable("移动", false); };
            inputActions.Disable();
        }
    }
}