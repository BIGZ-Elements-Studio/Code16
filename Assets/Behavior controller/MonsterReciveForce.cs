using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterReciveForce : MonoBehaviour
{
    [SerializeField]
    Rigidbody m_Rigidbody;
    [SerializeField]
    OpctoMonsterCoroutine c;
    // Start is called before the first frame update
    public void recevedForce(Vector3 vector3)
    {
        Debug.Log(vector3);
        m_Rigidbody.AddForce(vector3*10);
    }
}
