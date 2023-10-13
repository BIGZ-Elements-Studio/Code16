using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CombatSystem
{
    public class CombineBuff : MonoBehaviour
    {
        public TeamBuffContainer Team;
        public CharaBuffContainer Character;

        public UnityEvent<List<BuffIconDisplay.DisplayInfo>> buffDisplayChange;

        List<BuffIconDisplay.DisplayInfo> CurrentTeamDisplay;
        List<BuffIconDisplay.DisplayInfo> CurrentCharaDisplay;
        void setTeam(TeamBuffContainer newContainer)
        {
            if (Team!=null)
            {
                Team.BuffDisplayChange .RemoveListener( TeamIconChanged);
            }
            newContainer.BuffDisplayChange.AddListener(TeamIconChanged);
            TeamIconChanged(newContainer.BuffIcons);
            Team = newContainer;
        }

        void setcharacterTeam(CharaBuffContainer newContainer)
        {
            if (Character != null)
            {
                Character.BuffDisplayChange.RemoveListener(CharaIconChanged);
            }
            newContainer.BuffDisplayChange.AddListener(CharaIconChanged);
            CharaIconChanged(newContainer.BuffIcons);
            Character = newContainer;
        }
        void TeamIconChanged(List<BuffIconDisplay.DisplayInfo> team)
        {
            combine(team, CurrentCharaDisplay);
        }
        void CharaIconChanged(List<BuffIconDisplay.DisplayInfo> chara)
        {
            combine(CurrentTeamDisplay, chara);
        }

        void combine(List<BuffIconDisplay.DisplayInfo> team, List<BuffIconDisplay.DisplayInfo> chara)
        {
            List<BuffIconDisplay.DisplayInfo> combinedList = team.Concat(chara).ToList();
            buffDisplayChange?.Invoke(combinedList);
        }
    }
}