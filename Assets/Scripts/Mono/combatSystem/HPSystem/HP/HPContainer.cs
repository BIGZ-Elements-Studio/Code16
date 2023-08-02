using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CombatSystem
{
    public class HPContainer : MonoBehaviour
    {
        public TargetType type;
        public CombatColor color;
        public int HP;
        public int baseHP;
        public int ShieldAmount;
        public int MaxHP { get { return baseHP; } }

        public int def { get { return baseDef; } }
        public int baseDef;
        public int basePoise;
        public Transform DamagePoint;
        public string hpName;
        public string hitName;
        public bool armor;
        public TeamBuffContainer BuffContainer;
        public UnityEvent<string, float> onHPChange;
        public UnityEvent<string, bool> onHited;
        public UnityEvent<int, int> onHPChangeWithMaxHP;
        public UnityEvent shieldBreak;

        [SerializeField]
        playerAttibutesByGrade attribute;

        public void addBuff(TeamBuff buff)
        {
            if (!armor)
            {
                BuffContainer.addBuff(buff);
            }
        }
        internal void ForceaddBuff(TeamBuff buff)
        {
            BuffContainer.addBuff(buff);
        }
        private void setAmount()
        {
            
            baseHP = attribute.baseHP;
            baseDef = attribute.baseDef;
            basePoise = attribute.basePoise;
        }

        // Start is called before the first frame update

        public void setAremd(string s, bool result)
        {
            armor = result;
        }
        void Start()
        {
            Reset();
            onHPChange?.Invoke(hpName, HP);
            onHPChangeWithMaxHP?.Invoke(MaxHP,HP);
            if (attribute!=null) {
                setAmount();
            }
            HP = MaxHP;
        }

        public void Reset()
        {
            HP = MaxHP;
        }
        // Method to apply damage to HP
        public (int, bool) SelfDamage(int ActualDamage)
        {
            int remainingDamage = ActualDamage - ShieldAmount;
            
            if (ShieldAmount!=0&& remainingDamage<=0)
            {
                //damage can be shieldSystem;
                ShieldAmount -= ActualDamage;
                return (0, false);
            }
            shieldBreak?.Invoke();
            ActualDamage = remainingDamage;
            ShieldAmount = 0;
            
            onHited.Invoke(hitName, true);
            if (!armor)
            {
                HP -= ActualDamage;
                if (HP < 0)
                {
                    HP = 0;

                }
                onHPChange?.Invoke(hpName, HP);
                onHPChangeWithMaxHP?.Invoke(MaxHP, HP);
            }
            // Check if HP is below 0 and reset it to 0
            return (ActualDamage, !armor);
        }
        public bool Damage(DamageObject damage,CombatColor damageColor)
        {
            onHited.Invoke(hitName, true);
            if (!armor)
            {
                HP -= damage.damage;
                if (HP < 0)
                {
                    HP = 0;

                }
                onHPChange?.Invoke(hpName, HP);
                onHPChangeWithMaxHP?.Invoke(MaxHP, HP);
                if (type == TargetType.enemy)
                {
                    if (damage.Critic)
                    {
                        VisualEffectController.DoDamagePopUp(damage.damage, VisualEffectController.DamagePopUpType.criticDamage, DamagePoint.position);
                    }
                    else
                    {
                        VisualEffectController.DoDamagePopUp(damage.damage, VisualEffectController.DamagePopUpType.damage, DamagePoint.position);
                    }

                }
                if (type == TargetType.player)
                {
                    if (damage.damage > 0)
                    {
                        VisualEffectController.DoDamagePopUp(damage.damage, VisualEffectController.DamagePopUpType.damagePlayer, DamagePoint.position);
                    }
                    else if (damage.damage < 0)
                    {
                        VisualEffectController.DoDamagePopUp(damage.damage, VisualEffectController.DamagePopUpType.cure, DamagePoint.position);
                    }
                }
            }
            // Check if HP is below 0 and reset it to 0
            return !armor;
        }
        // Method to apply damage to HP by percentage
        public void DamageByPercent(int percent, CombatColor damageColor)
        {
            float damage = (percent / 100f) * MaxHP;
            DamageObject a = new DamageObject();
            // Call the Damage method with the calculated damage amount
            Damage(a, damageColor);
        }

        public TargetType getType()
        {
            return type;
        }
        public CombatColor getColor()
        {
            return color;
        }

    }
}