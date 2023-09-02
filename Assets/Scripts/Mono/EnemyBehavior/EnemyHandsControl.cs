using BehaviorControlling;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CombatSystem.boss.stoneperson
{
    public class EnemyHandsControl : MonoBehaviour
    {
       public stoneHandController controller1;
        public stoneHandController controller2;
        [SerializeField]
        public SkeletonAnimation skeletonAnimation;
        public EnemyShieldContainner ShieldContainner;
        [SpineAnimation]
        public string changeAnimation;
        [SpineAnimation]
        public string idle1;
        [SpineAnimation]
        public string idle2;
       public GameObject vineBullet;
        public void SetAnimation(string s)
        {
            spineAnimationState.SetAnimation(0, s, true);
        }
        public void SetAnimationNoRepeate(string s)
        {
            spineAnimationState.SetAnimation(0, s, false);
        }
        void state1()
        {
            SetAnimation(idle1);
            
        }
        private void Awake()
        {
            state1();
        }
        bool two;
        public void changeBlood(float hp)
        {
            if (hp<500&& !two)
            {
                changeState();
            }
        }
        void changeState()
        {
            two=true;
            SetAnimationNoRepeate(changeAnimation);
            spineAnimationState.AddAnimation(0,idle2,true,0);
        }
        Spine.AnimationState spineAnimationState { get { return skeletonAnimation.AnimationState; } }
        private void Start()
        {
            addshield();
            ShieldContainner.ShieldBreak.AddListener(delegate { StartCoroutine(OnShieldBreak()); });

        }
        Coroutine currrentC;
        public void skill1()
        {
            if (currrentC != null)
            {
                StopCoroutine(currrentC);
            }
            currrentC = StartCoroutine(trySkill1());
        }

       public void skill2()
        {
            if (currrentC != null)
            {
                StopCoroutine(currrentC);
            }
            currrentC = StartCoroutine(trySkill2());
        }
        public void skill3()
        {
            if (currrentC != null)
            {
                StopCoroutine(currrentC);
            }
            currrentC = StartCoroutine(trySkill3());
        }
        public void skill4()
        {
            if (currrentC != null)
            {
                StopCoroutine(currrentC);
            }
            currrentC = StartCoroutine(trySkill4());
        }
        public IEnumerator trySkill2()
        {
            yield return new WaitForSeconds(2f);
            controller1.Dofade();
            controller2.Dofade();
            while (true)
            {
                controller2.StartCoroutine(controller2.skill2());
                yield return new WaitForSeconds(7f);
                controller1.StartCoroutine(controller1.skill2());
                yield return new WaitForSeconds(7f);
            }
        }
        public IEnumerator trySkill1()
        {
            yield return new WaitForSeconds(2f);
            controller1.Dofade();
            controller2.Dofade();
            while (true)
            {
                controller1.StartCoroutine(controller1.skill1A());
                controller2.StartCoroutine(controller2.skill1B());
                yield return new WaitForSeconds(5f);
                controller1.StartCoroutine(controller1.skill1B());
                controller2.StartCoroutine(controller2.skill1A());
                yield return new WaitForSeconds(5f);

            }
        }

        public IEnumerator trySkill3()
        {
            yield return new WaitForSeconds(2f);
            controller1.Dofade();
            controller2.Dofade();
            while (true)
            {
                controller2.StartCoroutine(controller2.skill3());
                yield return new WaitForSeconds(7f);
                controller1.StartCoroutine(controller1.skill3());
                yield return new WaitForSeconds(7f);
            }

        }
        public IEnumerator trySkill4()
        {
            yield return new WaitForSeconds(2f);
            controller1.StartCoroutine(controller1.skill4());
                controller2.StartCoroutine(controller2.skill4());
            yield return new WaitForSeconds(1f);
            while (true)
            {
               GameObject g= Instantiate(vineBullet);
                g.transform.position = combatController.Player.transform.position;
                g.SetActive(true);
                
                yield return new WaitForSeconds(3f);
            }

        }
        public IEnumerator OnShieldBreak()
        {
            yield return new WaitForSeconds(3f);
            addshield();
        }

        void addshield()
        {
            ShieldContainner.setShield(CombatColor.blue,100);
        }
        public Transform origion;
        public float range;
        float MaxDistance=40;
        public void shootColotballToPlayer(CombatColor c)
        {
            float randomAngle = Random.Range(0f, Mathf.PI * 2f);
            Vector3 offset = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle))* range;
            GameObject g = Instantiate((combatColorController.GetColorBall(c)));
            g.transform.position = origion.position;
            Vector3 distanceBetween = (combatController.PlayerActualPosition - origion.position) + offset;
            Vector3 targetAngle = distanceBetween.normalized;
            float distance = distanceBetween.magnitude;
            float distancePercentage = distance / 60;
            float actualSpeed = MaxDistance * distancePercentage;
            float hight = 10;
            g.GetComponent<Rigidbody>().velocity = new Vector3(targetAngle.x * actualSpeed, hight, targetAngle.z * actualSpeed);
        }
    }
}