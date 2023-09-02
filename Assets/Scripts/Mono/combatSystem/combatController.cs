using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem.team;
using Unity.VisualScripting;
namespace CombatSystem
{
    public class combatController : MonoBehaviour
    {
        [SerializeField]
        private GameObject player { get { return team.CurrentCharacter.gameObject; } }
        private Vector3 playerActualposition { get { return team.CurrentCharacterActualPosition.position; } }
        [SerializeField]
        private playerTeamController team;
        public static List<Transform> allEnemyTargets { get { List<Transform> a = new List<Transform>(); a.Add(Instance.player.transform); return a; } }
        private List<characterState> characterStates { get { return team.characterStates; } }
        public static List<characterState> CharacterStates
        {
            get { return Instance.characterStates; }
        }
        public static GameObject defaultEffect { get { return instance.Effect; } }
        [SerializeField]
        GameObject Effect;
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
        private void Awake()
        {
            team.OnCharacterStateChange.AddListener(invokeVisualEvent);
        }

        private void invokeVisualEvent(List<characterState> states)
        {
            EventBus.Trigger("OnCharacterStatesChanged", states);
        }
    }

    
}[UnitTitle("On Character States Changed")]//The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Events\\MyEvents\\CombatSystem")]//Set the path to find the node in the fuzzy finder as Events > My Events.
    public class ScriptablEventCharacterStateChange : EventUnit<List<characterState>>
    {
        [DoNotSerialize]// No need to serialize ports.
        public ValueOutput result { get; private set; }// The Event output data to return when the Event is triggered.
        protected override bool register => true;

        // Add an EventHook with the name of the Event to the list of Visual Scripting Events.
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook("OnCharacterStatesChanged");
        }
        protected override void Definition()
        {
            base.Definition();
            // Setting the value on our port.
            result = ValueOutput<List<characterState>>(nameof(result));
        }
        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, List<characterState> data)
        {
            flow.SetValue(result, data);
        }
    }