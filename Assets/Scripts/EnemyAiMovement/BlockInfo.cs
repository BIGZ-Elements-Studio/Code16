using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatSystem.map
{
    public class BlockInfo : MonoBehaviour
    {
        public List<Collider> Obs;
        public List<Collider> Enemy;
        public Transform zHight;
        //public List<player> player;
        public Vector3 yAtSceneY(Vector3 vector)
        {
            return new Vector3(vector.x, zHight.position.y, vector.z);
        }
        public List<Collider> allColliders
        {
            get { Obs.AddRange(Enemy); return Obs; }
        }
    }
}