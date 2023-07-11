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
        public SkeletonAnimation skeletonAnimation;
        Spine.AnimationState spineAnimationState { get { return skeletonAnimation.AnimationState; } }
        [SpineAnimation]
       public string skill1;
        [SpineAnimation]
        public string idleAnimation;
        [SpineAnimation]
        public string skill2Animation;
        public GameObject stonePrefab;
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
       public Vector3 offsetPosition;
        public IEnumerator skill2()
        {
            followCharacter = false;
            rd.enabled = true;
            BoxCollider.enabled = false;
            yield return new WaitForFixedUpdate();
            spineAnimationState.SetAnimation(0, skill2Animation, false);
            currentPosion = player.position+ offsetPosition;
            GameObject g= Instantiate(stonePrefab);
            g.SetActive(true);
            yield return new WaitForSeconds(0.56f);

            //make stone, 

            
            //throw

            //setAnimation
        }
        public IEnumerator skill1B()
        {
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
            yield return new WaitForSeconds(0.2f);

            spineAnimationState.SetAnimation(0, skill1, false);
            float time = 0.25f;
            float movePosition = (skill1BHeight- normalHeight) / (time / Time.fixedDeltaTime);
            BoxBullet.active();
            BoxCollider.enabled = true;
            while (time > 0)
            {
                currentPosion = new Vector3(currentPosion.x, currentPosion.y - movePosition, currentPosion.z);
                time-=Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(0.3f);
            BoxBullet.diable();
            yield return new WaitForSeconds(1.2f);
            rd.enabled = false;
            BoxCollider.enabled = false;
           
            damagecircle.SetActive(false);
        }
        private void Start()
        {
            StartCoroutine(trything());
        }
        public IEnumerator trything()
        {
            while (true)
            {
                yield return new WaitForSeconds(5f);
                StartCoroutine(skill2());
            }
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