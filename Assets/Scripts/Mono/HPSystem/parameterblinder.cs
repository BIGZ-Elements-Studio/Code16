using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace codeTesting
{
    public class parameterblinder : MonoBehaviour
    {
        public string name;
        public BehaviorController controller;
        public void changeValue(int i)
        {
            controller.setFloatVariable(name,i);
        }
    }
}