using System;
using UnityEngine;

/// <remarks>ÍØÕ¹¹¤¾ßÀà£¬Èç£º<para>¶ÔSpine¶¯»­µÄ·â×°·½·¨</para><para>½«×ø±êµÄY,ZÖµ»¥»»µÄ·½·¨µÈ</para></remarks>
public static class ExtensionFunction
{
<<<<<<< HEAD
    //À©Õ¹·½·¨£º¶ÔSpine¶¯»­µÄ·â×°
=======
    /// <summary>
    /// Í£Ö¹µ±Ç°spine¶¯»­£¬²¢ÇÒ²¥·ÅĞÂ¶¯»­£¬
    /// </summary>
    //http://zh.esotericsoftware.com/spine-api-reference#AnimationState-setAnimation²Î¿¼ÍøÕ¾
>>>>>>> remotes/origin/çˆ±å¾·å
    public static Spine.TrackEntry SetAnimation(this Spine.AnimationState animationState, SkeletonAnimationData data)
    {
        return animationState.SetAnimation(data.trackIndex, data.animationName, data.loop);
    }

    //À©Õ¹·½·¨£º½«×ø±êµÄY,ZÖµ»¥»»
    public static Vector3 SetYToZ(this Vector3 vector3)
    {
        return new Vector3(vector3.x, vector3.z, vector3.y);
    }
    //À©Õ¹·½·¨£º½«¶şÎ»×ø±êµÄYÖµ¸³ÓèÈıÎ¬µÄZÖµ
    public static Vector3 SetYToZ(this Vector2 vector2)
    {
        return new Vector3(vector2.x, 0, vector2.y);
    }
}
