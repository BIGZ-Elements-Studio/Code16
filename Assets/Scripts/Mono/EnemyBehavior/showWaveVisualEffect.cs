using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showWaveVisualEffect : MonoBehaviour
{
    [SerializeField]
    LayerMask obsticle;
    Renderer r;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if ((obsticle.value & (1 << other.gameObject.layer)) != 0)
        {
            disappear(true);
        }
    }
    private void Awake()
    {
        r=GetComponent<Renderer>(); 
    }
    public void disappear(bool disappear)
    {
        r.enabled = !disappear;
    }
}
