using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSpSlider : MonoBehaviour
{
    [SerializeField]
    AttackAttributeController controller;
    [SerializeField]
    Slider slider;
    private void Awake()
    {
        controller.onSpChange.AddListener(changeValue);
    }
    // Start is called before the first frame update
    public void changeValue(string s, float value)
    {
        if (controller.MaxSp != 0)
        {
            slider.value = value / controller.MaxSp;
        }
    }
}
