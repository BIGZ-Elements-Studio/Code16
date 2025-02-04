using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于存储骨骼动画信息 以便调用
public struct SkeletonAnimationData
{
    public static Dictionary<string, SkeletonAnimationData> Data = new Dictionary<string, SkeletonAnimationData>()
    {
        {"Player_IdleState",new SkeletonAnimationData(0,"idle_3",true) },
        {"Player_WalkState",new SkeletonAnimationData(0,"walk",true) },
        {"Player_RunState",new SkeletonAnimationData(0,"run",true) },
        {"Player_JumpState",new SkeletonAnimationData(0,"jump",false) },
        {"Player_Attack",new SkeletonAnimationData(0,"shield_attack",false) },
        {"Player_Skill",new SkeletonAnimationData(0,"buff_1",false) },
    };
    public int trackIndex;
    public string animationName;
    public bool loop;

    public SkeletonAnimationData(int trackIndex, string animationName, bool loop)
    {
        this.trackIndex = trackIndex;
        this.animationName = animationName;
        this.loop = loop;
    }
}
