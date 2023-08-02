
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatSystem
{
    public class combatColorController : MonoBehaviour
    {
        [SerializeField]
        Color redShieldColor;
        [SerializeField]
        Color BlueShieldColor;
        [SerializeField]
        Color YellowShieldColor;
        [SerializeField]
        Color EmptyColor;
       public GameObject ColorBall;
        private static combatColorController instance;

        public static combatColorController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<combatColorController>();
                }
                return instance;
            }
        }

      public static  Color getShieldColor(CombatColor color)
        {
            if (color==CombatColor.red)
            {
                return Instance.redShieldColor;
            }else if (color == CombatColor.blue)
            {
                return Instance.BlueShieldColor;
            }
            else if (color == CombatColor.yellow)
            {
                return Instance.YellowShieldColor;
            }
            else
            {
                return Instance.EmptyColor;
            }
        }
    }
}