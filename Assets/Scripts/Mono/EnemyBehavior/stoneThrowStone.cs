using CombatSystem;using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class stoneThrowStone : MonoBehaviour
{
    public GameObject target { get { return combatController.Player; } }
    public GameObject flipTarget;
    public GameObject self;
    public Rigidbody selfrb;
    public Transform initialPosition;
   public SphereBullet bullet;
    
    public BoneFollower handFollower;
    Coroutine come;
    public SkeletonAnimation skeletonAnimation;
    Spine.AnimationState spineAnimationState { get { return skeletonAnimation.AnimationState; } }
    [SpineAnimation]
    public string breakAnimation;
    [SpineAnimation]
    public string idle;
    bool Hitable;
   public Transform thisTramsform;
    public LayerMask groundLayer;
    public List<Rigidbody> bodies;
    public float magnitude;
   public UnityEvent breakStone;
    public void StoneBreak()
    {
        Hitable = false;
        StopAllCoroutines();
        spineAnimationState.SetAnimation(0, breakAnimation, true);
        foreach (var body in bodies)
        {
            body.isKinematic = false;
            body.useGravity = true;
            body.velocity=((new Vector3(Random.Range(-3,3), Random.Range(2, 5),0))*magnitude);
        }
        breakStone?.Invoke();
        DistoryAfterSeconds.Destroy(self, 0.5f);
    }
    public float spinspeed;
    public IEnumerator process()
    {
        
        handFollower.enabled = true;
        //spineAnimationState.SetAnimation(0,idle,true);
        yield return new WaitForSeconds(0.6f);
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
        Destroy(self);
    }

    private void Awake()
    {
        selfrb.maxAngularVelocity = 50;
        spineAnimationState.SetAnimation(0, idle, true);
        come = StartCoroutine(process());
    }
    private void FixedUpdate()
    {
        selfrb.angularVelocity = new Vector3(0, 0, 1) * spinspeed;
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
        StoneBreak();
    }
    public void HitGround(Collider other)
    {
        if(groundLayer == (groundLayer | (1 << other.gameObject.layer))){
            StoneBreak();
        }
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
        StartCoroutine(flipProcess());
    }
}
