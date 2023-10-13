using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public Transform target;
    Vector3 distace;
    void Start()
    {
        distace= transform.position - target.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = target.position+ distace;
    }
}
