using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisualEffectController : MonoBehaviour
{
    [SerializeField]
    GameObject DamagePopUpPrefec;
    private static VisualEffectController Instance;
    public enum DamagePopUpType
    {
        damage,criticDamage,cure,damagePlayer
    }
    private void Start()
    {
        Instance = this;
    }
    public static void DoDamagePopUp(int amount, DamagePopUpType type,Vector3 position)
    {
        Vector3 v = new Vector3(Random.value-0.5f, Random.value - 0.5f, Random.value - 0.5f);
       GameObject g= Instantiate(Instance.DamagePopUpPrefec);
        g.GetComponent<TextMeshPro>().text = amount+ type.ToString();
        g.transform.position = position+v;
    }
    public static void DoDamagePopUp(int amount,TargetType type,bool critic,Vector3 position)
    {
        if (type==TargetType.player)
        {
            if (amount>0) {
                DoDamagePopUp(amount, DamagePopUpType.damagePlayer, position);
            }
            else
            {
                DoDamagePopUp(amount, DamagePopUpType.cure, position);
            }
        }
        else
        {
            if (critic)
            {
                DoDamagePopUp(amount, DamagePopUpType.criticDamage, position);
            }
            else
            {
                DoDamagePopUp(amount, DamagePopUpType.damage, position);
            }
        }
    }
}
