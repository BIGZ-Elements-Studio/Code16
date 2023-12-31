using CombatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHPHandler : MonoBehaviour
{
    public HPContainer HP;
    public string DieEventName;
    public UnityEvent<string, bool> die;
    public UnityEvent endGame;
    public float waitTime = 1;
    private void Awake()
    {
        HP.onHPChange.AddListener(checkHp);
    }
    public IEnumerator WaitDie()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        endGame?.Invoke();
        combatController.CharaDie();
    }
    private void checkHp(string arg0, float arg1)
    {
        Debug.Log(arg1);
        if (arg1<=0)
        {
            die?.Invoke(DieEventName, true); StartCoroutine(WaitDie());
        }
       
    }
}
