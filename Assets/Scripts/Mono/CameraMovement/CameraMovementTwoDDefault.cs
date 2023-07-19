using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementTwoDDefault :  CameraMovementController
{
        public Vector3 offset;
    public float z;
        public Vector3 playerPosition { get { return combatController.PlayerActualPosition; } }
        public override Vector3 GetDesirePosition()
        {
            return new Vector3( (playerPosition + offset).x, (playerPosition + offset).y, z);
        }
        
}
