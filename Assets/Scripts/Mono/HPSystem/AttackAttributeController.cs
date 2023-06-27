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

        public int sp;
        public int MaxSp;

        public string spName;

        [SerializeField]
        playerAttibutesByGrade controller;
        public UnityEvent<string, float> onSpChange;
        private void Awake()
        {
            Atk=baseAtk;
            CritcAtkRate=baseCritcAtkRate;
            CritcAtkDamage=baseCritcAtkDamage;
            onSpChange?.Invoke(spName, sp);
        }
        private void setAmount()
        {
            baseAtk=controller.baseAtk;
            baseCritcAtkDamage = controller.baseCritcAtkDamage;
            baseCritcAtkRate = controller.baseCritcAtkRate;
            MaxSp = controller.MaxSp;
        }
        public void GainSp(int i)
        {
            sp += i;
            sp = Mathf.Clamp(sp,0,MaxSp);
            onSpChange?.Invoke(spName,sp);
        }
        //  public int baseCritcAtkDamage;
        //   public UnityEvent<string, float> onPoiseChange;
        //   public UnityEvent<string, float> onHPChange;
        //  public UnityEvent<string, bool> onHited;
        //  public UnityEvent<Vector3> AddedForce;
    }
}