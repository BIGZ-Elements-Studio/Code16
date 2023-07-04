using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneThrowStone : MonoBehaviour
{
    public GameObject target { get { return combatController.Player; } }
    public GameObject flipTarget;
    public Transform initialPosition;
   public SphereBullet bullet;
    Coroutine come;
    Coroutine flipCoroutine;
    public IEnumerator process()
    {
        transform.position= initialPosition.position;
        bullet.affectPlayer = true;
        bullet.affectEnemy = false;
        yield return new WaitForSeconds(2);
        Vector3 distance = target.transform.position - transform.position;
        float FlyTime = 1f;
        Vector3 DistanceEachTime = distance / (FlyTime/ Time.fixedDeltaTime);
        float passedTime = 0;
        while(passedTime < FlyTime)
        {
            passedTime += Time.fixedDeltaTime;
            transform.position = transform.position + DistanceEachTime;
            yield return new WaitForFixedUpdate();
        }


    }

    private void Awake()
    {
        bullet.active();
        StartCoroutine(overall());
    }
    public IEnumerator overall()
    {
        while (true)
        {
            come = StartCoroutine(process());
            yield return new WaitForSeconds(5);
        }
    }
    public IEnumerator flipProcess()
    {
        bullet.affectPlayer = false;
        bullet.affectEnemy = true;
         Vector3 distance = flipTarget.transform.position - transform.position;
        float FlyTime = 0.7f;
        Vector3 DistanceEachTime = distance / (FlyTime / Time.fixedDeltaTime);
        float passedTime = 0;
        while (passedTime < FlyTime)
        {
            passedTime += Time.fixedDeltaTime;
            transform.position = transform.position + DistanceEachTime;
            yield return new WaitForFixedUpdate();
        }


    }
   public void flip(string s, bool b)
    {
        if (!b)
        {
            return;
        }
        if (come != null)
        {
            StopCoroutine(come);
        }
        flipCoroutine = StartCoroutine(flipProcess());
    }
}
