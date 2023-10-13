using CombatSystem;
using CombatSystem.boss.stoneperson;
using EZCameraShake;
using Scene;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnterBossScene : MonoBehaviour
{
    [SerializeField]
    List<RandonBreak> breakWalls;
    public Transform bossPositionInitiall;
    public Transform bossPositionFinal;
    public GameObject boss;
    [SerializeField]
    EnemyHandsControl bossSript;
    public Collider trigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent(typeof(playerSettings)) != null)
        {
            StartCoroutine(startBoss());
        }
    }
    public float magnitude;
    public float roughness;
    public float fadeInTime;
    public float fadeOutTime;
    public AnimationCurve curve;
    IEnumerator startBoss()
    {
        trigger.enabled = false;
        GameModeController.SetModeTo(false);
        yield return new WaitForSeconds(2f);
        foreach(RandonBreak randonBreak in breakWalls)
        {
            
            yield return new WaitForSeconds(0.5f);
            randonBreak.BreakLeaf();CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
        }
        var shake = CameraShaker.Instance.StartShake(magnitude, roughness, fadeInTime);
        StartCoroutine(Move());
        yield return new WaitForSeconds(2f);
        shake.StartFadeOut(fadeOutTime);
        //yield return new WaitForSeconds(0.5f);
        CameraShaker.Instance.ShakeOnce(magnitude+2, roughness, fadeInTime, fadeOutTime);
        yield return new WaitForSeconds(1.5f);
        // Ensure the final position is reached exactly
        bossSript.StartCoroutine(bossSript.Phaseoverall());

    }

    public IEnumerator Move()
    {
        float elapsedTime = 0f;
        float totalTime = 3.5f;
        Vector3 initialPosition = bossPositionInitiall.position;
        Vector3 finalPosition = bossPositionFinal.position;
        
        while (elapsedTime < totalTime)
        {
            // Calculate the new position based on the lerp factor
            float t = elapsedTime / totalTime;
            //boss.transform.position = Vector3.Lerp(initialPosition, finalPosition, curve.Evaluate(elapsedTime));
            boss.transform.position = initialPosition + (finalPosition - initialPosition) * curve.Evaluate(elapsedTime);
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }; boss.transform.position = finalPosition;
    }
    private void Start()
    {
        boss.transform.position = bossPositionInitiall.position;
    }
}
