using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeModeControll : MonoBehaviour
{
    [SerializeField]
    PlayerControllerTwoD twoDScript;
    [SerializeField]
    PlayerControllerThreeD threeDScript;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            changeMode(true);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            changeMode(false);
        }
    }

    private void changeMode(bool v)
    {
        twoDScript.enabled = v;
        threeDScript.enabled = !v;
    }
}
