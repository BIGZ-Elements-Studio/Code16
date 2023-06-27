using System.Collections;
using UnityEngine;
using Spine.Unity;
using BehaviorControlling;
using Vector3 = UnityEngine.Vector3;
using CombatSystem;
using oct.EnemyMovement;

namespace oct.ObjectBehaviors
{
    public class OpctoMonsterCoroutine : MoveableControlCoroutine
    {
        [SpineAnimation]
        public string moveName;
        public float moveSpeed;
        [SerializeField]
        MonsterAttribute controller;
        [SerializeField]
        SeekBehaviour behaviorController;
        [SpineAnimation]
        public string AtkName;

        public GameObject BulletPrefeb;
        [SerializeField]
        BehaviorController b;
        public Vector3 HitDirectiom;
        private void Start()
        {
            StartCoroutine(RandomAtk());
        }
        public void recevedForce(Vector3 vector3)
        {
            HitDirectiom = vector3;
        }
        private IEnumerator RandomAtk()
        {
            while (true)
            {
                
                    yield return new WaitForSeconds(Random.value*3+3);
                if (behaviorController.distance >= 7)
                {
                    b.setBoolVariable("¹¥»÷", true);
                    yield return null;
                    b.setBoolVariable("¹¥»÷", false);
                }
            }
        }
        public IEnumerator move()
        {
            bool d;
            controller.SetAnimation(moveName);
            if (Random.value > 0.5)
            {
                d =true;
            }
            else
            {
                d=false;
            }
            
            while (true)
            {

                
                if (behaviorController.distance<15&& behaviorController.distance >7)
                {
                    behaviorController.roughness = 5;
                    behaviorController.maxangle = 90;
                    float degree = 1-(behaviorController.distance - 7) / (15 - 7);
                    if (d) {
                        behaviorController.deflectAngle = degree * 90;
                    }
                    else
                    {
                        behaviorController.deflectAngle = -degree * 90;

                    }
                    controller.velocity = moveSpeed;
                }
                else if (behaviorController.distance <= 7)
                {
                    controller.velocity = moveSpeed;
                    behaviorController.maxangle = 30;
                    behaviorController.roughness = 2;
                    behaviorController.deflectAngle = 180;
                }
                else
                {
                    behaviorController.roughness = 5;
                    behaviorController.maxangle = 90;
                    behaviorController.deflectAngle = 0;
                    controller.velocity = moveSpeed;
                }
                
                yield return null;
            }

        }
        public IEnumerator hited()
        {
            lockState(true);
            controller.controlVelocity = false;
            yield return new WaitForFixedUpdate();
            controller.rb.AddForce(HitDirectiom);
            float time = 0;
            while (time < 0.2 || !controller.isGrounded())
            {
                yield return new WaitForFixedUpdate();
                time += Time.fixedDeltaTime;
            }
            controller.controlVelocity = true;
            lockState(false);
        }
        public IEnumerator Atk()
        {
            yield return null;
            lockState(true);
            controller.velocity = 0;
            controller.SetAnimation(AtkName);
            yield return new WaitForSeconds(1f);
            GameObject g = Instantiate(BulletPrefeb);
            g.transform.position = new Vector3(combatController.Player.transform.position.x, 2.68f, combatController.Player.transform.position.z);
            lockState(false);
        }

        public IEnumerator Wondering()
        {
            controller.velocity  = moveSpeed;
            controller.SetAnimation(moveName);
            while (true)
            {
                behaviorController.roughness = 5;
                behaviorController.maxangle = 360;
            }
        }
    }
}