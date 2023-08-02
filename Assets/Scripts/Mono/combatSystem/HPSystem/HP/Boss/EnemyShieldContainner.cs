using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyShieldContainner : MonoBehaviour
{
    
    public int maxShield;
    public int CurrentShield;
    public UnityEvent<CombatColor> ShieldBreak;
    public CombatColor color;
    public int Def;
    [SerializeField]
    CharaBuffContainer charaBuff;
    public UnityEvent<float> ShieldChanged;
    public UnityEvent<CombatColor> CombatColorChanged;
    public bool reduceShield(DamageObject amount)
    {
        CurrentShield-=amount.damage;
        if(CurrentShield < 0&& maxShield>0)
        {
            maxShield = 0;
            CurrentShield = 0;
            charaBuff.removeBuff(Buff);
            ShieldBreak?.Invoke(color);
            ShieldChanged.Invoke(0);
            return false;
        }
        if (maxShield>0) {
            ShieldChanged.Invoke((float)CurrentShield / (float)maxShield);
        }
        else
        {
            ShieldChanged.Invoke(0);
        }
        return true;
    }

    public void setShield(CombatColor Shieldcolor, int amount)
    {
        color= Shieldcolor;
        maxShield= amount;
        CurrentShield= amount;
        Buff = new shieldBuff();
        Buff.extraDef = Def;
        charaBuff.addBuff(Buff);
        ShieldChanged.Invoke(1);
        CombatColorChanged.Invoke(Shieldcolor);
    }
    shieldBuff Buff;
    public class shieldBuff : CharacterBuff
    {

        CharaBuffContainer Controller;
        FieldForCharacterBuff Property;
        public int extraDef;
        public void initiate(CharaBuffContainer target)
        {
            Controller = target;
            Property = target.FieldForBuff;
            if (Property != null)
            {
               Property.Def+= extraDef;
            }
        }
        public void overlayed()
        {

            Property.Def -= extraDef;
        }

        public void overlying(CharacterBuff overlayedBuff, CharaBuffContainer target)
        {
            Controller = target;
            Property = target.FieldForBuff;
            if (Property != null)
            {
                Property.Def += extraDef;
            }

        }

        public void OnRemoved()
        {
            Property.Def -= extraDef;
        }
    }
    }
