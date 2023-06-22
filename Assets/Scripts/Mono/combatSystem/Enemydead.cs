using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Enemydead : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    [SerializeField]
    GameObject falloutPrefeb;
    [SerializeField]
    Transform dropPosition;
   public void HpChanged(string s, float f)
    {
        if (f<=0)
        {
            Die();
        }
    }

    private void Die()
    {
      GameObject g=  Instantiate(falloutPrefeb);
        float a = UnityEngine.Random.value - 0.5f;
        float b = UnityEngine.Random.value - 0.5f;
        g.GetComponent<Rigidbody>().velocity = new Vector3(a*3,10,b*3);
        g.transform.position=dropPosition.position;
        Destroy(target);
    }
}
