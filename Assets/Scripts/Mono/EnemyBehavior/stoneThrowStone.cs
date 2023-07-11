using CombatSystem;
using Mono.Cecil.Cil;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class stoneThrowStone : MonoBehaviour
{
    public GameObject target { get { return combatController.Player; } }
    public GameObject flipTarget;
    public Transform initialPosition;
   public SphereBullet bullet;
    
    public BoneFollower handFollower;
    Coroutine come;
    Coroutine flipCoroutine;
    public SkeletonAnimation skeletonAnimation;
    Spine.AnimationState spineAnimationState { get { return skeletonAnimation.AnimationState; } }
    [SpineAnimation]
    public string breakAnimation;
    [SpineAnimation]
    public string idle;
    bool Hitable;
   public Transform thisTramsform;
    public IEnumerator process()
    {
        handFollower.enabled = true;
        spineAnimationState.SetAnimation(0,idle,true);
        Debug.Log(1);
        yield return new WaitForSeconds(0.6f);
        Debug.Log(2);
        Vector3 distance = target.transform.position - thisTramsform.position;
        Hitable =true;
        bullet.affectPlayer = true;
        bullet.affectEnemy = false;
        bullet.active();


        handFollower.enabled = false;

        float FlyTime = 1f;
        Vector3 DistanceEachTime = distance / (FlyTime/ Time.fixedDeltaTime);
        float passedTime = 0;
        while(passedTime < FlyTime)
        {
            passedTime += Time.fixedDeltaTime;
            thisTramsform.position = thisTramsform.position + DistanceEachTime;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void Awake()
    {
        //    bullet.active();
        come= StartCoroutine(process());
    }
    public void release()
    {
        handFollower.enabled = false;
        bullet.active();
       StartCoroutine(process());
    }
    public void HitPlayer(bool i)
    {
        Hitable =false;
        spineAnimationState.SetAnimation(0,breakAnimation,false);
        DistoryAfterSeconds.Destroy(gameObject,0.5f);
    }
    public IEnumerator flipProcess()
    {
        bullet.affectPlayer = false;
        bullet.affectEnemy = true;
         Vector3 distance = flipTarget.transform.position - thisTramsform.position;
        float FlyTime = 0.7f;
        Vector3 DistanceEachTime = distance / (FlyTime / Time.fixedDeltaTime);
        float passedTime = 0;
        while (passedTime < FlyTime)
        {
            passedTime += Time.fixedDeltaTime;
            thisTramsform.position = thisTramsform.position + DistanceEachTime;
            yield return new WaitForFixedUpdate();
        }


    }
   public void flip(string s, bool b)
    {
        
        if (!b|| !Hitable)
        {
            return;
        }
        Hitable=false;  
        if (come != null)
        {
            StopCoroutine(come);
        }
        flipCoroutine = StartCoroutine(flipProcess());
    }
}
