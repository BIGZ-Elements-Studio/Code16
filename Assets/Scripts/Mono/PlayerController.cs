using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

/// <remarks>角色控制类，主要用来控制角色的状态,其中状态以方法的形式呈现</remarks>
public class PlayerController : Controller
{
    SkeletonAnimation skeletonAnimation;
    Rigidbody rb;
    string nowState;
    bool _stateStart;
    TrackEntry nowEntry;
    float stateTime=0;

    public override void AddState(string stateName)
    {
        base.AddState(stateName);
        _stateStart = true;
        if (SkeletonAnimationData.Data.ContainsKey("Player_" + stateName))
            nowEntry = skeletonAnimation.AnimationState.SetAnimation(SkeletonAnimationData.Data["Player_" + stateName]);
        switch (stateName)
        {
            default:
                break;
        }
    }

    public void ChangeState(string stateName)
    {
        if (!string.IsNullOrEmpty(nowState))
            RemoveState(nowState);
        AddState(stateName);
        stateTime = 0;
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
        stateTime= Time.deltaTime;
        base.Update();

    }

    //待机状态
    void IdleState()
    {
        if (InputController.DirectionAxis != Vector2.zero)
        {
            ChangeState("WalkState");
        }
        if (InputController.Space > 0)
        {
            ChangeState("JumpState");
        }
        if (InputController.Fire1 > 0)
        {
            ChangeState("AttackState");
        }
        if (InputController.Fire2 > 0)
        {
            ChangeState("SkillState");
        }
    }
    //移动状态
    void WalkState()
    {
        rb.velocity = (InputController.DirectionAxis * Config.PlayerConfig.WalkSpeed).SetYToZ();
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
        if (InputController.Fire3 > 0)
        {
            ChangeState("RunState");
        }
        if (InputController.Space > 0)
        {
            ChangeState("JumpState");
        }
        if (InputController.Fire1 > 0)
        {
            ChangeState("AttackState");
        }
        if (InputController.Fire2 > 0)
        {
            ChangeState("SkillState");
        }
    }
    //跑步状态
    void RunState()
    {
        rb.velocity = (InputController.DirectionAxis * Config.PlayerConfig.RunSpeed).SetYToZ();
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
        if (InputController.Fire3 == 0)
        {
            ChangeState("WalkState");
        }
        if (InputController.Space > 0)
        {
            ChangeState("JumpState");
        }
        if (InputController.Fire1 > 0)
        {
            ChangeState("AttackState");
        }
        if (InputController.Fire2 > 0)
        {
            ChangeState("SkillState");
        }
    }
    //跳跃状态
    void JumpState()
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
        if (InputController.Fire1 > 0)
        {
            ChangeState("AttackState");
        }
    }
    void AttackState()
    {
        if (_stateStart)
        {
            if (nowEntry != null)
                nowEntry.Complete += (t) => { ChangeState("IdleState"); };
            _stateStart = false;
        }
    }
    void SkillState()
    {
        if (_stateStart)
        {
            if (nowEntry != null)
                nowEntry.Complete += (t) => { ChangeState("IdleState"); };
            _stateStart = false;
        }
    }
    //闪避状态
    void DodgeState()
    {

    }
    //僵直状态
    void RigidityState()
    {
        //TODO
    }
    //被控制状态
    void ControlledState()
    {
        //TODO
    }
    //攻击蓄力状态
    void AttackContinueState()
    {
        //TODO
    }
    //攻击释放状态
    void AttackReleaseState()
    {
        //TODO
    }
    //技能蓄力状态
    void SkillContinueState()
    {
        //TODO
    }
    //技能释放状态
    void SkillReleaseState()
    {
        //TODO
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plane" && nowState == "JumpState" && rb.velocity.y <= 0)
        {
            ChangeState("IdleState");
        }
    }
}
