using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace CombatSystem
{
    public class AttackAttributeController : MonoBehaviour
    {
        public TargetType type;
        public int Atk;
        public int baseAtk;

        public int AtkSpeed;

        public int CritcAtkRate;
        public int baseCritcAtkRate;

        public int CritcAtkDamage;
        public int baseCritcAtkDamage;

        public float moveSpeedFactor;
        private void Awake()
        {
            Atk=baseAtk;
            CritcAtkRate=baseCritcAtkRate;
            CritcAtkDamage=baseCritcAtkDamage;
        }
        //  public int baseCritcAtkDamage;
        //   public UnityEvent<string, float> onPoiseChange;
        //   public UnityEvent<string, float> onHPChange;
        //  public UnityEvent<string, bool> onHited;
        //  public UnityEvent<Vector3> AddedForce;
    }
}