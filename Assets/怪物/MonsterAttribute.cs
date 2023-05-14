using CombatSystem;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonsterAttribute : MonoBehaviour
{
    public Transform target;
    public HPController HPcontroller;
    public Rigidbody rb;
    public Vector3 Targetdirection { get { Vector3 direction = (target.transform.position - transform.position); return new Vector3(direction.x, 0, direction.z).normalized; }  }
    [SerializeField]
    public SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState spineAnimationState { get { return skeletonAnimation.AnimationState; } }

    public Vector3 movedirection;
    public float velocity;
    public bool controlVelocity;
    // Update is called once per frame
    private void flip(bool toright)
    {
        if (toright)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void Update()
    {
        if (movedirection.x>0.05)
        {
            flip(false);
        }
       else if (movedirection.x < 0.05)
        {
            flip(true);
        }
        if (controlVelocity) {
            rb.velocity = (movedirection * velocity);
        }
        
    }

    public void SetAnimation(string s)
    {
        spineAnimationState.SetAnimation(0, s, true);
    }
}
