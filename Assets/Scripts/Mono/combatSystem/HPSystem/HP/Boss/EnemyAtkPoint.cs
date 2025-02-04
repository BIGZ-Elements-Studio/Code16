using CombatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using static EnemyHPContainner;

public class EnemyAtkPoint : MonoBehaviour,DamageTarget
{
    [SerializeField]
    EnemyHPContainner enemyHPContainner;
    [SerializeField]
    Transform center;
    [SerializeField]
    Transform popupPosition;
    [SerializeField]
    Transform _lockedEnemyTransform;
    public UnityEvent<CombatColor> shieldBreak;
    public UnityEvent ReciveHit;
    public bool active = true;
    public UnityEvent<bool> _OnLockAppear;
    public UnityEvent _OnLockDistory;
    public UnityEvent<bool> OnLockAppear { get { return _OnLockAppear; } }
    public UnityEvent OnLockDistory { get { return _OnLockDistory; }  }

    public void addBuff(CharacterBuff buff)
    {
       // throw new System.NotImplementedException();
    }

    public void addBuff(TeamBuff buff)
    {
     //   throw new System.NotImplementedException();
    }
    public void ForceaddBuff(CharacterBuff buff)
    {
        throw new NotImplementedException();
    }

    public void ForceaddBuff(TeamBuff buff)
    {
        throw new NotImplementedException();
    }
    public void addforce(Vector3 originalWorldPosition, float magnitude)
    {
       // throw new System.NotImplementedException();
    }

    public void addforceOfDirection(Vector3 direction)
    {
      //  throw new System.NotImplementedException();
    }

    public bool Damage(DamageObject damage, CombatColor damageColor)
    {
        if (active) {
            EnemyHPContainner.damageInfo info = (enemyHPContainner.Damage(damage, damageColor));
            if (info.shieldBreak)
            {
                MakeColorBall(info.ShieldColor);
            }
            ReciveHit.Invoke();
            int i = info.CalculatedDamage;
            VisualEffectController.DoDamagePopUp(i, getType(), damage.Critic, popupPosition.position);
            return info.successfulDamaged;
        }
        return false;
    }

    private void MakeColorBall(CombatColor shieldColor)
    {
        shieldBreak?.Invoke(shieldColor);
    }

    public void DamageByPercent(int percent, CombatColor damageColor)
    {
         enemyHPContainner.DamageByPercent(percent, damageColor);
    }

    public Vector3 getCenterPosition()
    {
        return center.position;
    }

    public CombatColor getColor()
    {
        return enemyHPContainner.color;
    }

    public TargetType getType()
    {
       return enemyHPContainner.TargetType;
    }

    public Transform GetlockedEnemyTransform()
    {
        return _lockedEnemyTransform;
    }
}
