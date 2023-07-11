using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatSystem.boss.stoneperson
{
    public class EnemyHandsControl : MonoBehaviour
    {
       public stoneHandController controller1;
        public stoneHandController controller2;
        [SerializeField]
        public SkeletonAnimation skeletonAnimation;

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
        public void changeBlood(string s,float hp)
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
         //   InvokeRepeating("skill1", 0, 10);
           // InvokeRepeating("skill1B", 5, 10);

        }
        void skill1()
        {
            controller1.StartCoroutine(controller1.skill1A());
            controller2.StartCoroutine(controller2.skill1B());
        }

        void skillC()
        {

        }
        void skill1B()
        {
            controller2.StartCoroutine(controller2.skill1A());
            controller1.StartCoroutine(controller1.skill1B());
        }
    }
}