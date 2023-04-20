using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerThreeD : Controller
{
    SkeletonAnimation skeletonAnimation;
    Rigidbody rb;
    string nowState;
    bool _stateStart;
    TrackEntry nowEntry;
    float stateTime = 0;

    private void OnEnable()
    {
        ChangeState("IdleState");
    }
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


    #region 3d
    //没有改完但是大概意思是3d不能有任何ChangeState（2d state情况）
    //待机状态
    void IdleState()
    {
        if (InputController.DirectionAxis != Vector2.zero)
        {
            ChangeState("WalkState");
        }
        if (InputController.Fire2 > 0)
        {
            ChangeState("Skill");
        }
    }
    //移动状态

    void WalkState()
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
        if (InputController.Fire2 > 0)
        {
            ChangeState("Skill");
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

    //闪避状态

    void DodgeState()
    {

    }
    //#僵直状态#
    void RigidityState()
    {
        //TODO
    }
    //#被控制状态#
    void ControlledState()
    {
        //TODO
    }
    //#攻击蓄力状态#
    void AttackContinueState()
    {
        //TODO
    }
    //#攻击释放状态#
    void AttackReleaseState()
    {
        //TODO
    }
    //#技能蓄力状态#
    void SkillContinueState()
    {
        //TODO
    }
    //#技能释放状态#
    void SkillReleaseState()
    {
        //TODO
    }

    #endregion

}
