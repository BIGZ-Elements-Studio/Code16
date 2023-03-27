using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


/// <remarks>主角属性类</remarks>
public class LeadingRole
{
    public PlayerController playerController;
    public NaturalAttribute naturalAttribute;
    public FightingAttribute fightingAttribute;
    public List<CollectRole> collectRoles;

    public int Level { get; set; }
    public int Experience { get; set; }

    public class NaturalAttribute
    {
        //技巧
        public int Trick { get => _trick; }
        //体质
        public int Physique { get => _physique; }
        //精神
        public int Spirit { get => _spirit; }
        //魅力
        public int Charm { get => _charm; }
        //天赋点点
        public int Points { get => _points; }
        public NaturalAttribute(LeadingRole leadingRole) : this(leadingRole, 0, 0, 0, 0, 0)
        {
        }
        public NaturalAttribute(LeadingRole leadingRole, int skill, int physique, int spirit, int charm, int points)
        {
            _role = leadingRole;
            _trick = skill;
            _physique = physique;
            _spirit = spirit;
            _charm = charm;
            _points = points;
        }
        LeadingRole _role;
        int _trick;
        int _physique;
        int _spirit;
        int _charm;
        int _points;
        public void OnSkillChange(int num)
        {
            _trick += num;
            //TODO:数值改变时修改战斗属性
        }
        public void OnSkillPhysique(int num)
        {
            _physique += num;
            //TODO:数值改变时修改战斗属性
        }
        public void OnSkillSpirit(int num)
        {
            _spirit += num;
            //TODO:数值改变时修改战斗属性
        }
        public void OnSkillCharm(int num)
        {
            _charm += num;
            //TODO:数值改变时修改战斗属性
        }
        public void OnSkillPoints(int num)
        {
            _points += num;
            //TODO:数值改变时修改战斗属性
        }
    }
    public class FightingAttribute
    {
        //最大生命值
        public int HealthMax { get => _healthMax; }
        //当前生命值
        public int Health { get => _health; }
        //最大魔法值
        public int ManaMax { get => _manaMax; }
        //当前魔法值
        public int Mana { get => _mana; }
        //攻击力
        public int Attack { get => _attack; }
        //防御力
        public int Defense { get => _defense; }
        //暴击率
        public string CriticalChance { get => string.Format("{0:P2}", _criticalChance); }
        //暴击伤害
        public string CriticalDamage { get => string.Format("{0:P2}", _criticalDamage); }
        //暴击抵抗
        public string CriticalDefense { get => string.Format("{0:P2}", _criticalDefense); }
        //生命吸取
        public string HealthDrain { get => string.Format("{0:P2}", _healthDrain); }
        //最大闪避能量
        public string DodgeEnergyMax { get => string.Format("{0:0.#}", _dodgeEnergyMax); }
        //当前闪避能量
        public string DodgeEnergy { get => string.Format("{0:0.#}", _dodgeEnergy); }


        public int _healthMax;
        public int _health;
        public int _manaMax;
        public int _mana;
        public int _attack;
        public int _defense;
        public float _criticalChance;
        public float _criticalDamage;
        public float _criticalDefense;
        public float _healthDrain;
        public float _dodgeEnergyMax;
        public float _dodgeEnergy;


        public void DealDamage(int damage)
        {
        }

        public void TakeDamage(int damage)
        {
        }

        //TODO:other function
    }

}