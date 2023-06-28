using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTeamController : MonoBehaviour
{
    public List<GameObject> prefab=new List<GameObject>();
    public List<PlayerInTeam> playerInTeams = new List<PlayerInTeam>();
    public int currentCharacter;

    public bool allowChange=true;
   public void swtichCharacter(int index)
    {
        
        if (!allowChange)
        {
            return;
        }
        playerInTeams[currentCharacter].ActiveCharacter(false, playerInTeams[currentCharacter].position);
        playerInTeams[index].ActiveCharacter(true, playerInTeams[currentCharacter].position);
        currentCharacter=index;
    }
    private void Start()
    {
        swtichCharacter(1);
    }
    void loadTeam()
    {

    }
    void distoryCurrent()
    {

    }
    public int target;
    public void changeChara()
    {
        swtichCharacter(target);
    }
}
