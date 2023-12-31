using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPDisplay : CustomProgressBar
{
   public int saparationNum=2;
    [SerializeField]
    TextMeshProUGUI TextMeshPro;
    int remaining;
    public float each;
    [SerializeField]
    Image background;
    private void Start()
    {
        each = (1 / (float)saparationNum);
        remaining = saparationNum;
        TextMeshPro.text = remaining.ToString();
    }
    public override void SetValue(float ratio)
    {
        float newFloat = (ratio % each)/ each;
        
        int newRemaining=(int)(MathF. Ceiling(ratio / each));
        if (newRemaining<=1)
        {
            background.enabled = false;
            newRemaining = 1;
        }
        else
        {
            background.enabled = true;
        }
        if (newRemaining < remaining)
        {
            base.SetValue(1);
            base.SetValue(newFloat);
        }
        else
        {
            base.SetValue (newFloat);
        }
       
        remaining = newRemaining;
        TextMeshPro.text= newRemaining.ToString();
    }

}
