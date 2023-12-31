using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnimation : MonoBehaviour
{
   public Animation a;
    public GameObject g;
    void Start()
    {
        CombatSystem.combatController.OnCharaDie.AddListener(show);   
        g.SetActive(false);
    }

    private void show()
    {
        Debug.Log("dieedsss");
        g.SetActive(true);
        a.Play();
    }
    public void stop()
    {
        Debug.Log("dieedsss");
        Invoke("remove",0.07f);
    }
    private void remove()
    {
        g.SetActive(false);
    }
}
