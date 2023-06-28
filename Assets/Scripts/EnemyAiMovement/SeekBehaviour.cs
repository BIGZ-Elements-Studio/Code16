using UnityEngine;
namespace oct.EnemyMovement
{
    public class SeekBehaviour : SteeringBehaviour
    {
        [SerializeField]
        private bool showGizmo = true;


        public float deflectAngle;
        public float maxangle;
        //Ò»°ã1-10
        public int roughness = 10;
        public float seed;
        public float distance;

        [SerializeField]
        private int current;
        public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
        {

            current++;
            targetPositionCached = aiData.currentTarget.transform.position;
            distance = Vector3.Distance(aiData.sceneInfo.yAtSceneY(transform.position), targetPositionCached);
            //If we havent yet reached the target do the main logic of finding the interest directions
            Vector3 directionToTarget = (targetPositionCached - (aiData.sceneInfo.yAtSceneY(transform.position))).normalized;
            Vector3 inputDirection = AngleUtility.AddAngle(AngleUtility.VtoA(directionToTarget), deflectAngle);

            inputDirection = AngleUtility.AddNoise(inputDirection, maxangle, (float)roughness / 100, current, seed);

            for (int i = 0; i < interest.Length; i++)
            {
                float result = Vector3.Dot(inputDirection.normalized, Directions.eightDirections[i]);

                //accept only directions at the less than 90 degrees to the target direction
                if (result > 0)
                {
                    float valueToPutIn = result;
                    if (valueToPutIn > interest[i])
                    {
                        interest[i] = valueToPutIn;
                    }

                }
            }
            interestsTemp = interest;
            return (danger, interest);
        }
#if UNITY_EDITOR
        //gizmo parameters
        private Vector3 targetPositionCached;
        private float[] interestsTemp;

#endif
    }
}