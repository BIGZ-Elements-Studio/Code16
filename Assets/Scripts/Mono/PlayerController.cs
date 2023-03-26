using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerController : Controller
{
    SkeletonAnimation skeletonAnimation;
    Rigidbody rb;
    string nowState;
    bool _stateStart;
    TrackEntry nowEntry;

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
        base.Update();
    }

    //´ý»ú×´Ì¬
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
            ChangeState("Attack");
        }
        if (InputController.Fire2 > 0)
        {
            ChangeState("Skill");
        }
    }
    //ÒÆ¶¯×´Ì¬
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
            ChangeState("Attack");
        }
        if (InputController.Fire2 > 0)
        {
            ChangeState("Skill");
        }
    }
    //ÅÜ²½×´Ì¬
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
            ChangeState("Attack");
        }
        if (InputController.Fire2 > 0)
        {
            ChangeState("Skill");
        }
    }
    //ÌøÔ¾×´Ì¬
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
            ChangeState("Attack");
        }
    }
    void Attack()
    {
        if (_stateStart)
        {
            if (nowEntry != null)
                nowEntry.Complete += (t) => { ChangeState("IdleState"); };
            _stateStart = false;
        }
    }
    void Skill()
    {
        if (_stateStart)
        {
            if (nowEntry != null)
                nowEntry.Complete += (t) => { ChangeState("IdleState"); };
            _stateStart = false;
        }
    }
    //ÉÁ±Ü×´Ì¬
    void DodgeState()
    {

    }
    //½©Ö±×´Ì¬
    void RigidityState()
    {
        //TODO
    }
    //±»¿ØÖÆ×´Ì¬
    void ControlledState()
    {
        //TODO
    }
    //¹¥»÷ÐîÁ¦×´Ì¬
    void AttackContinueState()
    {
        //TODO
    }
    //¹¥»÷ÊÍ·Å×´Ì¬
    void AttackReleaseState()
    {
        //TODO
    }
    //¼¼ÄÜÐîÁ¦×´Ì¬
    void SkillContinueState()
    {
        //TODO
    }
    //¼¼ÄÜÊÍ·Å×´Ì¬
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
