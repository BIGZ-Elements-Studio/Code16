using CombatSystem;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonsterAttribute : MonoBehaviour
{
   // public HPController HPcontroller;
    public Rigidbody rb;
    [SerializeField]
    public SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState spineAnimationState { get { return skeletonAnimation.AnimationState; } }

    public Vector3 movedirection;
    public float velocity;
    public bool controlVelocity;
    // Update is called once per frame

    public void setmovedirection(Vector3 v)
    {
        movedirection = v;
    }
    private void flip(bool toright)
    {
        StartCoroutine(lockProcess());
        if (toright)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void Awake()
    {
        controlVelocity = true;
    }
    private void Update()
    {
        if (allowFlip) {
            if (movedirection.x > 0.05)
            {
                flip(false);
            }
            else if (movedirection.x < 0.05)
            {
                flip(true);
            }
        }
        if (controlVelocity) {
            rb.velocity =new Vector3((movedirection * velocity).x,rb.velocity.y, (movedirection * velocity).z);
        }
        
    }
    bool allowFlip=true;
    IEnumerator lockProcess()
    {
        allowFlip = false;
        yield return new WaitForSecondsRealtime(0.5f);
        allowFlip = true;
    }
    public void SetAnimation(string s)
    {
        spineAnimationState.SetAnimation(0, s, true);
    }
    public bool isGrounded()
    {
        Vector3 boxCenter = rb.GetComponent<BoxCollider>().bounds.center;
        Vector3 boxSize = rb.GetComponent<BoxCollider>().size;

        // Use OverlapBox to detect colliders below the box
        // Shift the box center downwards by 0.05f to ensure it is detecting the ground
        Collider[] hits = Physics.OverlapBox(boxCenter - new Vector3(0, 0.05f, 0), boxSize / 2f, Quaternion.identity);

        // Loop through all the hits to see if any of them are considered "ground"
        foreach (Collider hit in hits)
        {

            if (hit.gameObject != rb && hit.isTrigger == false)
            {
                return true;
            }
        }

        return false;
    }
}
