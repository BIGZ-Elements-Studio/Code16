using CombatSystem.shieldSystem;
using CombatSystem;
using oct.ObjectBehaviors;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace oct.ObjectBehaviors
{
    public class MagicianCoroutine : MoveableControlCoroutine
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
    public string runa;
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
    public string atk4;
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
    public GameObject Atk4Bullet;
    public GameObject shieldEffect;
    public GameObject dashRemainPrefeb;
    [SerializeField]
    Atkpoint point;

    [SerializeField]
    private float atk1Distance;
    [SerializeField]
    private float DashDistance = -5;
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
    public ParticleSystem DashParticleSystem;
    public ParticleSystem ChargeParticleEffect;
    public Vector3 flydirection;
    [SerializeField]
    UnityEvent<string, float> onComboChange;
    [SerializeField]
    UnityEvent<string, bool> finishCharge;
    public void SetFly(Vector3 vector3)
    {
        flydirection = vector3;
    }
    public IEnumerator idle()
    {
        PlayerControllerr.allowFlip = true;
        lockState(false);
        PlayerControllerr.UpdateVelocity = true;
        PlayerControllerr.speed = 0;
        yield return null;
        yield return new WaitForSecondsRealtime(MotionLastingTime);
        PlayerControllerr.SetAnimation(idleb);
        PlayerControllerr.UpdateVelocity = true;
        MotionLastingTime = 0f;
    }

    public IEnumerator run()
    {
        MotionLastingTime = 0f;
        PlayerControllerr.allowFlip = true;
        PlayerControllerr.speed = MoveSpeed;
        PlayerControllerr.SetAnimation(runa);
        lockState(true);
        yield return new WaitForSecondsRealtime(0.1f);
        lockState(false);
        while (true)
        {
            yield return new WaitForFixedUpdate();
            PlayerControllerr.SetAnimationTimeScale(PlayerControllerr.property.moveSpeedFactor);
        }

    }

        #region 蓄力
        public IEnumerator ChargeProcess()
    {
        PlayerControllerr.allowFlip = true;
        PlayerControllerr.SetAnimation(ChargeProcessAnimation);
        PlayerControllerr.speed = 0;

        yield return new WaitForSecondsRealtime(0.6f);
        ChargeParticleEffect.Play();
        yield return new WaitForSecondsRealtime(0.1f);
        finishCharge?.Invoke("蓄力结束", true);
        PlayerControllerr.allowFlip = false;
        yield return new WaitForSecondsRealtime(0.4f);
        finishCharge?.Invoke("蓄力超时", true);
    }
    public IEnumerator ChargedAtk()
    {
        lockState(true);
        yield return null;
        finishCharge?.Invoke("蓄力结束", false);
        finishCharge?.Invoke("蓄力超时", false);
        PlayerControllerr.createBullet(Atk4Bullet, bulletPosition, 0.07f);
        if (property.colorBar >= 1)
        {
            property.gainColor(-1);
        }
        PlayerControllerr.SetAnimation(ChargedAtkProcessAnimation);
        yield return new WaitForSecondsRealtime(0.7f);
        PlayerControllerr.allowFlip = true;
        lockState(false);
    }
        #endregion
        #region 普通攻击
        float MotionLastingTime;
    IEnumerator MoveDuringAtk(float distance, float duration, Vector3 target, bool moveToEnemy)
    {
        yield return new WaitForFixedUpdate();
        PlayerControllerr.UpdateVelocity = false;
        PlayerControllerr.speed = 0;
        PlayerControllerr.allowFlip = false;
        float speed = distance / duration;
        if (target != null && moveToEnemy)
        {
            var distaceBetween = (target - PlayerControllerr.transform.position);
            if (distaceBetween.x < 0.2)
            {
                PlayerControllerr.direction = Vector3.left;
                PlayerControllerr.faceRight = false;
            }
            else if (distaceBetween.x > 0.2)
            {
                PlayerControllerr.direction = Vector3.right;
                PlayerControllerr.faceRight = true;
            }
            if (distaceBetween.magnitude < 6)
            {
            }
            else if (distaceBetween.magnitude - 6 < distance)
            {
                PlayerControllerr.Rigidbody.velocity = new Vector3(distaceBetween.x, 0, distaceBetween.z).normalized * speed;
                yield return new WaitForSecondsRealtime(((Mathf.Clamp((distaceBetween.magnitude - 7.5f), 0, duration)) / speed));
                Debug.Log((distaceBetween.magnitude - 1f));
            }
            else
            {
                PlayerControllerr.Rigidbody.velocity = new Vector3(distaceBetween.x, 0, distaceBetween.z).normalized * speed;
                yield return new WaitForSecondsRealtime(duration);
            }
        }
        else
        {
            PlayerControllerr.Rigidbody.velocity = target.normalized * speed;
            yield return new WaitForSecondsRealtime(duration);
        }
        yield return new WaitForFixedUpdate();
        PlayerControllerr.UpdateVelocity = true;
        PlayerControllerr.allowFlip = true;
    }
    public IEnumerator hit1()
    {
        combatController.TryFindEnemy();
        MotionLastingTime = 0.25f;
        PlayerControllerr.allowFlip = true;
        if (canceling != null)
        {
            StopCoroutine(canceling);
        }
        lockState(true);

        float totalTime = atk1time[0] + atk1time[1] + atk1time[2];
        DamageTarget t = combatController.currentfollowingTarget;
        Vector3 target;
        if (t != null && t.GetlockedEnemyTransform() != null)
        {
            target = t.GetlockedEnemyTransform().position;
            StartCoroutine(MoveDuringAtk(atk1Distance, totalTime - 0.1f, target, true));
        }
        else
        {
            target = new Vector3(atk1Distance * PlayerControllerr.directionMultiplier, 0, 0);
            StartCoroutine(MoveDuringAtk(atk1Distance, totalTime - 0.1f, target, false));
        }

        PlayerControllerr.SetAnimationTimeScale(PlayerControllerr.property.AtkSpeed);
        yield return new WaitForSecondsRealtime(atk1time[0] / PlayerControllerr.property.AtkSpeed);
        combo += 1;
        onComboChange?.Invoke("combo", combo);
        //   PlayerControllerr.speed = 5;
        PlayerControllerr.SetAnimationNoRepeate(atk1);
        yield return new WaitForSecondsRealtime(atk1time[1] / PlayerControllerr.property.AtkSpeed);
        PlayerControllerr.allowFlip = false;
            PlayerControllerr.createBullet(Atk1Bullet, bulletPosition, 0.07f);
        yield return new WaitForSecondsRealtime(atk1time[2] / PlayerControllerr.property.AtkSpeed);
        yield return null;
        canceling = StartCoroutine(cancelCombo());
        PlayerControllerr.allowFlip = true;
        PlayerControllerr.UpdateVelocity = true;
        lockState(false);
    }
    public IEnumerator hit2()
    {
        combatController.TryFindEnemy();
        MotionLastingTime = 0.2f;
        PlayerControllerr.allowFlip = true;
        if (canceling != null)
        {
            StopCoroutine(canceling);
        }
        lockState(true);
        float totalTime = 0.43f;
        DamageTarget t = combatController.currentfollowingTarget;
        Vector3 target;
        if (t != null && t.GetlockedEnemyTransform() != null)
        {
            target = t.GetlockedEnemyTransform().position;
            StartCoroutine(MoveDuringAtk(atk1Distance, totalTime - 0.1f, target, true));
        }
        else
        {
            target = new Vector3(atk1Distance * PlayerControllerr.directionMultiplier, 0, 0);
            StartCoroutine(MoveDuringAtk(atk1Distance, totalTime - 0.1f, target, false));
        }
        PlayerControllerr.SetAnimationTimeScale(PlayerControllerr.property.AtkSpeed);
        combo += 1;
        onComboChange?.Invoke("combo", combo);
        float time = 0.2f;
        //PlayerControllerr.UpdateVelocity = false;
        //  PlayerControllerr.Rigidbody.velocity = new Vector3(atk1Distance * PlayerControllerr.directionMultiplier, 0, 0);
        PlayerControllerr.SetAnimationNoRepeate(atk2);
        yield return new WaitForSecondsRealtime(time / PlayerControllerr.property.AtkSpeed);
        PlayerControllerr.allowFlip = false;

            PlayerControllerr.createBullet(Atk2Bullet, bulletPosition, 0.07f);
        yield return new WaitForSecondsRealtime((0.43f - time) / PlayerControllerr.property.AtkSpeed);
        canceling = StartCoroutine(cancelCombo());
        PlayerControllerr.allowFlip = true;
        //   PlayerControllerr.UpdateVelocity = true;
        lockState(false);
    }
    public IEnumerator hit3()
    {
        combatController.TryFindEnemy();
        MotionLastingTime = 0.2f;
        PlayerControllerr.allowFlip = true;
        if (canceling != null)
        {
            StopCoroutine(canceling);
        }
        lockState(true);
        PlayerControllerr.SetAnimationTimeScale(PlayerControllerr.property.AtkSpeed);
        combo = 0;
        onComboChange?.Invoke("combo", combo);


        DamageTarget t = combatController.currentfollowingTarget;
        Vector3 target;
        float totalTime = 0.7f;
        if (t != null && t.GetlockedEnemyTransform() != null)
        {
            target = t.GetlockedEnemyTransform().position;
            StartCoroutine(MoveDuringAtk(atk1Distance, totalTime - 0.1f, target, true));
        }
        else
        {
            target = new Vector3((atk1Distance + 7) * PlayerControllerr.directionMultiplier, 0, 0);
            StartCoroutine(MoveDuringAtk(atk1Distance, totalTime - 0.1f, target, false));
        }

        float time = 0.25f;
        PlayerControllerr.SetAnimationNoRepeate(atk3);
        yield return new WaitForSecondsRealtime(time);
        PlayerControllerr.allowFlip = false;

            PlayerControllerr.createBullet(Atk3Bullet, bulletPosition, 0.07f);
        yield return new WaitForSecondsRealtime(0.7f - time);
        PlayerControllerr.allowFlip = true;
        PlayerControllerr.UpdateVelocity = true;
        lockState(false);
    }

        public IEnumerator hit4()
        {
            combatController.TryFindEnemy();
            MotionLastingTime = 0.2f;
            PlayerControllerr.allowFlip = true;
            if (canceling != null)
            {
                StopCoroutine(canceling);
            }
            lockState(true);
            PlayerControllerr.SetAnimationTimeScale(PlayerControllerr.property.AtkSpeed);
            combo = 0;
            onComboChange?.Invoke("combo", combo);


            DamageTarget t = combatController.currentfollowingTarget;
            Vector3 target;
            float totalTime = 0.7f;
            if (t != null && t.GetlockedEnemyTransform() != null)
            {
                target = t.GetlockedEnemyTransform().position;
                StartCoroutine(MoveDuringAtk(atk1Distance, totalTime - 0.1f, target, true));
            }
            else
            {
                target = new Vector3((atk1Distance + 7) * PlayerControllerr.directionMultiplier, 0, 0);
                StartCoroutine(MoveDuringAtk(atk1Distance, totalTime - 0.1f, target, false));
            }

            float time = 0.25f;
            PlayerControllerr.SetAnimationNoRepeate(atk3);
            yield return new WaitForSecondsRealtime(time);
            PlayerControllerr.allowFlip = false;
                PlayerControllerr.createBullet(Atk3Bullet, bulletPosition, 0.07f);
            yield return new WaitForSecondsRealtime(0.7f - time);
            PlayerControllerr.allowFlip = true;
            PlayerControllerr.UpdateVelocity = true;
            lockState(false);
        }

        Coroutine canceling;

    IEnumerator cancelCombo()
    {
        yield return new WaitForSecondsRealtime(1);
        combo = 0;
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
        yield return new WaitForSecondsRealtime(UltraDuration);
        lockState(false);

    }

    #endregion
    #region 打断
    public IEnumerator disrupt()
    {
        PlayerControllerr.SetAnimationTimeScale(1);
        lockState(true);
        PlayerControllerr.allowFlip = false;
        PlayerControllerr.speed = 0;
        PlayerControllerr.SetAnimationNoRepeate(stun);
        PlayerControllerr.UpdateVelocity = false;
        yield return new WaitForFixedUpdate();
        PlayerControllerr.Rigidbody.AddForce(new Vector3(flydirection.x / 2, 0, flydirection.z / 2));
        float time = 0;
        while (time < 0.2 || !PlayerControllerr.grounded)
        {
            yield return new WaitForFixedUpdate();
            time += Time.fixedDeltaTime;
        }
        yield return new WaitForSecondsRealtime(0.5f);
        PlayerControllerr.UpdateVelocity = true;
        PlayerControllerr.allowFlip = true;
        lockState(false);
    }

    public IEnumerator fly()
    {
        PlayerControllerr.SetAnimationTimeScale(1);
        lockState(true);
        PlayerControllerr.allowFlip = false;
        PlayerControllerr.speed = 0;
        PlayerControllerr.UpdateVelocity = false;
        PlayerControllerr.Rigidbody.AddForce(flydirection);
        PlayerControllerr.SetAnimation(flyanimation);
        yield return new WaitForFixedUpdate();
        float time = 0;
        bool groundedTimerStarted = false;
        float groundedTimer = 0;
        float waitTime = 0.07f;
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
        PlayerControllerr.SetAnimationNoRepeate(flyanimation2);
        yield return new WaitForSecondsRealtime(1.3f);
        PlayerControllerr.UpdateVelocity = true;
        StartCoroutine(arm());
        PlayerControllerr.allowFlip = true;
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
    public IEnumerator dash()
    {
        PlayerControllerr.allowFlip = true;
        PlayerControllerr.SetAnimationTimeScale(1);
        DashParticleSystem.Play();
        lockState(true);
        GameObject g = Instantiate(dashRemainPrefeb);
        g.transform.position = PlayerControllerr.transform.position;
        g.SetActive(true);
        var spatp = g.GetComponent<SimpleAtkPoint>();
        spatp.ReciveHit.AddListener(setHitForDash);
        yield return null;
        PlayerControllerr.speed = 0;
        Vector3 target = new Vector3(DashDistance, 0, 0) * PlayerControllerr.directionMultiplier;

        Vector3 v = target / 0.2f / Time.timeScale;


        PlayerControllerr.SetAnimationNoRepeate(dashname);
        HitForDash = false;
        PlayerControllerr.dash(true);
        PlayerControllerr.UpdateVelocity = false;
        PlayerControllerr.Rigidbody.velocity = v;
        yield return new WaitForSecondsRealtime(0.05f);
        PlayerControllerr.SetAnimationNoRepeate(dashname2);
        yield return new WaitForSecondsRealtime(0.15f);

        PlayerControllerr.dash(false);
        if (HitForDash)
        {
            PlayerControllerr.SetAnimation(dashSuccessfulName);
        }
        Destroy(g);
        yield return new WaitForSecondsRealtime(0.01f);
        PlayerControllerr.UpdateVelocity = true;
        lockState(false);
    }
    public bool HitForDash;
    public void setHitForDash()
    {
        HitForDash = true;
    }
    #endregion
}
}
