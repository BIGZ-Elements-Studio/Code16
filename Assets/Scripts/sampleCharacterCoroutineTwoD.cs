using oct.ObjectBehaviors;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sampleCharacterCoroutineTwoD : MoveableControlCoroutine
{


    [SerializeField]
    PlayerAttribute PlayerControllerr;

    [SpineAnimation]
    public string runa;
    [SpineAnimation]
    public string idleb;
    [SpineAnimation]
    public string jumpc;
    [SpineAnimation]
    public string air;
    public float jumpHight;
    public IEnumerator jump()
    {
        lockState(true);
        Debug.Log("jumpppp");
        yield return null;
        PlayerControllerr.UpdateVelocity = false;
        PlayerControllerr.Rigidbody.velocity = new Vector3(PlayerControllerr.Rigidbody.velocity.x, jumpHight, PlayerControllerr.Rigidbody.velocity.z);
        PlayerControllerr.SetAnimation(jumpc);
        yield return new WaitForSeconds(0.1f);
        PlayerControllerr.UpdateVelocity = true;
        PlayerControllerr.jump(1);
        lockState(false);

    }

    public IEnumerator idle()
    {
        lockState(false);
        yield return null;
        PlayerControllerr.speed = 0;
        
        PlayerControllerr.SetAnimation(idleb);
    }

    public IEnumerator run()
    {
        yield return null;
        PlayerControllerr.speed = 8;
        PlayerControllerr.SetAnimation(runa);
        lockState(true);
        yield return new WaitForSeconds(0.1f);
        lockState(false);
    }
    public IEnumerator inAir()
    {
        yield return null;
        PlayerControllerr.speed = 5;
        PlayerControllerr.SetAnimation(air);
        lockState(false);
    }


}
