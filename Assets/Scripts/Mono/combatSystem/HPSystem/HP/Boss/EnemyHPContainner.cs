using CombatSystem;
using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyShieldContainner))]

public class EnemyHPContainner : MonoBehaviour
{
    [SerializeField]
    EnemyShieldContainner shieldContainner;
    [SerializeField]
    FieldForCharacterBuff characterBuff;
    public TargetType TargetType=TargetType.enemy;
    public CombatColor color;
    public int HP = 100;
    public int baseHP = 100;
    public int MaxHP { get { return baseHP; } }

    public int def { get { return baseDef+ characterBuff.Def; } }
    public int baseDef = 100;
    public string hpName;
    public string hitName;
    public bool armor;
    public TeamBuffContainer BuffContainer;
    public UnityEvent<string, float> onHPChange;
    public UnityEvent<string, bool> onHited;
    public UnityEvent<int, int> onHPChangeWithMaxHP;
    public UnityEvent shieldBreak;
    public UnityEvent<float> HPChanged;
    public UnityEvent Dead;
    public void Reset()
    {
        HP = MaxHP;

    }
    //successful Damaged, CalculatedDamage, shieldBreak,ShieldColor
    public damageInfo Damage(DamageObject damage, CombatColor damageColor)
    {
        
        int actualDamage = DamageUtility.calculateDamage(def, damage.damage, 0);
        damageInfo info;
            HP -= actualDamage;
            if (HP < 0)
            {
                HP = 0;
            Dead?.Invoke();
            }

        onHPChange?.Invoke(hpName, HP);
        onHPChangeWithMaxHP?.Invoke(HP, MaxHP);
        info.successfulDamaged = true;
        info.CalculatedDamage = damage.damage;
        info.shieldBreak = shieldContainner.reduceShield(damage);
        info.ShieldColor = shieldContainner.color;
        HPChanged.Invoke(HP);
        return info;
    }


    public void DamageByPercent(int percent, CombatColor damageColor)
    {
        throw new NotImplementedException();
    }

   public struct damageInfo
    {
       public bool successfulDamaged;
        public int CalculatedDamage;
        public bool shieldBreak;
        public CombatColor ShieldColor;
    }
}
