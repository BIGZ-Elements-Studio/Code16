using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace oct.EnemyMovement
{
    public  class Detector : MonoBehaviour
    {
        [SerializeField]
        private LayerMask ObsticallayerMask;
        [SerializeField]
        private float DetectionRadius;
        public void Detect(AIData aiData)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, DetectionRadius, ObsticallayerMask);
            aiData.obstacles = colliders;
        }
    }
}