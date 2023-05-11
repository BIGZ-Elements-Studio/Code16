using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTwoD : Controller
{
    SkeletonAnimation skeletonAnimation;
    Rigidbody rb;
    string nowState;
    bool _stateStart;
    TrackEntry nowEntry;
    float stateTime = 0;


    public override void AddState(string stateName)
    {
        base.AddState(stateName);
        _stateStart = true;
      //  if (SkeletonAnimationData.Data.ContainsKey("Player_" + stateName))

            //nowEntry = skeletonAnimation.AnimationState.SetAnimation(SkeletonAnimationData.Data["Player_" + stateName]);

        switch (stateName)
        {
            default:
                break;
        }
    }
    private void OnEnable()
    {
        ChangeState("moveTwoD");
    }
    public void ChangeState(string stateName)
    {
        if (!string.IsNullOrEmpty(nowState))
            RemoveState(nowState);
        AddState(stateName);

        nowState = stateName;
    }
    protected override void Start()
    {
        base.Start();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        rb = GetComponent<Rigidbody>();
        ChangeState("IdleState");
    }
    protected override void Update()
    {
        stateTime += Time.deltaTime;
        base.Update();

    }


    #region 2D
    //没有改完但是大概意思是2d不能有任何ChangeState（3d state情况）
    void moveTwoD()
    {
        rb.velocity = new Vector3((InputController.DirectionAxis * Config.PlayerConfig.RunSpeed).SetYToZ().x, 0, 0);
        if (InputController.DirectionAxis.x > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
        }
        if (InputController.DirectionAxis.x < 0)
        {
            transform.rotation = new Quaternion(0, 1, 0, 0);
        }
        if (InputController.DirectionAxis == Vector2.zero)
        {
            ChangeState("IdleState");
        }
        if (InputController.Space > 0)
        {
            ChangeState("JumpStateTwoD");
        }
    }
    //跳跃状态

    void JumpStateTwoD()
    {
        if (_stateStart)
        {
            rb.AddForce(new Vector3(0, Config.PlayerConfig.JumpVelocity, 0), ForceMode.VelocityChange);
            _stateStart = false;
        }
        rb.velocity = (InputController.DirectionAxis * Config.PlayerConfig.RunSpeed).SetYToZ() + new Vector3(0, rb.velocity.y, 0);
        if (InputController.DirectionAxis.x > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
        }
        if (InputController.DirectionAxis.x < 0)
        {
            transform.rotation = new Quaternion(0, 1, 0, 0);
        }
    }
    void IdleState()
    {
        if (InputController.DirectionAxis != Vector2.zero)
        {
            ChangeState("moveTwoD");
        }
        if (InputController.Fire1 > 0)
        {
            ChangeState("JumpStateTwoD");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plane" && nowState == "JumpStateTwoD" && rb.velocity.y <= 0)
        {
            ChangeState("IdleState");
        }
    }
    #endregion 
}
