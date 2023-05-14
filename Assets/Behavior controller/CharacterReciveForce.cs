using CombatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReciveForce : MonoBehaviour
{
    public HPController controller;
    public Rigidbody rb;
    private void Start()
    {
        controller .AddedForce.AddListener( hited);
    }

    private void hited(Vector3 vector)
    {
        if (!controller.armor) {
            if (controller.Poise < 30)
            {
              //  rb.AddForce(new Vector3(rb.velocity.x * rb.mass + vector.x, rb.velocity.y * rb.mass + vector.y, rb.velocity.z * rb.mass + vector.z)*75);
            }
            else if (controller.Poise < 50)
            {
               // rb.AddForce(new Vector3(rb.velocity.x * rb.mass + vector.x, rb.velocity.y * rb.mass + vector.y, rb.velocity.z*rb.mass + vector.z) *0.5f *75);
            }
        }
    }
}
