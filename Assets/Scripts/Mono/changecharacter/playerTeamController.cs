using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class playerTeamController : MonoBehaviour
{
    public List<GameObject> prefab=new List<GameObject>();
    public List<PlayerInTeam> playerInTeams = new List<PlayerInTeam>();
    public PlayerInTeam Chara2d;
    [SerializeField]
    private int CurrentCharacterIndex;
    public bool allowChange=true;
    public PlayerInTeam CurrentCharacter { get { return playerInTeams[CurrentCharacterIndex]; } }
    public List<characterState> characterStates { get { return playerInTeams[CurrentCharacterIndex].characterStates; } }
    #region Ui
    public UnityEvent<int, int> onSpChangeWithMaxSp;
    public UnityEvent<int, int> onColorChangeWithMaxColor;
    public UnityEvent<CombatColor> onChangeColor;
    public UnityEvent<int, int> onHPChangeWithMaxHP;
    #endregion
    public void swtichCharacter(int index)
    {
        if (!allowChange)
        {
            return;
        }
        playerInTeams[CurrentCharacterIndex].ActiveCharacter(false, playerInTeams[CurrentCharacterIndex].position, playerInTeams[CurrentCharacterIndex].faceright);
        playerInTeams[CurrentCharacterIndex].properties.onSpChangeWithMaxSp .RemoveListener( invokeChangeSp);
        playerInTeams[CurrentCharacterIndex].properties.onColorChangeWithMaxColor .RemoveListener(invokeChangeColor);

        playerInTeams[index].ActiveCharacter(true, playerInTeams[CurrentCharacterIndex].position, playerInTeams[CurrentCharacterIndex].faceright);
         playerInTeams[index].properties.onSpChangeWithMaxSp .AddListener( invokeChangeSp);
        playerInTeams[index].properties.onColorChangeWithMaxColor.AddListener(invokeChangeColor);

        onChangeColor.Invoke(playerInTeams[index].properties.color);
        onSpChangeWithMaxSp?.Invoke(playerInTeams[index].properties.MaxSp, playerInTeams[index].properties.currentsp);
        onColorChangeWithMaxColor.Invoke(10,playerInTeams[index].properties.colorBar);
        CurrentCharacterIndex =index;
    }

    void invokeChangeSp(int maxSp, int current)
    {
        onSpChangeWithMaxSp?.Invoke(maxSp, current);
    }

    void invokeChangeColor(int maxSp, int current)
    {
        onColorChangeWithMaxColor?.Invoke(maxSp, current);
    }
    private void Start()
    {
        foreach (PlayerInTeam team in playerInTeams)
        {
            team.ActiveCharacter(false, team.position,true);
        }
        swtichCharacter(1);
        GameModeController.ModeChangediFTo2D += changeMode;
    }

    void changeMode(bool To2d)
    {
        if (To2d)
        {
            playerInTeams[CurrentCharacterIndex].ActiveCharacter(false, playerInTeams[CurrentCharacterIndex].position, playerInTeams[CurrentCharacterIndex].faceright);
            Chara2d.ActiveCharacter(true, playerInTeams[CurrentCharacterIndex].position, playerInTeams[CurrentCharacterIndex].faceright);
        }
        else {
            playerInTeams[CurrentCharacterIndex].ActiveCharacter(true, Chara2d.position,false);
            Chara2d.ActiveCharacter(false, playerInTeams[CurrentCharacterIndex].position,false);
        }

    }
    void loadTeam()
    {

    }
    void distoryCurrent()
    {

    }
}
