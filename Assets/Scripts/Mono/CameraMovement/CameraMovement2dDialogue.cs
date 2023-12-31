using CombatSystem;
using oct.cameraControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2dDialogue : CameraMovementController
{
    public Vector3 offset;
    public float z;
    public Vector3 playerPosition { get { return combatController.PlayerActualPosition; } }
    public Transform Npc;
    public override Vector3 GetDesirePosition()
    {
        return new Vector3((playerPosition.x+ Npc.position.x)/2+ offset.x, (playerPosition + offset).y, z);
    }
}
