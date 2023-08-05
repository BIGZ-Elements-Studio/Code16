using CombatSystem;
using oct.ObjectBehaviors;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    private bool IsGrounded()
    {

        Vector3 boxCenter = transform.position + c.center;
        Vector3 boxHalfExtents = c.size * 0.5f;
        bool isGrounded = Physics.BoxCast(boxCenter, boxHalfExtents, Vector3.down, Quaternion.identity, groundCheckDistance, groundMask);
        changeGround?.Invoke(Groundname,isGrounded);
        if (isGrounded) {
            coolDown?.Invoke(cooldownname, true);
        }
        
        return isGrounded;
    }
    public IEnumerator inAir()
    {
        lockState(true);
        yield return new WaitForFixedUpdate();
        SetAnimation(air);
        rb.useGravity = true;
        yield return new WaitForSecondsRealtime(0.5f);
        while (!IsGrounded())
        {
            yield return null;
        }
        // cheak grounded by waitforfixupdate in a while loop
        lockState(false);
    }

    public IEnumerator jump()
    {
        lockState(true);
        speed = 8;
        //set skill3releaseSpeed
        SetAnimation(jumpc);
        yield return new WaitForSeconds(0.1f);
        float velocity = Mathf.Sqrt(2 * MinjumpHeight * Physics.gravity.magnitude);
        rb.velocity = new Vector3(direction * speed, velocity, 0);
        yield return new WaitForSeconds(0.1f);
        changeGround?.Invoke(Groundname, false);
        lockState(false);
    }
    PlayerInput inputActions;
    private void Awake()
    {
        inputActions = new PlayerInput();
        inputActions.In2d.Enable();
        inputActions.In2d.move.performed += ctx => { direction = ctx.ReadValue<float>(); };
        inputActions.In2d.move.canceled += ctx => { direction =0 ; };
    }
    private void Start()
    {
        StartCoroutine(inAir());
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
    }

    public IEnumerator idle()
    {
        lockState(true);
        speed = 0;
        UpdateVelocity = true;
        yield return new WaitForSeconds(0.2f);

        SetAnimation(idleb);
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
