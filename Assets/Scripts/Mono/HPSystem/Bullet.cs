
//Bullet��һ��Unity�ű�������ʵ���ӵ�����Ϊ���������ӳ١������ʱ�䡢�˺���ֵ���������ʡ������˺������ԡ��������ӳٺ�ʼѭ����ÿ��һ����ʱ�����ͻ����Ƿ�����ײ�������������ײ������ײ�����������HPController����������ͷ����ӵ��趨���˺����ͣ��ͻ�����ӵ����˺���ֵ�ͱ������ʼ���������˺�ֵ��Ȼ�����Ŀ�������Damage��������������˺���
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CombatSystem
{
    public class Bullet : MonoBehaviour
    {
        //��ûд
        public bool BasedOnFixedTime;
        public float delay;
        public float last;

        //���
        public float interval;
        //��ʱ��
        public float time;
        public Vector3 size;
        //�˺���ֵ
        public int damage;
        public int critcAtkRate;
        public int critcAtkDamage;

        public bool affectEnemy;
        public bool affectObject;
        public bool affectPlayer;
        private void OnEnable()
        {
            StartCoroutine(process());
        }

        IEnumerator process()
        {
            float totalTimePassed = 0;
            yield return new WaitForSeconds(delay);
            while (totalTimePassed <= time)
            {
                Damage();
                if (interval>0.1) {
                    yield return new WaitForSeconds(interval);
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                }
                totalTimePassed += Time.deltaTime;
            }
            yield return new WaitForSeconds(last);

        }

        void Damage()
        {
            Collider[] overlaps = Physics.OverlapBox(transform.position, size / 2, transform.rotation);
            foreach (Collider b in overlaps)
            {
                HPController target = b.GetComponent<HPController>();
                if (target != null && (affectEnemy && target.type == Type.enemy) || (affectPlayer && target.type == Type.player) || (affectObject && target.type == Type.other))
                {
                    DamageObject a = DamageObject.GetdamageObject();
                    a.hardness = 10;
                    if (Random.value < critcAtkRate / 100f)
                    {
                        a.damage = calculateAmount(damage, critcAtkRate, true);
                        a.Critic = true;
                    }
                    else
                    {
                        a.damage = calculateAmount(damage, critcAtkRate, false);
                        a.Critic = false;
                    }

                    DamageTarget(target, a);
                }
            }
        }

        private int calculateAmount(int damage, int critcAtkDamage, bool critcAtk)
        {

            if (critcAtk)
            {
                return damage * (100+critcAtkDamage) / 100;
            }
            else
            {
                return damage;
            }
        }

        void DamageTarget(HPController target, DamageObject d)
        {
            target.Damage(d);
        }
    }
}