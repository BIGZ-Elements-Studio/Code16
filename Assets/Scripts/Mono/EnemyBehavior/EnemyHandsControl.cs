using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatSystem.boss.stoneperson
{
    public class EnemyHandsControl : MonoBehaviour
    {
       public stoneHandController controller1;
        public stoneHandController controller2;
        private void Start()
        {
            InvokeRepeating("skill1", 0, 10);
            InvokeRepeating("skill1B", 5, 10);

        }
        void skill1()
        {
            controller1.StartCoroutine(controller1.skill1A());
            controller2.StartCoroutine(controller2.skill1B());
        }
        void skill1B()
        {
            controller2.StartCoroutine(controller2.skill1A());
            controller1.StartCoroutine(controller1.skill1B());
        }
    }
}