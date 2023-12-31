
//Bullet��һ��Unity�ű�������ʵ���ӵ�����Ϊ���������ӳ١������ʱ�䡢�˺���ֵ���������ʡ������˺������ԡ��������ӳٺ�ʼѭ����ÿ��һ����ʱ�����ͻ����Ƿ�����ײ�������������ײ������ײ�����������HPController����������ͷ����ӵ��趨���˺����ͣ��ͻ�����ӵ����˺���ֵ�ͱ������ʼ���������˺�ֵ��Ȼ�����Ŀ�������Damage��������������˺���
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace CombatSystem
{
    public class Bullet : MonoBehaviour
    {
        public CombatColor color;
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
        public UnityEvent<bool> hit;
        public UnityEvent<int> gainSp;
        public UnityEvent<DamageTarget> hitTarget;

        [SerializeField]
        GameObject bulletObject;
        private void Start()
        {
            StartCoroutine(process());
            if (!faceRight)
            {
                origialPoint.transform.localPosition=new Vector3(-origialPoint.transform.localPosition.x, origialPoint.transform.localPosition.y, origialPoint.transform.localPosition.z);
            }
        }

        IEnumerator process()
        {
            float totalTimePassed = 0;
            if (BasedOnFixedTime) {
                yield return new WaitForSecondsRealtime(delay);
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }
            while (totalTimePassed <= time)
            {
                Damage();
                if (interval>0.1) {
                    if (BasedOnFixedTime)
                    {
                        yield return new WaitForSecondsRealtime(interval);
                    }
                    else
                    {
                        yield return new WaitForSeconds(interval);

                    }
                }
                else
                {
                    if (BasedOnFixedTime)
                    {
                        yield return new WaitForSecondsRealtime(0.1f);
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.1f);

                    }
                }
                totalTimePassed += Time.deltaTime;
            }
            if (BasedOnFixedTime)
            {
                yield return new WaitForSecondsRealtime(last);
            }
            else
            {
                yield return new WaitForSeconds(last);
            }
            Destroy(bulletObject);

        }

        void Damage()
        {
            int i = 0;
            Collider[] overlaps = Physics.OverlapBox(transform.position, size / 2, transform.rotation);
            bool Hit = false;
            bool sp = false;
            foreach (Collider b in overlaps)
            {
                DamageTarget target = b.GetComponent(typeof(DamageTarget)) as DamageTarget;
                if (target != null)
                {
                    if ((affectEnemy && target.getType() == TargetType.enemy) || (affectPlayer && target.getType() == TargetType.player) || (affectObject && target.getType() == TargetType.other))
                {
                    DamageObject a = DamageObject.GetdamageObject();
                    a.hardness = 10;
                    if (Random.value < critcAtkRate / 100f)
                    {
                        a.damage = calculateAmount(damagePercent*AtkValue/100, critcAtkRate, true);
                        a.Critic = true;
                    }
                    else
                    {
                        a.damage = calculateAmount(damagePercent * AtkValue/100, critcAtkRate, false);
                        a.Critic = false;
                    }

                        if (DamageTarget(target, a))
                        {
                            Hit = true;
                            if (target.getType()!=TargetType.other)
                            {
                                sp=true;
                            }
                        }
                        i += 1;
                    }
                }
                
            }
            if (sp) {
                    gainSp?.Invoke(Sp);
                }
            hit?.Invoke(Hit);
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

        bool DamageTarget(DamageTarget target, DamageObject d)
        {

            // target.addforce((origialPoint.position- targe.transform.position).normalized*-ForceMagnitude);
            target.addforceOfDirection((origialPoint.position - target.getCenterPosition()).normalized * -ForceMagnitude);
             hitTarget?.Invoke(target);
          return  target.Damage(d, color);
        }
    }
}