using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeController : MonoBehaviour
{
    private void OnEnable()
    {
        BuffTable.icons = Resources.Load<BuffTable>("BuffTable").sprites;
    }
    public delegate void handler(bool i);
    #region changeD: GameModeController.ModeChangediFTo2D(if 2d)
    public static bool Is2d { get; private set; }
    public static handler ModeChangediFTo2D;

    public static bool allowChange=true;
    static GameModeController g;

    private void Awake()
    {
        Is2d = false;
        g = this;
        isCharacerActive = true;
    }
    private void Start()
    {
        SetModeTo(true);
    }
    public static void changeMode()
    {
        SetModeTo(!Is2d);
    }
    public static bool SetModeTo(bool IsTarget2D)
    {
            if (allowChange&&IsTarget2D != Is2d)
            {
                Is2d = IsTarget2D;
                ModeChangediFTo2D?.Invoke(Is2d);
            }
        return allowChange;
    }

  /*  public static bool ForceSetModeTo(bool IsTarget2D)
    {

        if (IsTarget2D != Is2d)
        {
            Is2d = IsTarget2D;
            ModeChangediFTo2D?.Invoke(Is2d);
        }

        return TeamController.currentCharaGrounded;
    }*/
    #endregion

    #region changeCharacterActivation: GameModeController.CharacterChangedIfToActive(ifChangeToActive)

    public static handler CharacterChangedIfToActive;
    private bool isCharacerActive;
    public static void SetCharaActivation(bool isActive)
    {

        if (isActive != g.isCharacerActive)
        {
            g.isCharacerActive = isActive;
            CharacterChangedIfToActive?.Invoke(g.isCharacerActive);
        }
    }
    #endregion
    // if there are anything that changes timescalse before pause, and wants to continue it after pause, please set the timescale again in the resume function
    #region pause: GameModeController.PauseGame(Pause)

    public static handler PauseGame;
    public static void SetPause(bool ifPause)
    {
        if (ifPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        PauseGame?.Invoke(ifPause);
    }
    #endregion
}

