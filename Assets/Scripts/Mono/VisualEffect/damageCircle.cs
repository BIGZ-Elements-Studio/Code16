using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageCircle : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem System;
    public float time;
    private void OnEnable()
    {
        var main = System.main;
        main.simulationSpeed = 1 / time;
        System.Play();
        Invoke("stop", time);
    }

    private void stop()
    {
        gameObject.SetActive(false);
    }
}
