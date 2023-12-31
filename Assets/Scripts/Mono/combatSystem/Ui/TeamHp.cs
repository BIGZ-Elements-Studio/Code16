using CombatSystem;
using UnityEngine;
using UnityEngine.UI;

public class TeamHp : MonoBehaviour
{
    [SerializeField]
    HPContainer controller;
    [SerializeField]
    CustomProgressBar slider;
    private void Awake()
    {
        controller.onHPChangeWithMaxHP.AddListener(changeValue);
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
