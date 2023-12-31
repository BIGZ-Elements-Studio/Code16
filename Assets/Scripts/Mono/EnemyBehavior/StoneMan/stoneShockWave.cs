using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
namespace CombatSystem.boss.stoneperson
{
    public class stoneShockWave : MonoBehaviour
    {
        [SerializeField]
        float targetRadius;
        [SerializeField]
        float totalTime;
        [SerializeField]
        SphereCollider collider;
        [SerializeField]
        float height;
        [SerializeField]
        float ForceMagnitude;
        [SerializeField]
        LayerMask obstrctionlayerMask;
        [SerializeField]
        ParticleSystem particleSystem;

        public GameObject self;
        private void OnEnable()
        {
            particleSystem.Play();
            StartCoroutine(process());
        }
        public IEnumerator process()
        {

            float currentTime = 0;
            var Increment = targetRadius / (totalTime / Time.fixedDeltaTime);
            collider.radius = 0;
            yield return new WaitForSeconds(totalTime);
            Destroy(self);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Mathf.Abs(other.gameObject.transform.position.y - gameObject.transform.position.y) > height)
            {
                return;
            }
            var damagePoint = other.GetComponent(typeof(DamageTarget)) as DamageTarget;
            if (damagePoint == null || damagePoint.getType() != TargetType.player)
            {
                return;
            }

            bool Obstructed = Physics.Linecast(transform.position, other.ClosestPoint(transform.position), out RaycastHit hitInfo, obstrctionlayerMask);

            if (Obstructed)
            {
                // Debug.Log(" µ²×¡" + hitInfo.collider.gameObject.name); ;
                return;
            }
            damage(damagePoint);
        }
        void damage(DamageTarget damagePoint)
        {
            //  Debug.Log(damagePoint as MonoBehaviour);
            DamageObject a = DamageObject.GetdamageObject();
            a.hardness = 10;
            a.damage = 100;
            damagePoint.Damage(a, CombatColor.empty);
            //   Debug.Log((damagePoint.getCenterPosition() - transform.position).normalized);
            damagePoint.addforceOfDirection(((damagePoint.getCenterPosition() - transform.position).normalized + Vector3.up).normalized * ForceMagnitude);
        }
        public static class Directions
        {
            public static List<Vector3> eightDirections = new List<Vector3>{
            new Vector3(0,0,1).normalized,
            new Vector3(1,0,1).normalized,
            new Vector3(1,0,0).normalized,
            new Vector3(1,0,-1).normalized,
            new Vector3(0,0,-1).normalized,
            new Vector3(-1,0,-1).normalized,
            new Vector3(-1,0,0).normalized,
            new Vector3(-1,0,1).normalized
        };
        }
    }
}