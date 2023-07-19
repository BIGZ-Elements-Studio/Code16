using oct.cameraControl;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatSystem.boss.stoneperson
{
    public class stoneHandController : MonoBehaviour
    {
       public bool followCharacter;
        float currentHeight;
        public Transform player { get {return combatController.Player.transform; } }
        public float skill1BHeight;
        public Vector3 currentPosion;
       public Transform Self;
        public float normalHeight;
       public BoxBullet BoxBullet;
        public GameObject damagecircle;
        public MeshRenderer rd;
       public BoxCollider BoxCollider;
        public Transform skill1APosition;
        public Transform spawnStonePosition;
        public GameObject stonePrefab;
        public GameObject shockWave;
        public SkeletonAnimation skeletonAnimation;
        Spine.AnimationState spineAnimationState { get { return skeletonAnimation.AnimationState; } }
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

        public IEnumerator skill1A()
        {
            followCharacter = false;

            yield return new WaitForFixedUpdate();
            currentPosion = skill1APosition.position;
            Self.transform.position= skill1APosition.position;
            rd.enabled = true;
            BoxCollider.enabled = true;
            //setAnimation
        }
       public Vector3 Skill2offsetPosition;
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
            currentPosion= Self.transform.position;
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
                currentPosion = new Vector3(currentPosion.x, currentPosion.y - movePosition, currentPosion.z);
                time-=Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
           var g= Instantiate(shockWave);
            g.transform.position = new Vector3(Self.position.x, 0, Self.position.z);
            g.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            BoxBullet.diable();
            yield return new WaitForSeconds(1.2f);
            rd.enabled = false;
            BoxCollider.enabled = false;
           
            damagecircle.SetActive(false);
        }
        public IEnumerator skill2()
        {
            spineAnimationState.TimeScale = 1;
            followCharacter = false;
            rd.enabled = true;
            BoxCollider.enabled = false;
            yield return new WaitForFixedUpdate();
            currentPosion = player.position+ Skill2offsetPosition;
            int index = (MainCameraController.Instance.Movement3d as CameraMovementThreeDDefault).AddTarget(this.transform);
            (MainCameraController.Instance.Movement3d as CameraMovementThreeDDefault).ChangeWeight(index, 60, 1f);
            yield return new WaitForSeconds(0.7f);
            spineAnimationState.SetAnimation(0, skill2Animation, false);
             GameObject g= Instantiate(stonePrefab);
            g.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            (MainCameraController.Instance.Movement3d as CameraMovementThreeDDefault).RemoveTarget(index, 1f);
        }
        #region skill3

        public Transform holdCharaPosition;
        public float Scale;
        public IEnumerator skill3()
        {
            spineAnimationState.TimeScale = Scale;
            followCharacter = false;
            rd.enabled = true;
            BoxCollider.enabled = false;
            yield return new WaitForFixedUpdate();
            currentPosion = player.position;
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

            yield return new WaitForFixedUpdate();
            var rb = Target.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            float holdTime = 0;
            spineAnimationState.AddAnimation(0, skill3Animation2, false,0);
            while (holdTime < totalHoldTime)
            {
                yield return new WaitForFixedUpdate();
                holdTime += Time.fixedDeltaTime;

                rb.MovePosition(holdCharaPosition.position+Vector3.forward*2f);
                Debug.Log(holdCharaPosition.gameObject.name+ " "+(holdCharaPosition.position-rb.transform.position));
            }
            rb.isKinematic = false;
            yield return new WaitForFixedUpdate();
            rb.velocity = new Vector3(0, velocity, 0);
            rb.useGravity = true;

        }
        public float velocity;
       public float totalHoldTime = 2;
        public GameObject currentChara;
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
        #endregion
        Coroutine currrentC;
        private void Start()
        {
            currrentC=StartCoroutine(trySkill2());
        }
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

                StartCoroutine(skill1B());
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
  

        private void FixedUpdate()
        {
            if (followCharacter)
            {
                Self.position =new Vector3(player.position.x, currentHeight, player.position.z);
            }
            else
            {
                Self.position = currentPosion;

            }
        }
    }
}