using CombatSystem;
using CombatSystem.boss.stoneperson;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace CombatSystem.boss.stoneperson
{
    public class stoneThrowStone : MonoBehaviour
    {
        public static UnityEvent HitBack = new UnityEvent();
        public GameObject target { get { return combatController.Player; } }
        public GameObject flipTarget;
        public GameObject self;
        public GameObject effect;
        public GameObject Breakeffect;
        public Rigidbody selfrb;
        public Transform initialPosition;
        public SphereBullet bullet;

        public BoneFollower handFollower;
        Coroutine come;
        public SkeletonAnimation skeletonAnimation;
        Spine.AnimationState spineAnimationState { get { return skeletonAnimation.AnimationState; } }
        [SpineAnimation]
        public string breakAnimation;
        [SpineAnimation]
        public string idle;
        bool Hitable;
        public Transform thisTramsform;
        public LayerMask groundLayer;
        public List<Rigidbody> bodies;
        public float magnitude;
        public UnityEvent breakStone;
        public GameObject DetectAreas;
        public int initDamage;
        public int ReverseDamage;
        public void StoneBreak()
        {
            Hitable = false;
            StopAllCoroutines();
            spineAnimationState.SetAnimation(0, breakAnimation, true);

            foreach (var body in bodies)
            {
                body.isKinematic = false;
                body.useGravity = true;
                body.velocity = ((new Vector3(Random.Range(-3, 3), Random.Range(2, 5), 0)) * magnitude);
            }
            breakStone?.Invoke();
            DistoryAfterSeconds.Destroy(self, 0.5f);
        }
        public float spinspeed;
        public IEnumerator process()
        {

            handFollower.enabled = true;
            //spineAnimationState.SetAnimation(0,idle,true);
            yield return new WaitForSeconds(0.6f);
            Vector3 distance = target.transform.position - thisTramsform.position;
            Hitable = true;
            bullet.affectPlayer = true;
            bullet.affectEnemy = false;
            bullet.active();


            handFollower.enabled = false;

            float FlyTime = 1f;
            Vector3 DistanceEachTime = distance / (FlyTime / Time.fixedDeltaTime);
            float passedTime = 0;
            while (true)
            {
                yield return new WaitForFixedUpdate();
                passedTime += Time.fixedDeltaTime;
                selfrb.MovePosition(thisTramsform.position + DistanceEachTime);
                DetectAreas.transform.position = thisTramsform.position;

            }
            yield return new WaitForSeconds(0.5f);
            // Destroy(self);
        }

        private void Start()
        {
            bullet.damagePercent = initDamage;
            selfrb.maxAngularVelocity = 50;
            spineAnimationState.SetAnimation(0, idle, true);
            come = StartCoroutine(process());

        }
        private void FixedUpdate()
        {
            selfrb.angularVelocity = new Vector3(0, 0, 1) * spinspeed;
        }
        public void release()
        {
            handFollower.enabled = false;
            bullet.active();
            c = StartCoroutine(process());
        }
        Coroutine c;
        public void HitPlayer(bool i)
        {

            if (!Hitable)
            {
                Debug.Log(1111);
                control.hit();
            }
            Hitable = false;
            StoneBreak();
        }
        public EnemyHandsControl control;
        public Coroutine breakRoutine;
        public void OnTriggerEnter(Collider other)
        {

            if (groundLayer == (groundLayer | (1 << other.gameObject.layer)))
            {
                StopCoroutine(come);
                breakStone?.Invoke();
                selfrb.maxAngularVelocity = 0;
                GameObject g = Instantiate(effect);
                g.transform.position = transform.position + Vector3.down;
                breakRoutine = StartCoroutine(Break());
            }
        }
        IEnumerator Break()
        {
            bullet.diable();
            yield return new WaitForSeconds(2);
            GameObject g = Instantiate(Breakeffect);
            g.transform.position = transform.position + Vector3.down;
            StoneBreak();
        }
        public IEnumerator flipProcess()
        {
            bullet.damagePercent = ReverseDamage;
            yield return new WaitForFixedUpdate();
            selfrb.maxAngularVelocity = 50;
            bullet.affectPlayer = false;
            bullet.affectEnemy = true;
            Vector3 distance = flipTarget.transform.position - thisTramsform.position;
            float FlyTime = 0.7f;
            Vector3 DistanceEachTime = distance / (FlyTime / Time.fixedDeltaTime);
            float passedTime = 0;
            bullet.actived = true;
            while (passedTime < FlyTime)
            {
                passedTime += Time.fixedDeltaTime;
                thisTramsform.position = thisTramsform.position + DistanceEachTime;
                DetectAreas.transform.position = thisTramsform.position;
                yield return new WaitForFixedUpdate();
            }


        }
        public void flip(string s, bool b)
        {
            if (breakRoutine != null)
                StopCoroutine(breakRoutine);
            if (!b || !Hitable)
            {
                return;
            }
            Hitable = false;
            if (come != null)
            {
                StopCoroutine(come);
            }
            Debug.Log("called");
            HitBack?.Invoke();
            StartCoroutine(flipProcess());
        }
    }
}