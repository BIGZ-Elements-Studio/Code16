using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleAtkPoint : MonoBehaviour, DamageTarget
{
    public TargetType targetType;
    public CombatColor CombatColor;
    public Transform center;
    public UnityEvent<string, bool> onHited;
    public UnityEvent ReciveHit;
    public UnityEvent<float> HitRemainHp;
    public UnityEvent<Vector3> AddedForce;
    [SerializeField] string hitName;
    public bool armor;
    public bool returnAfterHit;
   public float maxHp;
    float currentHp;
    bool useHardness;
    public void addBuff(CharacterBuff buff)
    {
    }
    public void addBuff(TeamBuff buff)
    {
    }
    public void ForceaddBuff(CharacterBuff buff)
    {
        //throw new System.NotImplementedException();
    }

    public void ForceaddBuff(TeamBuff buff)
    {
        //throw new System.NotImplementedException();
    }
    public void addforceOfDirection(Vector3 direction)
    {
        AddedForce.Invoke(direction);
    }

    public bool Damage(DamageObject damage, CombatColor damageColor)
    {
        if (!useHardness) {
            currentHp -= damage.damage;
        }
        else
        {
            currentHp -= damage.hardness;
        }
        HitRemainHp?.Invoke(currentHp);
        ReciveHit?.Invoke();
        onHited.Invoke(hitName, true);
        return returnAfterHit;
    }
    private void Awake()
    {
        currentHp=maxHp;
    }
    public void DamageByPercent(int percent, CombatColor damageColor)
    {
        onHited.Invoke(hitName, true);
    }

    public Vector3 getCenterPosition()
    {
        return center.position;
    }

    public TargetType getType()
    {
        return targetType;
    }


    public void addforce(Vector3 originalWorldPosition, float magnitude)
    {
        AddedForce.Invoke((originalWorldPosition - center.position) * magnitude);
    }

    public CombatColor getColor()
    {
        return CombatColor;
    }


}

