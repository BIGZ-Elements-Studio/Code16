using BehaviorControlling;
using Spine.Unity;
using System;
using System.Collections;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;
using CombatSystem;
using Spine;

namespace oct.ObjectBehaviors
{
    public class PlayerAttribute : Controller
    {
        [SerializeField]
       public SkeletonAnimation skeletonAnimation;

        Spine.AnimationState spineAnimationState { get { return skeletonAnimation.AnimationState; } }
        [SerializeField]
        public GameObject DamageBox;
        [SerializeField]
        public GameObject positionBox;
        [SerializeField]
        public GameObject graphic;
        public Rigidbody Rigidbody;
        public PlayerInput input;
        Vector3 direction;
        public float speed;
        [SerializeField]
        public IndividualProperty property;
        [SerializeField] BehaviorController behaviourController3d;
        public UnityEvent<string, bool> onDashChange;
        public UnityEvent<string, bool> onAmored;
        public UnityEvent<bool> Amored;
        public int combo;
        public bool in2d;
        public Collider PositionCollider;
        public bool faceRight { get { return _faceRight; } set { if (_faceRight != value) { _faceRight = value; flip(value); } } }
        private bool _faceRight;

        public bool grounded { get { return isGrounded(); } }

        public bool UpdateVelocity;
        #region setVariable

       public void playEffect(ParticleSystem effect)
        {
            if (faceRight)
            {
                effect.GetComponent<ParticleSystemRenderer>().flip = new Vector3(0,0,0);
            }
            else
            {
                effect.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
            }
        }
        public void armoed(bool i)
        {
            onAmored?.Invoke("霸体", i);
            Amored.Invoke(i);
        }
        public void dash(bool i)
        {
            onAmored?.Invoke("霸体", i);
            Amored.Invoke(i);
            onDashChange?.Invoke("闪避中",i);
        }
        public void SetAnimation(string s)
        {
            spineAnimationState.SetAnimation(0,s,true);
        }
        public void SetAnimationNoRepeate(string s)
        {
            spineAnimationState.SetAnimation(0, s, false);
        }
        public void SetAnimationTimeScale(float time)
        {
            spineAnimationState.TimeScale=time;
        }
        #endregion

        public bool isGrounded()
        {
            Vector3 boxCenter = DamageBox.GetComponent<BoxCollider>().bounds.center;
            Vector3 boxSize = DamageBox.GetComponent<BoxCollider>().size;

            // Use OverlapBox to detect colliders below the box
            // Shift the box center downwards by 0.05f to ensure it is detecting the ground
            Collider[] hits = Physics.OverlapBox(boxCenter - new Vector3(0, 0.05f, 0), boxSize / 2f, Quaternion.identity);

            // Loop through all the hits to see if any of them are considered "ground"
            foreach (Collider hit in hits)
            {
                
                if (hit.gameObject != DamageBox&& hit.isTrigger==false)
                {
                    return true;
                }
            }
            return false;
        }




        private void Awake()
        {
            flip(true);
            //spineAnimationState = skeletonAnimation.AnimationState;
            input = new PlayerInput();
            input.Enable();
            input.In3d.run.performed += ctx => {  if (isActiveAndEnabled) { direction = (ctx.ReadValue<Vector2>()).normalized; } };
                
        }
        private void OnEnable()
        {
            if (input!=null) {
                input.Enable();
            }
            direction = Vector3.zero;
        }
        private void OnDisable()
        {
            input.Disable();
        }

        private void FixedUpdate()
        {
            if (direction.x > 0)
            {
                faceRight=true;
            }
            else if (direction.x <0)
            {
                faceRight=false;
            }
            if (UpdateVelocity) {
                if (!in2d)
                {
                    Rigidbody.velocity = new Vector3(direction.x * speed* property.moveSpeedFactor, Rigidbody.velocity.y, direction.y * speed* property.moveSpeedFactor);
                }
                else
                {
                    Rigidbody.velocity = new Vector3(direction.x * speed, Rigidbody.velocity.y,0);

                }
            }
        }


        private void flip(bool toright)
        {
            if (toright)
            {
                graphic.transform.localScale=new Vector3(1,1,1);
            }
            else
            {
                graphic.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        #region 子弹
        internal void createBullet(GameObject bulletPrefeb, Transform bulletPosition,float staggerTime)
        {
            GameObject g = Instantiate(bulletPrefeb);
            g.transform.position = bulletPosition.position;
            g.transform.localScale = bulletPosition.lossyScale;
            Bullet b = g.transform.GetChild(0).GetComponent<Bullet>();
            b.faceRight = faceRight;
            b.AtkValue = property.actualAtk;
            b.critcAtkRate= property.actualCritcRate;
            b.critcAtkDamage= property.actualCritcDamage;
            b.hit.AddListener((eventValue) =>
            {
                Stagger(eventValue, staggerTime);
            });
            b.gainSp.AddListener((eventValue) =>
            {
               property.GainSp(eventValue);
            });

        }
        
        private void Stagger(bool arg0,float time)
        {

            if (arg0)
            {
                StartCoroutine(stopAnimation(time));
            }
        }
        IEnumerator stopAnimation(float time)
        {
            skeletonAnimation.timeScale = 0;
            yield return new WaitForSecondsRealtime(time);
            skeletonAnimation.timeScale = 1;

        }
        #endregion
    }
}
