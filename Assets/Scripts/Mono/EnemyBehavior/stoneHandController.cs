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
        public IEnumerator skill1B()
        {
            //此时石巨人右手会随着玩家角色的移动而晃动，1.5秒后完成索敌，砸向目前锁定的位置，
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
           // StartCoroutine(trything());
        }
        public IEnumerator trything()
        {
            while (true)
            {
                yield return new WaitForSeconds(5f);
                StartCoroutine(skill1B());
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