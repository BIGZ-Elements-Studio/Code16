using System;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle, Patrol, Chase, React, Attack, Hit, Death, Walking, Jump
}

[Serializable]
public class LeadingRole
{
    public Animator animator;

    public NaturalAttribute naturalAttribute;
    public FightingAttribute fightingAttribute;
    public List<CollectRole> collectRoles;


    public int Level { get; set; }
    public int Experience { get; set; }

    [Serializable]
    public class NaturalAttribute
    {
        //技巧
        public int Trick { get => _skill; }
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
            _skill = skill;
            _physique = physique;
            _spirit = spirit;
            _charm = charm;
            _points = points;
        }
        LeadingRole _role;
        public int _skill;
        public int _physique;
        public int _spirit;
        public int _charm;
        public int _points;
        public void OnSkillChange(int num)
        {
            _skill += num;
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
    /// <summary></summary>
    /// <remarks></remarks>
    [Serializable]
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
public class FSM : MonoBehaviour
{
    public string markState;
    private IState currentState;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    public LeadingRole leadingRole;
    void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Walking, new WalkingState(this));
        states.Add(StateType.Jump, new JumpState(this));
        states.Add(StateType.Attack, new AttackState(this));


        TransitionState(StateType.Idle);
        leadingRole.animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
        currentState.OnUpdate();

    }

    public  void TransitionState(StateType type)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = states[type];
        markState = type.ToString();
        currentState.OnEnter();
    }

    public void FlipTo(Transform target)
    {
        if (target != null)
        {
            if (transform.position.x > target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x < target.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void OnTriggerExit2D(Collider2D other)
    {

    }

}