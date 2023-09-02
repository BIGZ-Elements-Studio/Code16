using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxBullet : MonoBehaviour
{
   public BoxCollider selfCollider;

    public CombatColor color;
    //ÉËº¦ÊýÖµ
    public int damagePercent;
    public int AtkValue;
    public int critcAtkRate;
    public int critcAtkDamage;
    public int Sp;
    public bool faceRight;
    public bool affectEnemy;
    public bool affectObject;
    public bool affectPlayer;
    public Transform origialPoint;
    public float ForceMagnitude;
    public bool actived;
    private void OnTriggerEnter(Collider other)
    {
        if (actived)
        {
            DamageTarget target = other.GetComponent(typeof(DamageTarget)) as DamageTarget;
            if (target != null)
            {
                damage(target);
            }
        }
    }

    public void active()
    {
        actived = true;
        Collider[] cs = Physics.OverlapBox(transform.position, selfCollider.size / 2, transform.rotation);
        foreach (Collider other in cs)
        {
            DamageTarget target = other.GetComponent(typeof(DamageTarget)) as DamageTarget;
            if (target != null)
            {
                damage(target);
            }
        }
    }
    public void disable()
    {
        actived = false;
    }

    void damage(DamageTarget target)
    {
            if (target != null)
            {
                if ((affectEnemy && target.getType() == TargetType.enemy) || (affectPlayer && target.getType() == TargetType.player) || (affectObject && target.getType() == TargetType.other))
                {
                    DamageObject a = DamageObject.GetdamageObject();
                    a.hardness = 10;
                    if (Random.value < critcAtkRate / 100f)
                    {
                        a.damage = calculateAmount(damagePercent * AtkValue / 100, critcAtkRate, true);
                        a.Critic = true;
                    }
                    else
                    {
                        a.damage = calculateAmount(damagePercent * AtkValue / 100, critcAtkRate, false);
                        a.Critic = false;
                    }

                DamageTarget(target, a);
                }
            }     
        //hit?.Invoke(Hit);
    }
    private int calculateAmount(int damage, int critcAtkDamage, bool critcAtk)
    {

        if (critcAtk)
        {
            return damage * (100 + critcAtkDamage) / 100;
        }
        else
        {
            return damage;
        }
    }

    bool DamageTarget(DamageTarget target, DamageObject d)
    {
        target.addforceOfDirection((origialPoint.position - target.getCenterPosition()).normalized * -ForceMagnitude);
        return target.Damage(d, color);
    }
}
