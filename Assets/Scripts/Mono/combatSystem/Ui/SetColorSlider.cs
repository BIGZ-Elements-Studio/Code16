using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColorSlider : MonoBehaviour
{
    [SerializeField]
    playerTeamController controller;
    [SerializeField]
    Slider slider;
    [SerializeField]
    Image Image;
    private void Start()
    {
        controller.onColorChangeWithMaxColor.AddListener(changeValue);
        controller.onChangeColor.AddListener(changeColor);
    }
    void changeColor(CombatColor color)
    {
        if (color==CombatColor.yellow)
        {
            Image.color = Color.yellow;
        }
        else if (color == CombatColor.red)
        {
            Image.color = Color.red;
        }
        else if (color == CombatColor.blue)
        {
            Image.color = Color.blue;
        }
        
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
