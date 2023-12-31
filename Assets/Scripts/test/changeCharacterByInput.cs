using CombatSystem;
using CombatSystem.team;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCharacterByInput : MonoBehaviour
{
    PlayerInput input;
    public playerTeamController c;
    private void Awake()
    {
        input=new PlayerInput();
        input.changeChara._1.performed += ctx => { c.swtichCharacter(0); };
        input.changeChara._2.performed += ctx => { c.swtichCharacter(1); };
        input.changeChara._3.performed += ctx => { c.swtichCharacter(2); };
        GameModeController.ModeChangediFTo2D += change;
    }

    private void change(bool i)
    {
        if (i)
        {
            input.Disable();
        }
        else
        {
            input.Enable();
        }
    }


}
