using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInTeam : MonoBehaviour
{
    public Vector3 position;
   public void ActiveCharacter(bool active,Vector3 TargetPosition)
    {
        if (!active)
        {
            position=transform.position;
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        gameObject.transform.position = TargetPosition;   

    }
    public void distory()
    {
        
    }

}
