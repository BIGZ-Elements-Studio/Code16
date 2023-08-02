using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;
using Unity.Burst.Intrinsics;
using CombatSystem;
using UnityEngine.Events;
using CombatSystem.shieldSystem;
using Unity.VisualScripting;

namespace oct.ObjectBehaviors
{

    public class sampleCharacterIEnumeratorclass : MoveableControlCoroutine
    {
        [SerializeField]
        PlayerAttribute PlayerControllerr;
        [SerializeField]
        IndividualProperty property;
        [SpineAnimation]
        public string flyanimation;
        [SpineAnimation]
        public string flyanimation2;
        [SpineAnimation]
        public  string runa;
        [SpineAnimation]
        public string idleb;
        [SpineAnimation]
        public string stun;
        [SpineAnimation]
        public string dashname;
        [SpineAnimation]
        public string dashname2;
        [SpineAnimation]
        public string dashSuccessfulName;
        [SpineAnimation]
        public string atk1;
        [SpineAnimation]
        public string atk2;
        [SpineAnimation]
        public string atk3;
        [SpineAnimation]
        public string skillAnimation;
        [SpineAnimation]
        public string ChargeProcessAnimation;
        [SpineAnimation]
        public string ChargedAtkProcessAnimation;
        [SpineAnimation]
        public string UltraAnimation;
        public float MoveSpeed;
        public GameObject skillBullet; 
        public GameObject Atk1Bullet;
        public GameObject Atk2Bullet;
        public GameObject Atk3Bullet;
        public GameObject boostBullet;
        public GameObject shieldEffect;
        [SerializeField]
        Atkpoint point;

        [SerializeField]
        private float atk1Distance;
        [SerializeField]
        private float DashDistance=-5;
        [SerializeField]
        private float[] atk1time;
        private float atk2Distance;
        private float atk3Distance;
        [SerializeField]
        public float skillDuration; 
        [SerializeField]
        public float UltraDuration;
        public Transform bulletPosition;





        public int combo { get; private set; }
        public ParticleSystem ParticleSystem;
        public Vector3 flydirection;
        [SerializeField]
        UnityEvent<string, float> onComboChange;
        [SerializeField]
        UnityEvent<string, bool> finishCharge;
        private bool boost;
       public void SetFly(Vector3 vector3)
        {
            flydirection=vector3;
        }
        public IEnumerator idle()
        {
            lockState(false);
            PlayerControllerr.UpdateVelocity = true;
            PlayerControllerr.speed = 0;
            yield return null;
            PlayerControllerr.SetAnimation(idleb); 
        }

        public IEnumerator run()
        {
            PlayerControllerr.speed = MoveSpeed;
            PlayerControllerr.SetAnimation(runa);
            lockState(true);
                yield return new WaitForSeconds(0.1f);
            lockState(false);
            while (true)
                {
                yield return new WaitForFixedUpdate();
                PlayerControllerr.SetAnimationTimeScale(PlayerControllerr.property.moveSpeedFactor);
            }
            
        }

