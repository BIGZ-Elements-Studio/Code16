
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
        [SerializeField]
        GameObject redColorball;
        [SerializeField]
        GameObject BlueColorball;
        [SerializeField]
        GameObject YellowColorball;
        [SerializeField]
        GameObject emptyColorball;
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
        public static GameObject GetColorBall(CombatColor color)
        {
            if (color == CombatColor.red)
            {
                return Instance.redColorball;
            }
            else if (color == CombatColor.blue)
            {
                return Instance.BlueColorball;
            }
            else if (color == CombatColor.yellow)
            {
                return Instance.YellowColorball;
            }
            else
            {
                return Instance.YellowColorball;
            }
        }







    }
}