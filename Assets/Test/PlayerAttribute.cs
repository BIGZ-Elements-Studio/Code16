using Spine.Unity;
using System;
using System.Collections;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace codeTesting
{
    public class PlayerAttribute : Controller
    {
        [SerializeField]
       public SkeletonAnimation skeletonAnimation;
        [SerializeField]
        public GameObject DamageBox;
        [SerializeField]
        public GameObject positionBox;
        [SerializeField]
        public GameObject graphic;
        public PlayerInput input;
        Rigidbody rb;
        Vector3 direction;
        public float speed;
        public Spine.AnimationState spineAnimationState;
        [SerializeField]BehaviorController behaviourController2d;
        [SerializeField] BehaviorController behaviourController3d;
        public bool in2d { get { return _in2d; }set { if (_in2d!=value) { changeMode(value); } } }

        public void changeMode(bool to2d)
        {
            behaviourController2d.enabled = to2d;
            behaviourController3d.enabled = !to2d;
            _in2d = to2d;
            if (to2d) {
                behaviourController2d.storedState = -1;
                behaviourController2d.CheakCondition();
                behaviourController2d.setBoolVariable("ดฅตุ", grounded);
            }
            else
            {
                behaviourController3d.storedState = -1;
                behaviourController3d.LockState = false;
            }
        }

        private bool _in2d;
        public bool faceRight { get { return _faceRight; } set { if (_faceRight!=value) {  _faceRight = value;flip(value); }  } }
        private bool _faceRight;

        public bool grounded { get { return isGrounded(); } }

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

        private void flip(bool toright)
        {
            if (toright)
            {
                graphic.transform.rotation=Quaternion.Euler(0,0,0);
            }
            else
            {
                graphic.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }


        private void Awake()
        {
            
            spineAnimationState = skeletonAnimation.AnimationState;
            input = new PlayerInput();
            input.Enable();
            rb = DamageBox.GetComponent<Rigidbody>();
            input.In3d.run.performed += ctx => {  if (isActiveAndEnabled) { direction = ctx.ReadValue<Vector2>(); } };
                
        }
        private void Update()
        {
            if (direction.x > 0)
            {
                faceRight=true;
            }
            else if (direction.x < 0)
            {
                faceRight=false;
            }
            float i = 0;
            if (!in2d)
            {
                i = direction.y * speed;
            }
            rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, i);
        }
       public void SetAnimation(string s)
        {
            spineAnimationState.SetAnimation(0,s,true);
        }

        internal void jump(int v)
        {
            rb.velocity = new Vector3(rb.velocity.x, 10f, rb.velocity.z);
            StartCoroutine(jumpprocess());
        }

        private IEnumerator jumpprocess()
        {
            bool j=true;
            behaviourController2d.setBoolVariable("ดฅตุ", false);
            while (j){
                yield return new WaitForFixedUpdate();
                j = !grounded;
            }
            behaviourController2d.setBoolVariable("ดฅตุ",true);
        }
    }
}
