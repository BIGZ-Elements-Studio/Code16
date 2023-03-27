using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


/// <remarks>����������</remarks>
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
        //����
        public int Trick { get => _trick; }
        //����
        public int Physique { get => _physique; }
        //����
        public int Spirit { get => _spirit; }
        //����
        public int Charm { get => _charm; }
        //�츳���
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
            //TODO:��ֵ�ı�ʱ�޸�ս������
        }
        public void OnSkillPhysique(int num)
        {
            _physique += num;
            //TODO:��ֵ�ı�ʱ�޸�ս������
        }
        public void OnSkillSpirit(int num)
        {
            _spirit += num;
            //TODO:��ֵ�ı�ʱ�޸�ս������
        }
        public void OnSkillCharm(int num)
        {
            _charm += num;
            //TODO:��ֵ�ı�ʱ�޸�ս������
        }
        public void OnSkillPoints(int num)
        {
            _points += num;
            //TODO:��ֵ�ı�ʱ�޸�ս������
        }
    }
    public class FightingAttribute
    {
        //�������ֵ
        public int HealthMax { get => _healthMax; }
        //��ǰ����ֵ
        public int Health { get => _health; }
        //���ħ��ֵ
        public int ManaMax { get => _manaMax; }
        //��ǰħ��ֵ
        public int Mana { get => _mana; }
        //������
        public int Attack { get => _attack; }
        //������
        public int Defense { get => _defense; }
        //������
        public string CriticalChance { get => string.Format("{0:P2}", _criticalChance); }
        //�����˺�
        public string CriticalDamage { get => string.Format("{0:P2}", _criticalDamage); }
        //�����ֿ�
        public string CriticalDefense { get => string.Format("{0:P2}", _criticalDefense); }
        //������ȡ
        public string HealthDrain { get => string.Format("{0:P2}", _healthDrain); }
        //�����������
        public string DodgeEnergyMax { get => string.Format("{0:0.#}", _dodgeEnergyMax); }
        //��ǰ��������
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