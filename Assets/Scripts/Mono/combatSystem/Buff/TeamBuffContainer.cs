using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace CombatSystem
{
    public class TeamBuffContainer : MonoBehaviour
    {
        public TargetType type;
        public HPContainer HpInfo;
        public FieldForTeamBuff AttackAttributeHp;
        List<TeamBuff> buffs = new List<TeamBuff>();

      public  List<BuffIconDisplay.DisplayInfo> BuffIcons = new List<BuffIconDisplay.DisplayInfo>();

        public UnityEvent<List<BuffIconDisplay.DisplayInfo>> BuffDisplayChange;
        public void addBuff(TeamBuff buff)
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                TeamBuff b = buffs[i];
                if (b.GetType() == buff.GetType())
                {
                    TeamBuff overlayedbuff = buffs[i];
                    buffs[i] = buff;
                    buff.overlying(b, AttackAttributeHp);
                    overlayedbuff.overlayed();
                    return;
                }
            }
            buffs.Add(buff);
            buff.initiate(AttackAttributeHp);
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
            BuffIcons[id]=Icon;
            BuffDisplayChange?.Invoke(BuffIcons);
        }

        // remove buff icon Ui
        public void RemoveBuffIcon(int ID)
        {
            BuffIcons.RemoveAt(ID);
            BuffDisplayChange?.Invoke(BuffIcons);
        }

        public void removeBuff(TeamBuff buff)
        {
            buffs.Remove(buff);
        }
    }
}