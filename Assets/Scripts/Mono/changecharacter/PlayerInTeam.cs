using BehaviorControlling;
using CombatSystem;
using oct.ObjectBehaviors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInTeam : MonoBehaviour
{
    public Vector3 position { get { return gameObject.transform.position; } }
    public List<characterState> characterStates { get { return controller.characterStates; } }
    [SerializeField]
    BehaviorController controller;
    [SerializeField]
    PlayerAttribute a;
    public IndividualProperty properties;
    public bool faceright;
   public void ActiveCharacter(bool active,Vector3 TargetPosition,bool TofaceRight)
    {
        if (!active)
        {
            if (a!=null) {
                faceright = a.faceRight;
            }
            gameObject.SetActive(false);
            return;
        }
        if (a != null)
        {
            a.faceRight = TofaceRight;
        }
        gameObject.SetActive(true);
        gameObject.transform.position = TargetPosition;
        controller.LockState = false;
        StartCoroutine(wait1Frame());
    }
    IEnumerator wait1Frame()
    {
        yield return null;
        yield return null;
        controller.CheakCondition();
    }
    public void distory()
    {
        
    }

}
