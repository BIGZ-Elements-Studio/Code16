using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField]
    Transform effectPoint;
    public float rotationRange;
    public float Scale;
    public void MakeEffect()
    {
        GameObject g = Instantiate(combatController.defaultEffect);
        g.transform.position = effectPoint.position;

        // Generate a random rotation around the Z-axis within the rotationRange
        float randomRotationZ = Random.Range(-rotationRange, rotationRange);
        Vector3 newRotation = new Vector3(0f, 0f, randomRotationZ);
        g.transform.rotation = Quaternion.Euler(newRotation);
        g.transform.localScale=new Vector3(Scale/2, Scale/2, Scale/2);
    }
}