        public IEnumerator ChargeProcess()
        {
            PlayerControllerr.SetAnimation(ChargeProcessAnimation);
            yield return new WaitForSecondsRealtime(0.7f);
            finishCharge?.Invoke("蓄力结束",true) ;
            yield return new WaitForSecondsRealtime(0.4f);
            finishCharge?.Invoke("蓄力超时", true);
        }
        public IEnumerator ChargedAtk()
        {
            lockState(true);
            yield return null;
            finishCharge?.Invoke("蓄力结束", false);
            finishCharge?.Invoke("蓄力超时", false);
            PlayerControllerr.createBullet(Atk1Bullet, bulletPosition, 0.07f);
            if (property.colorBar>=1)
            {
                property.gainColor(-1);
            }
            PlayerControllerr.SetAnimation(ChargedAtkProcessAnimation);
            yield return new WaitForSeconds(0.7f);
            lockState(false);
        } 
        #region 普通攻击
        public IEnumerator hit1()
        {

            if (canceling != null)
            {
                StopCoroutine(canceling);
            }
            lockState(true);
            PlayerControllerr.SetAnimationTimeScale(PlayerControllerr.property.AtkSpeed);
            yield return new WaitForSecondsRealtime(atk1time[0]/ PlayerControllerr.property.AtkSpeed);
            combo += 1;
            onComboChange?.Invoke("combo", combo);
            PlayerControllerr.speed = atk1Distance/(atk1time[1] * Time.timeScale);
            PlayerControllerr.SetAnimationNoRepeate(atk1);
            yield return new WaitForSecondsRealtime(atk1time[1] / PlayerControllerr.property.AtkSpeed);
            if (!boost)
            {
                PlayerControllerr.createBullet(Atk1Bullet, bulletPosition, 0.07f);
            }
            else
            {
                PlayerControllerr.createBullet(boostBullet, bulletPosition, 0.07f);
            }

            PlayerControllerr.speed =  (0);
            yield return new WaitForSecondsRealtime(atk1time[2] / PlayerControllerr.property.AtkSpeed);
            canceling = StartCoroutine(cancelCombo());
            lockState(false);
        }
        public IEnumerator hit2()
        {
            if (canceling != null)
            {
                StopCoroutine(canceling);
            }
            lockState(true);
            PlayerControllerr.SetAnimationTimeScale(PlayerControllerr.property.AtkSpeed);
            combo += 1;
            onComboChange?.Invoke("combo", combo);
            float time = 0.3f;
            PlayerControllerr.speed = atk2Distance / (time * Time.timeScale);
            PlayerControllerr.SetAnimationNoRepeate(atk2);
            yield return new WaitForSecondsRealtime(time / PlayerControllerr.property.AtkSpeed);
            if (!boost)
            {
                PlayerControllerr.createBullet(Atk2Bullet, bulletPosition, 0.07f);
            }
            else
            {
                PlayerControllerr.createBullet(boostBullet, bulletPosition, 0.07f);
            }
            yield return new WaitForSecondsRealtime((0.7f-time) / PlayerControllerr.property.AtkSpeed);
            canceling = StartCoroutine(cancelCombo());
            lockState(false);
        }
        public IEnumerator hit3()
        {
            if (canceling != null)
            {
                StopCoroutine(canceling);
            }
            lockState(true);
            PlayerControllerr.SetAnimationTimeScale(PlayerControllerr.property.AtkSpeed);
            combo = 0;
            onComboChange?.Invoke("combo", combo);
            float time = 0.3f;
            PlayerControllerr.speed = atk3Distance / (time * Time.timeScale);
            PlayerControllerr.SetAnimationNoRepeate(atk3);

            yield return new WaitForSecondsRealtime(time);
            if (!boost)
            {
                PlayerControllerr.createBullet(Atk3Bullet, bulletPosition, 0.07f);
            }
            else
            {
                PlayerControllerr.createBullet(boostBullet, bulletPosition, 0.07f);
            }
            yield return new WaitForSecondsRealtime(0.7f - time);
            lockState(false);
        }

        Coroutine canceling;
        
        IEnumerator cancelCombo()
        {
            yield return new WaitForSecondsRealtime(1);
            combo =0;
            onComboChange?.Invoke("combo", combo);
        }
        #endregion
        #region 技能

        GameObject shield;
        public IEnumerator E()
        {
            lockState(true);
            PlayerControllerr.speed = 0;
            PlayerControllerr.SetAnimation(skillAnimation);
            PlayerControllerr.createBullet(skillBullet, bulletPosition, 0.07f);
            yield return new WaitForSecondsRealtime(skillDuration);
            shield s = new shield(150, false, 10);
            point.addShield(s);
            shield = Instantiate(shieldEffect);
            shield.SetActive(true);
            s.shieldBreak.AddListener(delegate { Destroy(shield); });
            s.shieldReplaced.AddListener(delegate { Destroy(shield); });
            lockState(false);

        }

        public IEnumerator ultraSkill()
        {
            lockState(true);
            PlayerControllerr.property.GainSp(-70);
            PlayerControllerr.speed = 0;
            PlayerControllerr.SetAnimation(UltraAnimation);
            StartCoroutine(boostProcess());
            yield return new WaitForSecondsRealtime(UltraDuration);
            lockState(false);

        }

