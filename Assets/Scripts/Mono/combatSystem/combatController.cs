using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem.team;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Events;

namespace CombatSystem
{
    public class combatController : MonoBehaviour
    {
        [SerializeField]
        private GameObject player { get { return team.CurrentCharacter.gameObject; } }
        private Vector3 playerActualposition { get { return team.CurrentCharacterActualPosition.position; } }
        private Vector3 playerActualposition2D { get { return team.CharacterActualPosition2D.position; } }
        [SerializeField]
        private playerTeamController team;
        public static List<Transform> allEnemyTargets { get { List<Transform> a = new List<Transform>(); a.Add(Instance.player.transform); return a; } }
        private List<characterState> characterStates { get { return team.characterStates; } }
        public static List<characterState> CharacterStates
        {
            get { return Instance.characterStates; }
        }
        public static playerTeamController Team
        {
            get { return Instance.team; }
        }
        public static GameObject defaultEffect { get { return instance.Effect; } }
        [SerializeField]
        GameObject Effect;
        private static combatController instance;
       public GameObject lockIcon;
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
        public static Vector3 PlayerActualposition2D
        {
            get
            {
                return Instance.playerActualposition2D;
            }
        }
        public static float DetectionRadius=10;

        public static void TryFindEnemy()
        {
            if (followingTargrtCashed == null)
            {
                FindLockEnemy(PlayerActualPosition, direction);
            }
        }
        public static DamageTarget FindLockEnemy(Vector3 playerPosition,Vector2 playerDirection)
        {
            Collider[] colliders = Physics.OverlapSphere(playerPosition, DetectionRadius);
            float maxWeight = -1;
            DamageTarget maxtarget = null;
            foreach (Collider collider in colliders)
            {
               // Debug.Log(collider.gameObject.name);
                DamageTarget target=(DamageTarget)collider.GetComponent(typeof( DamageTarget));
                if (target != null&&target.GetlockedEnemyTransform()!=null)
                {
                    Vector3 t = target.GetlockedEnemyTransform().position;
                    Vector2 distance = new Vector2((t - playerPosition).x, (t - playerPosition).z);
                    float weight = (Vector3.Dot(distance.normalized, playerDirection.normalized)-0.2f) + ((1-(distance.magnitude / DetectionRadius))*2);
                    if (weight > maxWeight&& (Vector3.Dot(distance.normalized, playerDirection.normalized) - 0.2f)>0)
                    {
                       
                        maxtarget = target;
                        maxWeight = weight;

                    }
                }
                
                

            }
            if (maxWeight >0)
            {
                rejesterLock(maxtarget);
                currentfollowingTarget = maxtarget;
                followingTargrtCashed = currentfollowingTarget;
                instance.lockIcon.GetComponent<FollowTransform>().target = maxtarget.GetlockedEnemyTransform();
                instance.StartCoroutine(calculateWeight());
                instance.lockIcon.SetActive(true);
                return maxtarget;
            }
            return null; 
        }
        private void Awake()
        {
            input = new PlayerInput();
            input.Enable();
            OnCharaDie = new UnityEvent();
            input.In3d.run.performed += ctx => { if (isActiveAndEnabled) { direction = (ctx.ReadValue<Vector2>()).normalized; } };
        }
       static Vector2 direction;
        static public PlayerInput input;

        public static  DamageTarget currentfollowingTarget;
         static DamageTarget followingTargrtCashed;
        static void rejesterLock(DamageTarget maxtarget)
        {
            maxtarget.OnLockDistory?.AddListener(DisjesterLock);
            maxtarget.OnLockAppear?.AddListener(hide);
        }
        static void hide(bool i)
        {
            if (i)
            {
                instance.lockIcon.SetActive(true);
                currentfollowingTarget = followingTargrtCashed;
            }
            else
            {
                instance.lockIcon.SetActive(false);
                currentfollowingTarget =null;
            }
        }
        static void DisjesterLock()
        {
            instance.lockIcon.SetActive(false);
            followingTargrtCashed.OnLockDistory?.RemoveListener(DisjesterLock);
            followingTargrtCashed.OnLockAppear?.RemoveListener(hide);
             currentfollowingTarget=null;
             followingTargrtCashed=null;
    }
        static float distanceThreadHold=8;
       static IEnumerator calculateWeight()
        {
           float maxWeight = 2;
            float currentWeight=0;
            float detectionInterval=0.2f;
            while (currentWeight< maxWeight)
            {
               // Debug.Log();
                currentWeight += EnemyWeight(currentfollowingTarget.GetlockedEnemyTransform().position, combatController.PlayerActualPosition, direction)* detectionInterval;
                yield return new WaitForSeconds(detectionInterval);
            }
            DisjesterLock();
            
        }
       static float EnemyWeight(Vector3 enemyPosition,Vector3 playerPosition,Vector2 playerDirection)
        {
            Vector3 distance = (enemyPosition - playerPosition);
            float distanceWeight = Mathf.Clamp((distance.magnitude - distanceThreadHold), 0, 10);
            float directionWeight = 1-Vector3.Dot(playerDirection.normalized, distance.normalized);
            if (directionWeight>=0.8)
            {
                distanceWeight = 2;
            }
            return distanceWeight* distanceWeight * directionWeight;
        }
        public static void CharaDie()
        {
            Debug.Log("dieed");
            OnCharaDie?.Invoke();
        }
        
        public static UnityEvent OnCharaDie;
    }

    
}