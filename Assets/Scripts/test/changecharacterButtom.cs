using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changecharacterButtom : MonoBehaviour
{
    public int num;
  public  playerTeamController c;

    public void change()
    {
        c.swtichCharacter(num);
    }
}
