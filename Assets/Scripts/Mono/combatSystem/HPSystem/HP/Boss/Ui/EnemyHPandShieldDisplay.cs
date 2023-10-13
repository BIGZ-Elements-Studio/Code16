using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPandShieldDisplay : MonoBehaviour
{
    [SerializeField]
    EnemyHPContainner target;
    [SerializeField]
    EnemyShieldContainner target2;
    [SerializeField]
    CustomProgressBar HPSlider;
    [SerializeField]
    Slider ShieldSlider;
    [SerializeField]
    Image ShieldFillColor;
    private void Awake()
    {
        target2.CombatColorChanged.AddListener(ChangeColor);
        target.onHPChangeWithMaxHP.AddListener(changeHp);
        target2.ShieldChanged.AddListener(changeShield); 
    }
    void ChangeColor(CombatColor color)
    {
        ShieldFillColor.color=combatColorController.getShieldColor(color);
    }

    void changeHp(int min,int max)
    {
      
        HPSlider.SetValue((float)min/max);
        
    }
    void changeShield(float f)
    {
        ShieldSlider.value = f;
    }
}
