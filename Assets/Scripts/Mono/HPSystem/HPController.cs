using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HPController��һ��Unity�ű������ڿ��������Ѫ�������������͡���ǰѪ��������Ѫ�������Ѫ���������������������������Ժͻ������Ե����ԡ������Ը����趨����ʼ����ǰѪ����Ҳ����ͨ��Damage���������ٵ�ǰѪ���������ǰѪ��С��0�����Զ�����Ϊ0�������ṩ�˰��ٷֱȼ���Ѫ���ķ���������ű����������κ���ҪѪ������������ϣ�������ҡ����˵ȡ�

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
                Debug.Log("������������");
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