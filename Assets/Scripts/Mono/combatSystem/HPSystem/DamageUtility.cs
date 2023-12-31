using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUtility
{
    public static float defReductionRate=0.01f;
    public static int calculateDamage(int def,int damage,int Amplify)
    {
       
        // Calculate damage with amplification
        int amplifiedDamage = (int)(damage *((100f+Amplify)/100));
        float reductionPercent = def * defReductionRate;
        int finalDamage = (int)(amplifiedDamage *(1f- reductionPercent) );

        // Ensure damage is non-negative (no negative damage)
        if (finalDamage < 0)
        {
            finalDamage = 0;
        }
        return finalDamage;
    }
    public static int calculateDamage(int def, int damage, int Amplify,bool differentColor)
    {

        // Calculate damage with amplification
        int amplifiedDamage = (int)(damage * ((100f + Amplify) / 100));
        float reductionPercent = (def) * (1f - defReductionRate);
        int finalDamage = (int)(amplifiedDamage * (1f - reductionPercent));

        // Ensure damage is non-negative (no negative damage)
        if (finalDamage < 0)
        {
            finalDamage = 0;
        }
        Debug.Log((def) * (1f - defReductionRate));
        return finalDamage;
    }
}
