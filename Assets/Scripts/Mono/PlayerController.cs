using UnityEngine;

/// <remarks>角色控制类，主要用来控制角色的状态</remarks>
public class PlayerController : Controller
{


    protected override void Start()
    {
        base.Start();
        AddState("IdleState");
    }
    protected override void Update()
    {
        base.Update();

    }
    //待机状态
    void IdleState()
    {

        Debug.Log("待机状态");
        //TODO
    }
    //移动状态
    void MoveState()
    {


        //TODO

    }
    //移动状态
    void DodgeState()
    {
        //TODO
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
}
