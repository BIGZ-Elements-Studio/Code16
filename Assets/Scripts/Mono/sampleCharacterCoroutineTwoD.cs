
using EZCameraShake;
using oct.ObjectBehaviors;
using oct.world;
using Spine.Unity;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class sampleCharacterCoroutineTwoD : MoveableControlCoroutine
{

    [SpineAnimation]
    public string runa;
    [SpineAnimation]
    public string idleb;
    [SpineAnimation]
    public string jumpc;
    [SpineAnimation]
    public string jumpd;
    [SpineAnimation]
    public string air;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    BoxCollider c;
    public bool UpdateVelocity;
    public float MinjumpHeight;
    public float MaxJumpTime;
    public float direction;
    public bool faceRight { get { return _faceRight; } set { if (_faceRight != value) { _faceRight = value; flip(value); } } }
    [SerializeField]
    public SkeletonAnimation skeletonAnimation;
    Spine.AnimationState spineAnimationState { get { return skeletonAnimation.AnimationState; } }
    public void SetAnimation(string s)
    {
        spineAnimationState.SetAnimation(0, s, true);
    }

    private void flip(bool toright)
    {
        if (toright)
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
        else
        {
            transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
        }
    }

    private bool _faceRight=true;
    public int speed;
   public LayerMask groundMask;
    public float groundCheckDistance;
    public string Groundname;
    public string cooldownname;
    public UnityEvent<string, bool> changeGround;
    public UnityEvent<string, bool> coolDown;
    public bool CanTurn3d()
    {
        Vector3 boxCenter = transform.position + c.center;
        Vector3 boxHalfExtents = c.size * 0.5f;
        Collider[] hits = Physics.OverlapBox(boxCenter - new Vector3(0, groundCheckDistance, 0), boxHalfExtents, Quaternion.identity, groundMask);
        foreach(Collider collider in hits)
        {

            if (collider.GetComponent<BlockConfig>() != null)
            {
                return collider.GetComponent<BlockConfig>().allow3d;
            }
        }
        return false;
    }
    private bool IsGrounded()
    {

        Vector3 boxCenter = transform.position + c.center;
        Vector3 boxHalfExtents = c.size * 0.5f;
        RaycastHit m_Hit;
       // bool isGrounded = Physics.BoxCast(boxCenter, boxHalfExtents, Vector3.down, out m_Hit, Quaternion.identity, groundCheckDistance, groundMask);
        Collider[] hits = Physics.OverlapBox(boxCenter - new Vector3(0, groundCheckDistance, 0), boxHalfExtents, Quaternion.identity, groundMask);
        changeGround?.Invoke(Groundname, (hits.Count()>=1));
        if ((hits.Count() >= 1)) {
           
            coolDown?.Invoke(cooldownname, true);
        //  StartCoroutine(  DoShake(-rb.velocity.y));
        }
        return (hits.Count() >= 1);
    }
    bool ground;
    IEnumerator DoShake(float velocityY)
    {
        // yield return new WaitForSeconds(0.15f);
        while (!Physics.BoxCast(transform.position + c.center, c.size * 0.5f, Vector3.down, Quaternion.identity, 0.5f, groundMask))
        {
            //Debug.Log("notYet");
            yield return new WaitForFixedUpdate();
        } 
         if (velocityY > 15){
            CameraShaker.Instance.ShakeOnce(1, 1.5f, 0.2f, 0.2f);
        }else if (velocityY>5)
        {
           
            CameraShaker.Instance.ShakeOnce(0.5f, 1, 0.1f, 0.1f);
        }
    }
    public IEnumerator inAir()
    {
        lockState(true);
        yield return new WaitForFixedUpdate();
        SetAnimation(air);
        //rb.useGravity = true;
        yield return new WaitForSecondsRealtime(0.5f);
        while (!ground)
        {
            yield return new WaitForFixedUpdate();
        }
        // cheak grounded by waitforfixupdate in a while loop
        lockState(false);
    }
    bool justJumped;
    bool alreadyCanceled;
    public IEnumerator jump()
    {
        lockState(true);
        speed = 8;
        justJumped=true;
        alreadyCanceled=false;
        //set skill3releaseSpeed
        spineAnimationState.SetAnimation(0, jumpc, false);
        yield return new WaitForSeconds(0.1f);
        float velocity = Mathf.Sqrt(2 * MinjumpHeight * Physics.gravity.magnitude);
        rb.velocity = new Vector3(direction * speed, velocity, 0);
        yield return new WaitForSeconds(0.1f);
        changeGround?.Invoke(Groundname, false);
        if (!alreadyCanceled) {
            rb.useGravity = false;
        }
        StartCoroutine(floatMaxTime());
        lockState(false);
    }
    
    IEnumerator cancelFLy()
    {
        yield return null;
        alreadyCanceled=true;
        if (justJumped)
        {
            rb.useGravity = true;
        }
        justJumped = false;
    }
    IEnumerator floatMaxTime()
    {
        yield return new WaitForSeconds(MaxJumpTime);
      StartCoroutine(  cancelFLy());
    }
    PlayerInput inputActions;
    bool allowInput { get { return InputController.allow2dInput && enabled; } }
    void refreshInputaction()
    {
        Debug.Log(allowInput);
        if (allowInput)
        {
            inputActions.Enable();
        }
        else
        {
            inputActions.Disable();
        }
    }
    private void Awake()
    {
        inputActions = new PlayerInput();
        inputActions.In2d.Enable();
        inputActions.In2d.move.performed += ctx => { direction = ctx.ReadValue<float>(); };
        inputActions.In2d.move.canceled += ctx => { direction =0 ; };
    }
    private void OnEnable()
    {
        inputActions?.In2d.Enable();
    }
    private void OnDisable()
    {
        inputActions.In2d.Disable();
    }
    private void Start()
    {
        InputController.allow2dInputChanged?.AddListener(delegate { refreshInputaction(); });
        StartCoroutine(inAir());
        inputActions.In2d.jump.canceled += ctx => {StartCoroutine( cancelFLy()); };
    }

    private void FixedUpdate()
    {
        
        if (direction > 0)
        {
            faceRight = true;
        }
        else if (direction < 0)
        {
            faceRight = false;
        }
           rb.velocity = new Vector3(direction* speed, rb.velocity.y, 0);
        ground= IsGrounded();
    }

    public IEnumerator idle()
    {
        lockState(true);
        speed = 0;
        UpdateVelocity = true;
        SetAnimation(idleb);
        yield return new WaitForSeconds(0.2f);

       
    }


    public IEnumerator run()
    {
        yield return null;
        speed = 8;
        UpdateVelocity = true;
        SetAnimation(runa);
        lockState(true);
        yield return new WaitForSeconds(0.1f);
        lockState(false);
    }


}
