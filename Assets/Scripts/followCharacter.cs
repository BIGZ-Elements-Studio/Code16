using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCharacter : MonoBehaviour
{
    private void Update()
    {
        transform.position=combatController.PlayerActualPosition;
    }
}
