using CombatSystem.team;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RestartController : MonoBehaviour
{
    public Transform restartPostion;
    public playerTeamController playerTeam;
    public void restart()
    {
        playerTeam.LoadTeam();
        GameModeController.SetModeTo(true);
        InputController.allow2dInput = true;
        GameModeController.allowChange=true;
        playerTeam.setPosition(restartPostion.position);
        Debug.Log(GameModeController.Is2d);
        GameModeController.restat();
    }
}
