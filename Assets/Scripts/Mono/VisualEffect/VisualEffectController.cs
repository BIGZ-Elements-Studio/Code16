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
}
