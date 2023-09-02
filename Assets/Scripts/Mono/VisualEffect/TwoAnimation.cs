using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class TwoAnimation : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    Spine.AnimationState spineAnimationState { get { return skeletonAnimation.AnimationState; } }
    [SpineAnimation]
    public string first;
    [SpineAnimation]
    public string second;
    private void Awake()
    {
        spineAnimationState.SetAnimation(0, first, false);
        spineAnimationState.AddAnimation(0, second, true,0);
    }
}
