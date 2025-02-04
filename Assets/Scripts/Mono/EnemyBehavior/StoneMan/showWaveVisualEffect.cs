using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatSystem.boss.stoneperson
{
    public class showWaveVisualEffect : MonoBehaviour
    {
        [SerializeField]
        LayerMask obsticle;
        public Renderer r;
        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other)
        {

            if ((obsticle.value & (1 << other.gameObject.layer)) != 0)
            {
                disappear(true);
            }
        }
        private void Awake()
        {
            r = GetComponent<Renderer>();
        }
        public void disappear(bool disappear)
        {
            r.enabled = !disappear;
        }
    }
}