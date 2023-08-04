using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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

        public IEnumerator trySkill2()
        {
            yield return new WaitForSeconds(2f);
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
            while (true)
            {
                controller2.StartCoroutine(controller2.skill3());
                yield return new WaitForSeconds(7f);
                controller1.StartCoroutine(controller1.skill3());
                yield return new WaitForSeconds(7f);
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
            Debug.Log("called");
            float randomAngle = Random.Range(0f, Mathf.PI * 2f);
            Vector3 offset = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle))* range;
            GameObject g = Instantiate((combatColorController.Instance.ColorBall));
            g.transform.position = origion.position;
            Vector3 distanceBetween = (combatController.PlayerActualPosition - origion.position) + offset;
            Vector3 targetAngle = distanceBetween.normalized;
            float distance = distanceBetween.magnitude;
            Debug.Log(distance);
            float distancePercentage = distance / 60;
            float actualSpeed = MaxDistance * distancePercentage;
            float hight = 10;
            g.GetComponent<Rigidbody>().velocity = new Vector3(targetAngle.x * actualSpeed, hight, targetAngle.z * actualSpeed);
            g.GetComponent<colorball>().color = c;
        }
    }
}