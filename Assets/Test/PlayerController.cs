using Spine.Unity;
using UnityEngine;

namespace codeTesting
{
    public class PlayerController : Controller
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
        public bool faceRight { get { return _faceRight; } set { if (_faceRight!=value) {  _faceRight = value;flip(value); }  } }
        private bool _faceRight;
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
            rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.y * speed);
        }
       public void SetAnimation(string s)
        {
            spineAnimationState.SetAnimation(0,s,true);
        }
    }
}
