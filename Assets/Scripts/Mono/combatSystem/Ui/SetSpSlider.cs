using CombatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
namespace CombatSystem.Ui
{
    public class SetSpSlider : MonoBehaviour
    {
        [SerializeField]
        playerTeamController controller;
        [SerializeField]
        Slider slider;
        private void Start()
        {
            controller.onSpChangeWithMaxSp.AddListener(changeValue);
        }
        // Start is called before the first frame update
        public void changeValue(int s, int value)
        {
            if (s != 0)
            {
                slider.value = (float)value / s;
            }
        }
    }
}