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
    //����״̬
    void IdleState()
    {
        Debug.Log("����״̬");
        //TODO
    }
    //�ƶ�״̬
    void MoveState()
    {
        //TODO
    }
    //�ƶ�״̬
    void DodgeState()
    {
        //TODO
    }
    //��ֱ״̬
    void RigidityState()
    {
        //TODO
    }
    //������״̬
    void ControlledState()
    {
        //TODO
    }
    //��������״̬
    void AttackContinueState()
    {
        //TODO
    }
    //�����ͷ�״̬
    void AttackReleaseState()
    {
        //TODO
    }
    //��������״̬
    void SkillContinueState()
    {
        //TODO
    }
    //�����ͷ�״̬
    void SkillReleaseState()
    {
        //TODO
    }
}
