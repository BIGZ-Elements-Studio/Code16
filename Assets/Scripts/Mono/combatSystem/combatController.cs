using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatSystem
{
    public class combatController : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private List<characterState> characterStates=new List<characterState>();
        public static List<characterState> CharacterStates
        {
            get { return Instance.characterStates; }
        }
        

        private static combatController instance;

        public static combatController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<combatController>();
                }
                return instance;
            }
        }
        public static GameObject Player
        {
            get
            {
               return Instance.player;
            }
        }

        public static void setState(List<characterState> states)
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<combatController>();
            }
            instance.characterStates = states;
        }
    }
}