        private IEnumerator boostProcess()
        {
            boost = true;
            yield return new WaitForSecondsRealtime(10f);
            boost = false;
        }
        #endregion
        #region 打断
        public IEnumerator disrupt()
        {
            PlayerControllerr.SetAnimationTimeScale(1);
            lockState(true);
            PlayerControllerr.speed = 0;
            PlayerControllerr.SetAnimationNoRepeate(stun);
            PlayerControllerr.UpdateVelocity = false;
            yield return new WaitForFixedUpdate();
            PlayerControllerr.Rigidbody.AddForce(new Vector3( flydirection.x / 2,0,  flydirection.z/2));
            float time = 0;
            while (time < 0.2 || !PlayerControllerr.grounded)
            {
                yield return new WaitForFixedUpdate();
                time += Time.fixedDeltaTime;
            }
            yield return new WaitForSecondsRealtime(0.5f);
            PlayerControllerr.UpdateVelocity = true;
            lockState(false);
        }

        public IEnumerator fly()
        {
            PlayerControllerr.SetAnimationTimeScale(1);
            lockState(true);
            PlayerControllerr.speed = 0;
            PlayerControllerr.UpdateVelocity = false;
            PlayerControllerr.Rigidbody.AddForce(flydirection);
            PlayerControllerr.SetAnimation(flyanimation);
            yield return new WaitForFixedUpdate();
            float time = 0;
            bool groundedTimerStarted = false;
            float groundedTimer = 0;
            float waitTime = 0.07f;
            Debug.Log("fly");
            while (time < 0.2 || groundedTimer < waitTime)
            {
                time += Time.fixedDeltaTime;
                if (PlayerControllerr.grounded)
                {
                    if (!groundedTimerStarted)
                    {
                        groundedTimerStarted = true;
                        groundedTimer = 0;
                    }
                    else
                    {
                        groundedTimer += Time.fixedDeltaTime;
                    }
                }
                else
                {
                    groundedTimerStarted = false;
                    groundedTimer = 0;
                }
                yield return new WaitForFixedUpdate();
            }
            Debug.Log("end");
            PlayerControllerr.SetAnimationNoRepeate(flyanimation2);
            yield return new WaitForSecondsRealtime(1.3f);
            PlayerControllerr.UpdateVelocity = true;
            StartCoroutine(arm());
            lockState(false);
        }
        private IEnumerator arm()
        {
            PlayerControllerr.armoed(true);
            yield return new WaitForSecondsRealtime(1f);
            PlayerControllerr.armoed(false);
        }
        #endregion
        #region 闪避
        bool dashEffect;
        public IEnumerator dash()
        {

            PlayerControllerr.SetAnimationTimeScale(1);
            ParticleSystem.Play();
            dashEffect =true;
            lockState(true);
            yield return null;
            PlayerControllerr.speed = 0;
            Vector3 target = new Vector3(DashDistance, 0,0);
            if (PlayerControllerr.faceRight)
            {
                target = target * -1;
            }
            Vector3 v = target / 0.3f;


            PlayerControllerr.SetAnimationNoRepeate(dashname);
            HitForDash=false;
            PlayerControllerr.dash(true);
            PlayerControllerr.Collider.isTrigger = true;
            PlayerControllerr.Rigidbody.isKinematic = true;
            Rigidbody rb= PlayerControllerr.positionBox.AddComponent<Rigidbody>();
            yield return null;
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.freezeRotation = true;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode=CollisionDetectionMode.Continuous;
            rb.velocity = v;
            yield return new WaitForFixedUpdate();
            PlayerControllerr.PositionCollider.isTrigger = false;
            yield return new WaitForSecondsRealtime(0.1f);
            PlayerControllerr.SetAnimationNoRepeate(dashname2);
            yield return new WaitForSecondsRealtime(0.3f);
            Destroy(rb);
            PlayerControllerr.PositionCollider.isTrigger = true;
            PlayerControllerr.DamageBox.transform.position = PlayerControllerr.positionBox.transform.position;
            PlayerControllerr.positionBox.transform.localPosition = Vector3.zero;
            PlayerControllerr.Collider.isTrigger = false;
            PlayerControllerr.Rigidbody.isKinematic = false;
            PlayerControllerr.dash(false);
            if (HitForDash)
            {
                PlayerControllerr.SetAnimation(dashSuccessfulName);
            }

            yield return new WaitForSecondsRealtime(0.3f);
            lockState(false);
        }
        public bool HitForDash;
        public void setHitForDash()
        {
            HitForDash = true;
        }

        public IEnumerator successfulDash()
        {
            Debug.Log("called");
            PlayerControllerr.SetAnimationTimeScale(1);
            if (dashEffect) {
                yield return null;
                dashEffect=false;
            }
            
        }
        #endregion
    }
}