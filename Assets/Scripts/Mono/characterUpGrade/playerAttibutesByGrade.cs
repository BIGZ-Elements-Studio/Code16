using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "角色面板信息", menuName = "角色面板")]
public class playerAttibutesByGrade : ScriptableObject
{
   public int multipler=1;
    public int level;
    public int baseHP {
        get
        {
            return multipler*level* _baseHp;
        }
    }
    [SerializeField]
    private int _baseHp;
    public int baseDef
    {
        get
        {
            return multipler * level * _baseDef;
        }
    }

    [SerializeField]
    private int _baseDef;

    public int basePoise
    {
        get
        {
            return multipler * level * _basePoise;
        }
    }

    [SerializeField]
    private int _basePoise;

    public int baseAtk
    {
        get
        {
            return multipler * level * _baseAtk;
        }
    }

    [SerializeField]
    private int _baseAtk;

    public int baseCritcAtkRate
    {
        get
        {
            return multipler * level * _baseCritcAtkRate;
        }
    }

    [SerializeField]
    private int _baseCritcAtkRate;

    public int baseCritcAtkDamage
    {
        get
        {
            return multipler * level * _baseCritcAtkDamage;
        }
    }

    [SerializeField]
    private int _baseCritcAtkDamage;

    public int MaxSp
    {
        get
        {
            return multipler * level * _maxSp;
        }
    }

    [SerializeField]
    private int _maxSp;

}
