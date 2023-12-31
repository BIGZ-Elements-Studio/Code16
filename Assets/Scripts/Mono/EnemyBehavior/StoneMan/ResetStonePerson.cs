using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatSystem.boss.stoneperson
{
    public class ResetStonePerson : MonoBehaviour
    {
        public EnemyHPContainner hpContainner;
        public EnemyHandsControl control;
       public void ResetBoss()
        {
            
            hpContainner.Reset();
            control.Reset();
        }
    }
}