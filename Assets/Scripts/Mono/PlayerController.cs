using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //´ý»ú×´Ì¬
    void IdleState()
    {
        Debug.Log("´ý»ú×´Ì¬");
        //TODO
    }
    //ÒÆ¶¯×´Ì¬
    void MoveState()
    {
        //TODO
    }
    //ÒÆ¶¯×´Ì¬
    void DodgeState()
    {
        //TODO
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
}
