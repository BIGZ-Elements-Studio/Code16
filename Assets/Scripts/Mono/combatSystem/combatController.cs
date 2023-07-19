using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatSystem
{
    public class combatController : MonoBehaviour
    {
        [SerializeField]
        private GameObject player { get { return team.CurrentCharacter.gameObject; } }
        private Vector3 playerActualposition { get { return team.CurrentCharacter.ActualTransform.position; } }
        [SerializeField]
        private playerTeamController team;
        public static List<Transform> allEnemyTargets { get { List<Transform> a = new List<Transform>(); a.Add(Instance.player.transform);return a; } }
        private List<characterState> characterStates { get { return team.characterStates; } }
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

        public static Vector3 PlayerActualPosition
        {
            get
            {
                return Instance.playerActualposition;
            }
        }
    }
}