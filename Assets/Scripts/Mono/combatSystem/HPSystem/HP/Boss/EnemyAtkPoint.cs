using CombatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public UnityEvent<CombatColor> shieldBreak;
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
        
        EnemyHPContainner.damageInfo info = (enemyHPContainner.Damage(damage, damageColor));
       if(info.shieldBreak)
        {
            MakeColorBall(info.ShieldColor);
        }
        int i= info.CalculatedDamage;
        VisualEffectController.DoDamagePopUp(i, getType(), damage.Critic, popupPosition.position);
        return info.successfulDamaged;
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
        throw new System.NotImplementedException();
    }

    public TargetType getType()
    {
       return enemyHPContainner.TargetType;
       return enemyHPContainner.TargetType;
    }


}
