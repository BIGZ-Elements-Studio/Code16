using oct.cameraControl;
using Spine;
using Spine.Unity;
using System.Collections;
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
        public Transform S1_1Position;
        public Transform skill4Position;
        public Transform spawnStonePosition;
        public GameObject stonePrefab;
        public GameObject shockWave;
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
        [SpineAnimation]
        public string skill4Animation1;
        [SpineAnimation]
        public string skill4Animation2;

        [SpineEvent]
        public string skill4_1;
        [SpineEvent]
        public string skill4_2;
        public bool followCharacter;
        public Transform holdCharaPosition;
        public Vector3 TargetPosition;
        public Vector3 Skill2offsetPosition;
        GameObject currentChara;

        public UnityEvent skill3HitFloor;
        float currentHeight;
        Coroutine currrentC;
        #region skills
        Coroutine skill4routine;
        public bool inSkill;
        public GameObject CrashGroundCircle;
        public float groundHeight;

        public GameObject CanBeHitEffect;
        public GameObject RedEffect;
        public UnityEvent currentSkillFinish;
        public UnityEvent<bool> ThrowPlayerSuccessful;
        public UnityEvent reciveAtk;


        public void ReciveAtk()
        {
            reciveAtk?.Invoke();
        }
        private void HandleEvent(TrackEntry trackEntry, Spine.Event e)
        {
            if (e.Data.Name == skill4_1)
            {
                Debug.Log("1");
                skill4routine= StartCoroutine(ThrowPlayer_2());
            }
            else if (e.Data.Name == skill4_2)
            {
                Debug.Log("2");
                StopCoroutine(skill4routine);
                StartCoroutine(ThrowPlayer_3());
            }
        }
        public IEnumerator HandOnGround(Vector3 targetWorldTransform)
        {
            CanBeHitEffect.SetActive(true);
            BoxBullet.enabled=false;
            spineAnimationState.TimeScale = 1;
            followCharacter = false;
            spineAnimationState.SetAnimation(0, idleAnimation, true);
            yield return new WaitForFixedUpdate();
            TargetPosition =new Vector3( targetWorldTransform.x, groundHeight, targetWorldTransform.z);
            Self.transform.position= targetWorldTransform;
            rd.enabled = true;
            BoxCollider.enabled = true;
            //setAnimation
        }


        public Vector3 crashGround_1Position { get { return new Vector3(player.position.x, skill1BHeight+4, player.position.z); } }
        public IEnumerator crashGround_1()
        {
            CanBeHitEffect.SetActive(false);
            //4s
            inSkill =true;
            spineAnimationState.TimeScale = 1;
            //此时石巨人右手会随着玩家角色的移动而晃动，1.5秒后完成索敌，砸向目前锁定的位置，
            spineAnimationState.SetAnimation(0, idleAnimation, true);
            yield return null;
            rd.enabled = true;
            currentHeight = skill1BHeight;
            followCharacter = true;
            float timeCount = 0;
            float speed=8;
            rb.MovePosition(new Vector3(player.position.x, currentHeight+ groundHeight, player.position.z));
           yield return new WaitForFixedUpdate();
            
            while (timeCount < 1.5)
            {
                Vector3 Distance = new Vector3(player.position.x, currentHeight + groundHeight, player.position.z)-rb.transform.position;
                Vector3 direction= Distance.normalized;
                Vector3 displacement = direction * speed * Time.deltaTime;
                if (displacement.magnitude> Distance.magnitude)
                {
                    rb.MovePosition(rb.transform.position + Distance);
                }
                else
                {
                    rb.MovePosition(rb.transform.position+ displacement);
                }

                timeCount += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
            //yield return new WaitForSeconds(1.5f);

            //索敌完成
            followCharacter = false;
            TargetPosition= Self.transform.position;
            yield return new WaitForSeconds(1f);
            BoxBullet.enabled = true;
            spineAnimationState.SetAnimation(0, skill1, false);
            float time = 0.25f;
            BoxCollider.enabled = true;
            while (time > 0)
            {
                float TargetY = Mathf.Lerp( skill1BHeight, groundHeight - 1.42f, 1-(time/ 0.25f)* (time / 0.25f));
                TargetPosition = new Vector3(TargetPosition.x, TargetY, TargetPosition.z);


                
                time-=Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            skill3HitFloor?.Invoke();
            var g= Instantiate(shockWave);
            g.transform.position = new Vector3(Self.position.x, groundHeight, Self.position.z);
            g.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            BoxBullet.enabled = false;
            yield return new WaitForSeconds(1.2f);
            BoxCollider.enabled = false;
            inSkill=false;
            currentSkillFinish?.Invoke();
        }

        public IEnumerator crashGround_2(Vector3 Target)
        {
            CanBeHitEffect.SetActive(false);
            //4s
            inSkill = true;
            spineAnimationState.TimeScale = 1;
            //此时石巨人右手会随着玩家角色的移动而晃动，1.5秒后完成索敌，砸向目前锁定的位置，

            //索敌完成
            followCharacter = false;
            TargetPosition = Target;
            damagecircle.transform.position = new Vector3(Target.x, groundHeight, Target.z);
            damagecircle.SetActive(true);
            damagecircle.GetComponent<damageCircle>().time = 1f;
            //  yield return new WaitForSeconds(1f);
            BoxBullet.enabled = true;
            spineAnimationState.SetAnimation(0, skill1, false);
            float time = 0.25f;
            BoxCollider.enabled = true;
            while (time > 0)
            {
                float TargetY = Mathf.Lerp(skill1BHeight, groundHeight - 1.42f, 1 - (time / 0.25f) * (time / 0.25f));
                TargetPosition = new Vector3(TargetPosition.x, TargetY, TargetPosition.z);



                time -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            skill3HitFloor?.Invoke();
            var g = Instantiate(shockWave);
            g.transform.position = new Vector3(Self.position.x, groundHeight, Self.position.z);
            g.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            BoxBullet.enabled = false;
            yield return new WaitForSeconds(1.2f);
            BoxCollider.enabled = false;
            inSkill = false;
            currentSkillFinish?.Invoke();
        }

        public Vector3 ThrowStonePosition { get {
                Vector3 final;
                if (left)
                {
                    final = player.position + Skill2offsetPosition;
                }
                else
                {
                    var flip = new Vector3(Skill2offsetPosition.x * -1, Skill2offsetPosition.y, Skill2offsetPosition.z);
                    final = player.position + flip;
                }
                return final;
            } }

        public IEnumerator ThrowStone()
        {
            CanBeHitEffect.SetActive(false);
            RedEffect.SetActive(true);
            inSkill = true;
            spineAnimationState.TimeScale = 1;
            followCharacter = false;
            rd.enabled = true;
            BoxCollider.enabled = false;
            BoxBullet.enabled=false;
            yield return new WaitForFixedUpdate();
            
            TargetPosition = ThrowStonePosition;
            int index = (MainCameraController.Instance.Movement3d as CameraMovementThreeDDefault).AddTarget(this.transform);
          //  (MainCameraController.Instance.Movement3d as CameraMovementThreeDDefault).ChangeWeight(index, 60, 1f);
            yield return new WaitForSeconds(0.7f);
            spineAnimationState.SetAnimation(0, skill2Animation, false);
             GameObject g= Instantiate(stonePrefab);
            g.GetComponent<BoneFollower>().SkeletonRenderer = renderer;
            g.SetActive(true);
            yield return new WaitForSeconds(1.5f);
           // (MainCameraController.Instance.Movement3d as CameraMovementThreeDDefault).RemoveTarget(index, 1f);
            inSkill = false;
            currentSkillFinish?.Invoke();
            RedEffect.SetActive(false);

        }

        public IEnumerator ThrowPlayer_1()
        {
            CanBeHitEffect.SetActive(false);
            inSkill = true;
            spineAnimationState.TimeScale = Scale;
            damagecircle.transform.position = new Vector3(player.position.x, 0, player.position.z);


            followCharacter = false;
            rd.enabled = true;
            BoxCollider.enabled = false;
            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(0.6f);
            TargetPosition = player.position;
            damagecircle.GetComponent<damageCircle>().time = 1f;
            damagecircle.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            spineAnimationState.SetAnimation(0, skill3Animation, false);
           
        }

        private IEnumerator ThrowPlayer_2()
        {
            inSkill = true;
            float searchTime = 0;
            GameObject Target = null;
            while (searchTime < 0.05)
            {
                yield return new WaitForFixedUpdate();
                searchTime += Time.fixedDeltaTime;
                if (currentChara != null)
                {
                    DamageObject hardDamage = new DamageObject();
                    hardDamage.hardness = 10000;
                    (currentChara.GetComponent(typeof(DamageTarget)) as DamageTarget).Damage(hardDamage, CombatColor.empty);
                    Target = currentChara;
                    break;
                }
            }
            Debug.Log("FINISH SEARCH");
            if (Target == null || !combatController.CharacterStates.Contains(characterState.fly))
            {
                ThrowPlayerSuccessful.Invoke(false);
                spineAnimationState.SetAnimation(0, Failed, false);
                inSkill = false;
                currentSkillFinish?.Invoke();
                yield break;
            }
            //rd.sortingOrder += 1;
            ThrowPlayerSuccessful.Invoke(true);

            yield return new WaitForFixedUpdate();
            skill4HoldRigidBody = Target.GetComponent<Rigidbody>();
            skill4HoldRigidBody.useGravity = false;
            skill4HoldRigidBody.isKinematic = true;
            float holdTime = 0;
            spineAnimationState.AddAnimation(0, skill3Animation2, false, 0);
            while (true)
            {
                yield return new WaitForFixedUpdate();
                holdTime += Time.fixedDeltaTime;
                skill4HoldRigidBody.MovePosition(holdCharaPosition.position + Vector3.forward * 2f);
            }
        }
         Rigidbody skill4HoldRigidBody;
        private IEnumerator ThrowPlayer_3()
        {
            inSkill = true;
            skill3HitFloor?.Invoke();
            skill4HoldRigidBody.isKinematic = false;
            yield return new WaitForFixedUpdate();
            skill4HoldRigidBody.velocity = new Vector3(0, skill3releaseSpeed, 0);
            skill4HoldRigidBody.useGravity = true;
            inSkill = false;
            currentSkillFinish?.Invoke();
        }
        public IEnumerator SpawnVine()
        {
            CanBeHitEffect.SetActive(false);
            spineAnimationState.TimeScale = 1;
            followCharacter = false;
            spineAnimationState.SetAnimation(0, skill4Animation1, false);
            spineAnimationState.AddAnimation(0, skill4Animation2, true,0);
            yield return new WaitForFixedUpdate();
            TargetPosition = skill4Position.position;
            Self.transform.position = skill4Position.position;
            rd.enabled = true;
            BoxCollider.enabled = false;
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
            CanBeHitEffect.SetActive(false);
            BoxCollider.enabled=false;
            spineAnimationState.SetAnimation(0, fade, false);
        }
        #endregion
        #region testSkill
        public IEnumerator trySkill2()
        {
            yield return new WaitForSeconds(2f);
            while (true)
            {
                StartCoroutine(ThrowStone());
                yield return new WaitForSeconds(5f);
            }
        }
      public IEnumerator trySkill1()
        {
            yield return new WaitForSeconds(2f);
            while (true)
            {
               // StartCoroutine(HandOnGround());
                yield return new WaitForSeconds(5f);
            
            }
        }

        public IEnumerator trySkill3()
        {
            yield return new WaitForSeconds(2f);
            while (true)
            {
                StartCoroutine(ThrowPlayer_1());
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
            skeletonAnimation.AnimationState.Event += HandleEvent;
        }

        private void FixedUpdate()
        {
            if (followCharacter)
            {
                //rb.MovePosition(new Vector3(player.position.x, currentHeight, player.position.z));
            }
            else
            {
                Self.position = TargetPosition;

            }
        }
    }
}