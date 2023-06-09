using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    //help to acess different part of the character
    //also control extra script such as buff
    public class BuffContainer : MonoBehaviour
    {
        public TargetType type;
        List<Buff> buffs = new List<Buff>();

        List<Sprite> BuffIcons = new List<Sprite>();

      
        public void addBuff(Buff buff)
        {
            for(int i =0; i< buffs.Count; i++)
            {
                Buff b = buffs[i];
                if (b.GetType() == buff.GetType())
                {
                    Buff overlayedbuff=  buffs[i];
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
        public void AddBuffIcon(Sprite Icon)
        {
            BuffIcons.Add(Icon);
        }

        // remove buff icon Ui
        public void RemoveBuffIcon(Sprite Icon)
        {
            BuffIcons.Remove(Icon);
        }

        public void removeBuff(Buff buff)
        {
            buffs.Remove(buff);
        }
    }
}