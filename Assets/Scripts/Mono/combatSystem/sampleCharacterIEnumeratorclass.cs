using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;
using Unity.Burst.Intrinsics;
using CombatSystem;
using UnityEngine.Events;

namespace oct.ObjectBehaviors
{

    public class sampleCharacterIEnumeratorclass : MoveableControlCoroutine
    {

        [SpineAnimation]
        public string flyanimation;
        [SpineAnimation]
        public string flyanimation2;
        [SerializeField]
        PlayerAttribute PlayerControllerr;
        [SerializeField]
        IndividualProperty property;
        public float speed;
        [SpineAnimation]
        public  string runa;
        [SpineAnimation]
        public string idleb;
        [SpineAnimation]
        public string stun;
        [SpineAnimation]
        public string dashname;
        [SpineAnimation]
        public string dashSuccessfulName;
        [SpineAnimation]
        public string atk1;
        [SerializeField]
        private float atk1Distance;
        [SerializeField]
        private float[] atk1time;
        [SpineAnimation]
        public string atk2;
        [SerializeField]
        private float atk2Distance;
        [SpineAnimation]
        public string atk3;
        [SerializeField]
        private float atk3Distance;
        public GameObject bulletPrefeb;
        public GameObject boostBullet;
        public Transform bulletPosition;

        [SpineAnimation]
        public string skillAnimation;
        [SerializeField]
        public GameObject skillBullet;
        [SerializeField]
        public float skillDuration;

        [SpineAnimation]
        public string UltraAnimation;
        [SerializeField]
        public GameObject UltraBullet;
        [SerializeField]
        public float UltraDuration;

        [SpineAnimation]
        public string ChargeProcessAnimation;
        [SpineAnimation]
        public string ChargedAtkProcessAnimation;
        public int combo { get; private set; }
        private int _combo;
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
            PlayerControllerr.speed = speed;
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
            PlayerControllerr.createBullet(bulletPrefeb, bulletPosition, 0.07f);
            if (property.colorBar>=1)
            {
                property.gainColor(-1);
            }
            PlayerControllerr.SetAnimation(ChargedAtkProcessAnimation);
            yield return new WaitForSeconds(0.5f);
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
            yield return new WaitForSecondsRealtime(atk1time[0]);
            combo += 1;
            onComboChange?.Invoke("combo", combo);
            PlayerControllerr.speed = atk1Distance/(atk1time[1] * Time.timeScale);
            PlayerControllerr.SetAnimationNoRepeate(atk1);

            yield return new WaitForSecondsRealtime(atk1time[1]);
            if (!boost)
            {
                PlayerControllerr.createBullet(bulletPrefeb, bulletPosition, 0.07f);
            }
            else
            {
                PlayerControllerr.createBullet(boostBullet, bulletPosition, 0.07f);
            }

            PlayerControllerr.speed =  (0);
            yield return new WaitForSecondsRealtime(atk1time[2]);
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
            combo += 1;
            onComboChange?.Invoke("combo", combo);
            float time = 0.3f;
            PlayerControllerr.speed = atk2Distance / (time * Time.timeScale);
            PlayerControllerr.SetAnimationNoRepeate(atk2);
            
            yield return new WaitForSecondsRealtime(time);
            if (!boost)
            {
                PlayerControllerr.createBullet(bulletPrefeb, bulletPosition, 0.07f);
            }
            else
            {
                PlayerControllerr.createBullet(boostBullet, bulletPosition, 0.07f);
            }
            yield return new WaitForSecondsRealtime(0.7f-time);
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
            combo = 0;
            onComboChange?.Invoke("combo", combo);
            float time = 0.3f;
            PlayerControllerr.speed = atk3Distance / (time * Time.timeScale);
            PlayerControllerr.SetAnimationNoRepeate(atk3);

            yield return new WaitForSecondsRealtime(time);
            if (!boost)
            {
                PlayerControllerr.createBullet(bulletPrefeb, bulletPosition, 0.07f);
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
        public IEnumerator E()
        {
            lockState(true);
            PlayerControllerr.speed = 0;
            PlayerControllerr.SetAnimation(skillAnimation);
            PlayerControllerr.createBullet(skillBullet, bulletPosition, 0.07f);
            yield return new WaitForSecondsRealtime(skillDuration);
            lockState(false);

        }

        public IEnumerator ultraSkill()
        {
            lockState(true);
            PlayerControllerr.property.GainSp(-70);
            PlayerControllerr.speed = 0;
            PlayerControllerr.SetAnimation(UltraAnimation);
            PlayerControllerr.createBullet(UltraBullet, bulletPosition, 0.07f);
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
            PlayerControllerr.SetAnimation(stun);
            PlayerControllerr.UpdateVelocity = false;
            yield return new WaitForFixedUpdate();
            PlayerControllerr.Rigidbody.AddForce(flydirection/2);
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
            while (time < 0.2 || !PlayerControllerr.grounded)
            {
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
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
            dashEffect =true;
            lockState(true);
            yield return null;
            float time=0;
            PlayerControllerr.speed = 0;
            Vector3 target = new Vector3(5,0,0);
            if (PlayerControllerr.faceRight)
            {
                target = target * -1;
            }
            PlayerControllerr.SetAnimation(dashname);
            PlayerControllerr.dash(true);
            while (time < 0.3)
            {
                yield return null;
                time += Time.unscaledDeltaTime;
                PlayerControllerr.positionBox.transform.localPosition = Vector3.Lerp(Vector3.zero, target,time/0.3f);
            }
            PlayerControllerr.DamageBox.transform.position = PlayerControllerr.positionBox.transform.position;
            PlayerControllerr.positionBox.transform.localPosition = Vector3.zero;
            PlayerControllerr.dash(false);
            if (!dashEffect)
            {
                PlayerControllerr.SetAnimation(dashSuccessfulName);
            }
            yield return new WaitForSecondsRealtime(0.3f);
            lockState(false);
        }



        public IEnumerator successfulDash()
        {
            PlayerControllerr.SetAnimationTimeScale(1);
            if (dashEffect) {
                yield return null;
                dashEffect=false;
            }
            
        }
        #endregion
    }
}