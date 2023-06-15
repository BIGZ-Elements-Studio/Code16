using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;
using Unity.Burst.Intrinsics;
using CombatSystem;

namespace oct.ObjectBehaviors
{

    public class sampleCharacterIEnumeratorclass : MoveableControlCoroutine
    {


        [SerializeField]
        PlayerAttribute PlayerControllerr;
       public float speed;
        [SpineAnimation]
      public  string runa;
        [SpineAnimation]
        public string idleb;
        [SpineAnimation]
        public string atkc;
        [SpineAnimation]
        public string atkD;
        [SpineAnimation]
        public string atkE;
        [SpineAnimation]
        public string stun;
        [SpineAnimation]
        public string knockup;
        [SpineAnimation]
        public string dashname;
        [SpineAnimation]
        public string dashSuccessfulName;
        public GameObject bulletPrefeb;
        public Transform bulletPosition;
        public int combo;

        public Vector3 flydirection;
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
                PlayerControllerr.SetAnimationTimeScale(PlayerControllerr.attackAttributeController.moveSpeedFactor);
            }
            
        }
        #region ��ͨ����
        public IEnumerator hit()
        {
            PlayerControllerr.SetAnimationTimeScale(1);
            if (canceling!=null)
            {
                StopCoroutine(canceling);
            }
            if (combo<3)
            {
                combo += 1;
            }
            else
            {
                combo = 1;
            }
            PlayerControllerr.speed = 0;
            if (combo==1) {
                PlayerControllerr.SetAnimation(atkc);
            }else if (combo == 2)
            {
                PlayerControllerr.SetAnimation(atkD);
            }
            else if (combo == 3)
            {
                PlayerControllerr.SetAnimation(atkE);
            }
            yield return null;
            PlayerControllerr.createBullet(bulletPrefeb, bulletPosition,0.1f);
            lockState(true);
                yield return new WaitForSeconds(1f);
            lockState(false);
            canceling= StartCoroutine(cancelCombo());

        }
        Coroutine canceling;
        
        IEnumerator cancelCombo()
        {
            yield return new WaitForSecondsRealtime(1);
            combo=0;
        }
        #endregion
        #region ���
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
            PlayerControllerr.SetAnimation(knockup);
            yield return new WaitForFixedUpdate();
            float time = 0;
            while (time < 0.2 || !PlayerControllerr.grounded)
            {
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
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
        #region ����
        bool dashEffect;
        public IEnumerator dash()
        {
            PlayerControllerr.SetAnimationTimeScale(1);
            dashEffect =true;
            lockState(true);
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
                Debug.Log("!!!!!!!!!!!!!!!!");
                dashEffect=false;
            }
            
        }
        #endregion
    }
}