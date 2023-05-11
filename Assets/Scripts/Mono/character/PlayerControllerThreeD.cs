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
    [SerializeField]
    GameObject DamageBox;
    [SerializeField]

    GameObject positionBox;
    [SerializeField]

    GameObject graphic;
    #region ����
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

    #endregion

    #region ״̬
    //û�и��굫�Ǵ����˼��3d�������κ�ChangeState��2d state�����
    //����״̬
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
    //�ƶ�״̬
    
    void WalkState()
    {
        rb.velocity = (InputController.DirectionAxis * Config.PlayerConfig.RunSpeed).SetYToZ();
        //
        if (InputController.DirectionAxis.x > 0)
        {
            graphic.transform.rotation = new Quaternion(0, 0, 0, 1);
        }
        if (InputController.DirectionAxis.x < 0)
        {
            graphic.transform.rotation = new Quaternion(0, 1, 0, 0);
        }
        if (InputController.DirectionAxis == Vector2.zero)
        {
            ChangeState("IdleState");
        }
    }
    #endregion

}
