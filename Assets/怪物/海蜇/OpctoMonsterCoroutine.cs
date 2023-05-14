using oct.generatedBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using codeTesting;
using CombatSystem;
using UnityEngine.EventSystems;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Unity.VisualScripting;

public class OpctoMonsterCoroutine : MoveableControlCoroutine
{
    [SpineAnimation]
    public string moveName;
    public float moveSpeed;
    [SerializeField]
    MonsterAttribute controller;
    [SpineAnimation]
    public string AtkName;

    public GameObject BulletPrefeb;
    [SerializeField]
    BehaviorController b;
    public Vector3 HitDirectiom;
    private void Start()
    {
        StartCoroutine(RandomAtk());
    }
    public void recevedForce(Vector3 vector3)
    {
        HitDirectiom=vector3;
    }
    private IEnumerator RandomAtk()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            b.setBoolVariable("¹¥»÷", true);
            yield return null;
            b.setBoolVariable("¹¥»÷", false);
        }
    }
    public IEnumerator move()
    {
        controller.SetAnimation(moveName);
        while (true)
        {
            yield return null;
            controller.velocity = moveSpeed;
            controller.movedirection = controller.Targetdirection;

        }

    }
    public IEnumerator hited()
    {    
        lockState(true);    
       // controller.controlVelocity = false;
        yield return null;
        yield return new WaitForFixedUpdate();
        float v = HitDirectiom.magnitude / controller.rb.mass/60;
        controller.movedirection=HitDirectiom;
        float time=0;
        while (time<0.2)
        {
            yield return null;
            time += Time.deltaTime;
            controller.velocity = (v - (v *( time / 0.3f)))/10; 
        }
        yield return new WaitForSeconds(0.1F);
        controller.controlVelocity = true;
        lockState(false);
    }
    public IEnumerator Atk()
    {
        yield return null;
        lockState(true);
        controller.velocity = 0;
        controller.movedirection = controller.Targetdirection;
        controller.SetAnimation(AtkName);
       GameObject g= Instantiate(BulletPrefeb);
        g.transform.position = new Vector3(controller.target.transform.position.x,2.68f, controller.target.transform.position.z);
        yield return new WaitForSeconds(1f);
        lockState(false);
    }

    public IEnumerator Wondering()
    {
        controller.SetAnimation(moveName);
        while (true)
        {
            
            float speed = (Random.value) * 1.5f+2;
            float Totaltime = Random.value+1;
            float passedTime = 0;
            Vector3 perpendicularDirection = new Vector3(controller.movedirection.z, 0f, controller.movedirection.x).normalized;
            if (Random.value>0.5) {
                perpendicularDirection *= -1;
            }
            controller.movedirection = perpendicularDirection;
            while (Totaltime>passedTime) {
                yield return null;
                controller.velocity = speed;
                passedTime += Time.deltaTime;
            }
            controller.velocity = 0;
            float i = (Random.value-0.3f)*1.5f;
            if (i<0)
            {
                i = 0;
            }
            yield return new WaitForSeconds(i);
            Debug.Log(i);
        }
    }
}
