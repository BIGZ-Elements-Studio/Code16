using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CombatSystem
{
    //help to acess different part of the character
    //also control extra script such as buff
    public class BuffContainer : MonoBehaviour
    {
        public TargetType type;
        List<Buff> buffs = new List<Buff>();

        List<Sprite> BuffIcons = new List<Sprite>();

        public UnityEvent<List<Sprite>> BuffIconChange;
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
        public int AddBuffIcon(Sprite Icon)
        {
            BuffIcons.Add(Icon);
            BuffIconChange?.Invoke(BuffIcons);
            return buffs.Count-1;
           
        }

        // remove buff icon Ui
        public void RemoveBuffIcon(int ID)
        {
            BuffIcons.RemoveAt(ID);
            BuffIconChange?.Invoke(BuffIcons);
        }

        public void removeBuff(Buff buff)
        {
            buffs.Remove(buff);
        }
    }
}