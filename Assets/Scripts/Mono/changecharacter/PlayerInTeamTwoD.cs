using BehaviorControlling;
using CombatSystem;
using oct.ObjectBehaviors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatSystem.team
{
    public class PlayerInTeamTwoD : MonoBehaviour
    {
        public Vector3 position { get { return gameObject.transform.position; } }
        [SerializeField]
        BehaviorController controller;
        [SerializeField]
        public Transform ActualTransform;

        public bool faceright;
        [SerializeField]
        sampleCharacterCoroutineTwoD a;
        public void ActiveCharacter(bool active, Vector3 TargetPosition, bool TofaceRight)
        {
            if (!active)
            {
                if (a != null)
                {
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
}