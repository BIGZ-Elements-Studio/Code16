using System;
using UnityEngine;

public static class ExtensionFunction
{
    //扩展方法：对Spine动画的封装
    public static Spine.TrackEntry SetAnimation(this Spine.AnimationState animationState, SkeletonAnimationData data)
    {
        return animationState.SetAnimation(data.trackIndex, data.animationName, data.loop);
    }

    //扩展方法：将坐标的Y,Z值互换
    public static Vector3 SetYToZ(this Vector3 vector3)
    {
        return new Vector3(vector3.x, vector3.z, vector3.y);
    }
    //扩展方法：将二位坐标的Y值赋予三维的Z值
    public static Vector3 SetYToZ(this Vector2 vector2)
    {
        return new Vector3(vector2.x, 0, vector2.y);
    }
}
