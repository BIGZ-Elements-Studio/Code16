using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombatSystem.team.UI
{
    public class changecharacterButtom : MonoBehaviour
    {
        public int num;
        public playerTeamController c;
        public Color DefalutColor;
        public Image self;
        private void Awake()
        {
            c.CharacterChanged.AddListener(checkChange);
        }
        public void checkChange(int index)
        {
            if (index== num)
            {
                self.color = Color.white;
            }
            else
            {
                self.color = DefalutColor;
            }
        }
        public void change()
        {
            c.swtichCharacter(num);

        }
    }
}