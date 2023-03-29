using System;
using UnityEngine;

/// <remarks>��չ�����࣬�磺<para>��Spine�����ķ�װ����</para><para>�������Y,Zֵ�����ķ�����</para></remarks>
public static class ExtensionFunction
{
    //��չ��������Spine�����ķ�װ
    public static Spine.TrackEntry SetAnimation(this Spine.AnimationState animationState, SkeletonAnimationData data)
    {
        return animationState.SetAnimation(data.trackIndex, data.animationName, data.loop);
    }

    //��չ�������������Y,Zֵ����
    public static Vector3 SetYToZ(this Vector3 vector3)
    {
        return new Vector3(vector3.x, vector3.z, vector3.y);
    }
    //��չ����������λ�����Yֵ������ά��Zֵ
    public static Vector3 SetYToZ(this Vector2 vector2)
    {
        return new Vector3(vector2.x, 0, vector2.y);
    }
}
