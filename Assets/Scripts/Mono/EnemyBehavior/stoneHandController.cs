using oct.cameraControl;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

namespace CombatSystem.boss.stoneperson
{
    public class stoneHandController : MonoBehaviour
    {
       public bool left;
        [SerializeField]
        SkeletonRenderer renderer;
        public Transform player { get {return combatController.Player.transform; } }
        public SkeletonAnimation skeletonAnimation;

        Spine.AnimationState spineAnimationState { get { return skeletonAnimation.AnimationState; } }


       public BoxBullet BoxBullet;

        public MeshRenderer rd;
       public BoxCollider BoxCollider;
        public Rigidbody rb;
        public Transform Self;
        public Transform skill1APosition;
        public Transform spawnStonePosition;
        public GameObject stonePrefab;
        public GameObject shockWave;
        public GameObject HitEffect;
        public GameObject damagecircle;

        public float skill1BHeight;
        public float Scale;
        public float skill3releaseSpeed;
        public float skill3totalHoldTime = 2;
        
        [SpineAnimation]
        public string skill1;
        [SpineAnimation]
        public string idleAnimation;
        [SpineAnimation]
        public string skill2Animation;
        [SpineAnimation]
        public string skill3Animation;
        [SpineAnimation]
        public string skill3Animation2;
        [SpineAnimation]
        public string Failed;
        [SpineAnimation]
        public string fade;
        public bool followCharacter;
        public Transform holdCharaPosition;
        public Vector3 TargetPosition;
        public Vector3 Skill2offsetPosition;
        GameObject currentChara;

        public UnityEvent skill3HitFloor;
        float currentHeight;
        Coroutine currrentC;
        #region skills
        public IEnumerator skill1A()
        {
            followCharacter = false;
            spineAnimationState.SetAnimation(0, idleAnimation, true);
            yield return new WaitForFixedUpdate();
            TargetPosition = skill1APosition.position;
            Self.transform.position= skill1APosition.position;
            rd.enabled = true;
            BoxCollider.enabled = true;
            //setAnimation
        }

