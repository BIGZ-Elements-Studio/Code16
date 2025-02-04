using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class SphereBullet : MonoBehaviour
{
    public SphereCollider selfCollider;

    public CombatColor color;
    //�˺���ֵ
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
    public int hardness;
       public UnityEvent<bool> hit;
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
        Collider[] cs = Physics.OverlapSphere(transform.position, selfCollider.radius);
        foreach (Collider other in cs)
        {
            DamageTarget target = other.GetComponent(typeof(DamageTarget)) as DamageTarget;
            if (target != null)
            {
                 damage(target);
            }
        }
    }
    public void diable()
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
                a.hardness = hardness;
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
                Debug.Log((target as MonoBehaviour).gameObject.name);
                DamageTarget(target, a);
                hit?.Invoke(true);
            }
        }

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

        // target.addforce((origialPoint.position- targe.transform.position).normalized*-ForceMagnitude);
        target.addforceOfDirection((origialPoint.position - target.getCenterPosition()).normalized * -ForceMagnitude);
        return target.Damage(d, color);
    }
}
