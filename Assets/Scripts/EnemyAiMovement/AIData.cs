using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace oct.EnemyMovement
{
    public class AIData : MonoBehaviour
    {
        public BlockInfo sceneInfo;
        public List<Transform> targets = null;
        public Collider[] obstacles = null;
        public Collider[] Enemys = null;

        public Transform currentTarget;

        public int GetTargetsCount() => targets == null ? 0 : targets.Count;
    }
}