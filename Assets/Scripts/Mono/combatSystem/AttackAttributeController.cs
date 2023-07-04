using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace CombatSystem
{
    public class AttackAttributeController : MonoBehaviour
    {
        public TargetType type;
        public int Atk { get { return baseAtk; } }
        public int baseAtk;

        public int AtkSpeed;

        public int CritcAtkRate { get { return baseCritcAtkRate; } }
        public int baseCritcAtkRate;

        public int CritcAtkDamage { get { return baseCritcAtkDamage; } }
        public int baseCritcAtkDamage;

        public float moveSpeedFactor;

        public int MaxSp;

        public string spName;

        [SerializeField]
        playerAttibutesByGrade controller;

        private void Start()
        {
            if (controller!=null) {
                setAmount();
            } }

        private void setAmount()
        {
            baseAtk=controller.baseAtk;
            baseCritcAtkDamage = controller.baseCritcAtkDamage;
            baseCritcAtkRate = controller.baseCritcAtkRate;
            MaxSp = controller.MaxSp;
        }
        //  public int baseCritcAtkDamage;
        //   public UnityEvent<string, float> onPoiseChange;
        //   public UnityEvent<string, float> onHPChange;
        //  public UnityEvent<string, bool> onHited;
        //  public UnityEvent<Vector3> AddedForce;
    }
}