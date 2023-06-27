using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//HPController是一个Unity脚本，用于控制物体的血量。它包括类型、当前血量、基础血量、最大血量、防御力、基础防御力、韧性和基础韧性等属性。它可以根据设定来初始化当前血量，也可以通过Damage方法来减少当前血量。如果当前血量小于0，则自动重置为0。它还提供了按百分比减少血量的方法。这个脚本可以用于任何需要血量管理的物体上，例如玩家、敌人等。

namespace CombatSystem
{
    public class HPController : MonoBehaviour
    {
        public TargetType type;
        public int HP;
        public int baseHP;
        public int MaxHP;

        public int def;
        public int baseDef;

        public int Poise;
        public int basePoise;
        public Transform DamagePoint;
        public string PoiseName;
        public string hpName;
        public string hitName;
        public bool armor;
        public BuffContainer BuffContainer;
        public UnityEvent< string,float> onPoiseChange;
        public UnityEvent<string,float> onHPChange;
        public UnityEvent<string, bool> onHited;
        public UnityEvent<Vector3> AddedForce;
        [SerializeField]
        playerAttibutesByGrade attribute;
        
        private void setAmount()
        {
            baseHP = attribute.baseHP;
            baseDef = attribute.baseDef;
            basePoise = attribute.basePoise;
        }
        
        // Start is called before the first frame update

        public void setAremd(string s, bool result)
        {
            armor=result;
        }
        void Start()
        {
            Reset();
            onHPChange?.Invoke(hpName, HP);
            onPoiseChange?.Invoke(PoiseName, Poise);
        }

        public void Reset()
        {
            HP = MaxHP;
        }
        public void addforce(Vector3 force){

            AddedForce?.Invoke(force);
        }
            // Method to apply damage to HP
            public bool Damage(DamageObject damage)
        {
            onHited.Invoke(hitName, true);
            if (!armor) {
                HP -= damage.damage;
                if (HP < 0)
                {
                    HP = 0;

                }
                Poise -= damage.hardness;
                if (Poise < 0)
                {
                    Poise = 0;

                }
                onHPChange?.Invoke(hpName, HP);
                onPoiseChange?.Invoke(PoiseName, Poise);
                if (type == TargetType.enemy) {
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
                    if (damage.damage > 0) {
                        VisualEffectController.DoDamagePopUp(damage.damage, VisualEffectController.DamagePopUpType.damagePlayer, DamagePoint.position);
                    }
                    else if(damage.damage < 0)
                    {
                        VisualEffectController.DoDamagePopUp(damage.damage, VisualEffectController.DamagePopUpType.cure, DamagePoint.position);
                    }
                }
            }
            // Check if HP is below 0 and reset it to 0
            return !armor;
        }

        // Method to apply damage to HP by percentage
        public void DamageByPercent(int percent)
        {
            float damage = (percent / 100f) * MaxHP;
            DamageObject a = new DamageObject();
            // Call the Damage method with the calculated damage amount
            Damage(a);
        }

    }
}