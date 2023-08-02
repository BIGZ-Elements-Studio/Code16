using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace CombatSystem
{
    public class AttackAttributeController : MonoBehaviour
    {
        [SerializeField]
        FieldForTeamBuff fieldForTeamBuff;
        public int AtkPercent{ get { return 100+fieldForTeamBuff.AtkPercentage; }}
        public int Atk { get { return baseAtk + fieldForTeamBuff.Atk; } }
        public int baseAtk;

        public float AtkSpeed { get { return 1 + fieldForTeamBuff.ATkSpeedFactor; } }
        public float moveSpeedFactor { get { return 1 + fieldForTeamBuff.moveSpeedFactor; } }
        public int CritcAtkRate { get { return baseCritcAtkRate + fieldForTeamBuff.criticAttackRate; } }
        public int baseCritcAtkRate;

        public int CritcAtkDamage { get { return baseCritcAtkDamage + fieldForTeamBuff.criticAttackDamage; } }
        public int baseCritcAtkDamage;

        public int Def { get { return baseDef + fieldForTeamBuff.Def; } }
        public int baseDef;
        public int DefPercent { get { return 100 + fieldForTeamBuff.DefPercentage; } }

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

    }
}