        public IEnumerator skill1B()
        {
            spineAnimationState.TimeScale = 1;
            //此时石巨人右手会随着玩家角色的移动而晃动，1.5秒后完成索敌，砸向目前锁定的位置，
            spineAnimationState.SetAnimation(0, idleAnimation, false);
            yield return null;
            rd.enabled = true;
            currentHeight = skill1BHeight;
            followCharacter = true; 
            yield return new WaitForSeconds(1.5f);

            //索敌完成
            followCharacter = false;
            TargetPosition= Self.transform.position;
            damagecircle.transform.position = new Vector3(Self.transform.position.x, 0, Self.transform.position.z);
            damagecircle.SetActive(true);
            damagecircle.GetComponent<damageCircle>().time = 1f;
            yield return new WaitForSeconds(1f);

            spineAnimationState.SetAnimation(0, skill1, false);
            float time = 0.25f;
            float movePosition = skill1BHeight / (time / Time.fixedDeltaTime);
            BoxBullet.active();
            BoxCollider.enabled = true;
            while (time > 0)
            {
                TargetPosition = new Vector3(TargetPosition.x, TargetPosition.y - movePosition, TargetPosition.z);
                time-=Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            skill3HitFloor?.Invoke();
            HitEffect.transform.position = new Vector3(Self.position.x, 0, Self.position.z);
            HitEffect.SetActive(true);
            var g= Instantiate(shockWave);
            g.transform.position = new Vector3(Self.position.x, 0, Self.position.z);
            g.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            BoxBullet.diable();
            yield return new WaitForSeconds(1.2f);
            rd.enabled = false;
            BoxCollider.enabled = false;
            HitEffect.SetActive(false);
            damagecircle.SetActive(false);
        }
        public IEnumerator skill2()
        {
            spineAnimationState.TimeScale = 1;
            followCharacter = false;
            rd.enabled = true;
            BoxCollider.enabled = false;
            yield return new WaitForFixedUpdate();
            if (left) {
                TargetPosition = player.position + Skill2offsetPosition;
            }
            else
            {
                var flip = new Vector3(Skill2offsetPosition.x * -1, Skill2offsetPosition.y, Skill2offsetPosition.z);
                TargetPosition = player.position + flip;
            }
            int index = (MainCameraController.Instance.Movement3d as CameraMovementThreeDDefault).AddTarget(this.transform);
            (MainCameraController.Instance.Movement3d as CameraMovementThreeDDefault).ChangeWeight(index, 60, 1f);
            yield return new WaitForSeconds(0.7f);
            spineAnimationState.SetAnimation(0, skill2Animation, false);
             GameObject g= Instantiate(stonePrefab);
            g.GetComponent<BoneFollower>().SkeletonRenderer = renderer;
            g.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            (MainCameraController.Instance.Movement3d as CameraMovementThreeDDefault).RemoveTarget(index, 1f);
        }

        public IEnumerator skill3()
        {
            
            spineAnimationState.TimeScale = Scale;
            followCharacter = false;
            rd.enabled = true;
            BoxCollider.enabled = false;
            yield return new WaitForFixedUpdate();
            TargetPosition = player.position;
            yield return new WaitForSeconds(0.7f);
            yield return new WaitForSeconds(0.1f);
            spineAnimationState.SetAnimation(0, skill3Animation, false);
            float searchTime = 0;
            GameObject Target=null;
            while (searchTime<1)
            {
                yield return new WaitForFixedUpdate();
                searchTime+=Time.fixedDeltaTime;
                if (currentChara!=null)
                {
                    Debug.Log("called"); 
                    DamageObject hardDamage = new DamageObject();
                    hardDamage.hardness = 10000;
                    Debug.Log("!!!!");
                    (currentChara.GetComponent(typeof(DamageTarget)) as DamageTarget).Damage(hardDamage,CombatColor.empty);
                    Target = currentChara;
                    break;
                }
            }
            if (Target == null||!combatController.CharacterStates.Contains(characterState.fly))
            {
                spineAnimationState.SetAnimation(0, Failed, false);
                yield break;    
            }
            rd.sortingOrder += 1;
            yield return new WaitForFixedUpdate();
            var rb = Target.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            float holdTime = 0;
            spineAnimationState.AddAnimation(0, skill3Animation2, false,0);
            while (holdTime < skill3totalHoldTime)
            {
                yield return new WaitForFixedUpdate();
                holdTime += Time.fixedDeltaTime;

                rb.MovePosition(holdCharaPosition.position+Vector3.forward*2f);
            }
            skill3HitFloor?.Invoke();
            rb.isKinematic = false;
            yield return new WaitForFixedUpdate();
            rb.velocity = new Vector3(0, skill3releaseSpeed, 0);
            rb.useGravity = true;
        }

        public void getCharacter(Collider c)
        {
            if (c.GetComponent(typeof(DamageTarget))!=null) {
                currentChara = c.gameObject;
            }
        }

        public void lostCharacter(Collider c)
        {
            if (c.gameObject== currentChara)
            {
                currentChara = null;
            }
        }

        public void Dofade()
        {
            spineAnimationState.SetAnimation(0, fade, false);
        }
        #endregion
        #region testSkill
        public IEnumerator trySkill2()
        {
            yield return new WaitForSeconds(2f);
            while (true)
            {
                StartCoroutine(skill2());
                yield return new WaitForSeconds(5f);
            }
        }
      public IEnumerator trySkill1()
        {
            yield return new WaitForSeconds(2f);
            while (true)
            {
                StartCoroutine(skill1A());
                yield return new WaitForSeconds(5f);
            
            }
        }

        public IEnumerator trySkill3()
        {
            yield return new WaitForSeconds(2f);
            while (true)
            {
                StartCoroutine(skill3());
                yield return new WaitForSeconds(7f);

            }

        }



        public void chageTo1()
        {
            if (currrentC!=null)
            {
                StopCoroutine(currrentC);
            }
            currrentC= StartCoroutine(trySkill1());
        }
        public void chageTo2()
        {
            if (currrentC != null)
            {
                StopCoroutine(currrentC);
            }
            currrentC = StartCoroutine(trySkill2());
        }
        public void chageTo3()
        {
            if (currrentC != null)
            {
                StopCoroutine(currrentC);
            }
            currrentC = StartCoroutine(trySkill3());
        }

        #endregion        #endregion

        private void Start()
        {
            Dofade();
        }
        private void FixedUpdate()
        {
            if (followCharacter)
            {
                rb.MovePosition(new Vector3(player.position.x, currentHeight, player.position.z));
            }
            else
            {
                Self.position = TargetPosition;

            }
        }
    }
}