using BehaviorControlling;
using CombatSystem;
using oct.ObjectBehaviors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInTeamTwoD : MonoBehaviour
{
    public Vector3 position { get { return gameObject.transform.position; } }
    [SerializeField]
    BehaviorController controller;
    [SerializeField]
    public Transform ActualTransform;
    public IndividualProperty properties;
    public bool faceright;
    public void ActiveCharacter(bool active, Vector3 TargetPosition, bool TofaceRight)
    {
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
