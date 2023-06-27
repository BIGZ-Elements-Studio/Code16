using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace oct.EnemyMovement
{
    public class EnemyAI : MonoBehaviour
    {

        [SerializeField]
        private List<SteeringBehaviour> steeringBehaviours;

        [SerializeField]
        private AIData aiData;

        [SerializeField]
        private float aiUpdateDelay = 0.06f;

        [SerializeField]
        BlockInfo blockInfo;
        public UnityEvent<Vector3> OnMovementInput;

        [SerializeField]
        private Vector3 movementInput;

        bool following = false;

        private void Start()
        {
            RefreshList();
            InvokeRepeating("setDirection", 0, aiUpdateDelay);

        }
        private void RefreshList()
        {
            aiData.obstacles = blockInfo.allColliders.ToArray();
            aiData.currentTarget = combatController.Player.transform;

            aiData.targets =new List<Transform>();
            aiData.targets.Add(combatController.Player.transform);
        }
        private void setDirection()
        {
            movementInput = GetDirectionToMove(steeringBehaviours, aiData);
            OnMovementInput?.Invoke(movementInput);
        }
        Vector3 resultDirection = Vector3.zero;
        public Vector3 GetDirectionToMove(List<SteeringBehaviour> behaviours, AIData aiData)
        {
            float[] danger = new float[8];
            float[] interest = new float[8];
            //Loop through each behaviour
            foreach (SteeringBehaviour behaviour in behaviours)
            {
                (danger, interest) = behaviour.GetSteering(danger, interest, aiData);
            }

            //subtract danger values from interest array
            for (int i = 0; i < 8; i++)
            {
                interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
            }


            //get the average direction
            Vector3 outputDirection = Vector3.zero;
            for (int i = 0; i < 8; i++)
            {
                outputDirection += Directions.eightDirections[i] * interest[i];
            }

            outputDirection.Normalize();

            resultDirection = outputDirection;

            //return the selected movement direction
            return resultDirection;
        }
    }
}