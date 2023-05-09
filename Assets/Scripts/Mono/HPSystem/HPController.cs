using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HPController是一个Unity脚本，用于控制物体的血量。它包括类型、当前血量、基础血量、最大血量、防御力、基础防御力、韧性和基础韧性等属性。它可以根据设定来初始化当前血量，也可以通过Damage方法来减少当前血量。如果当前血量小于0，则自动重置为0。它还提供了按百分比减少血量的方法。这个脚本可以用于任何需要血量管理的物体上，例如玩家、敌人等。

namespace CombatSystem
{
    public class HPController : MonoBehaviour
    {
        public Type type;
        public int HP;
        public int baseHP;
        public int MaxHP;

        public int def;
        public int baseDef;

        public int Poise;
        public int basePoise;
        // Start is called before the first frame update
        void Start()
        {
            Reset();
        }

        public void Reset()
        {
            HP = MaxHP;
        }

        // Method to apply damage to HP
        public void Damage(DamageObject damage)
        {
            HP -= damage.damage;

            // Check if HP is below 0 and reset it to 0
            if (HP < 0)
            {
                HP = 0;
                Debug.Log("好死好死好死");
            }
        }

        // Method to apply damage to HP by percentage
        public void DamageByPercent(int percent)
        {
            float damage = (percent / 100f) * MaxHP;
            int roundedDamage = Mathf.RoundToInt(damage);
            DamageObject a = new DamageObject();
            // Call the Damage method with the calculated damage amount
            Damage(a);
        }

    }
}