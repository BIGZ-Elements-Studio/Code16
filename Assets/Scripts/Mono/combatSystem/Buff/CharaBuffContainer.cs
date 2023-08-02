using CombatSystem.shieldSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CombatSystem
{
    //help to acess different part of the character
    //also control extra script such as buff
    public class CharaBuffContainer : MonoBehaviour
    {
        public FieldForCharacterBuff FieldForBuff;
        List<CharacterBuff> buffs = new List<CharacterBuff>();

       public List<BuffIconDisplay.DisplayInfo> BuffIcons = new List<BuffIconDisplay.DisplayInfo>();

        public UnityEvent<List<BuffIconDisplay.DisplayInfo>> BuffDisplayChange;
        public void addBuff(CharacterBuff buff)
        {
            for(int i =0; i< buffs.Count; i++)
            {
                CharacterBuff b = buffs[i];
                if (b.GetType() == buff.GetType())
                {
                    CharacterBuff overlayedbuff=  buffs[i];
                    buffs[i] = buff;
                    buff.overlying(b,this);
                    overlayedbuff.overlayed();
                    return;
                }
            }
            buffs.Add(buff);
            buff.initiate(this);
        }

        // add buff icon Ui
        public int AddBuffIcon(BuffIconDisplay.DisplayInfo Icon)
        {
            BuffIcons.Add(Icon);
            BuffDisplayChange?.Invoke(BuffIcons);
            return buffs.Count - 1;

        }
        public void ModifyBuffIcon(int id, BuffIconDisplay.DisplayInfo Icon)
        {
            BuffIcons[id] = Icon;
            BuffDisplayChange?.Invoke(BuffIcons);
        }

        // remove buff icon Ui
        public void RemoveBuffIcon(int ID)
        {
            BuffIcons.RemoveAt(ID);
            BuffDisplayChange?.Invoke(BuffIcons);
        }

        public void removeBuff(CharacterBuff buff)
        {
            buff.OnRemoved();
            buffs.Remove(buff);

        }

    }
}