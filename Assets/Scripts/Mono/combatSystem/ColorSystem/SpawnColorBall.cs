using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColorBall : MonoBehaviour
{
   public float range;
    public float hight;
    public Transform position;
    public void createBall(CombatColor c)
    {
        Debug.Log("called");
        GameObject g = Instantiate((combatColorController.Instance.ColorBall));
        g.transform.position = position.position;
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        g.GetComponent<Rigidbody>().velocity= new Vector3(Mathf.Sin(randomAngle)*range, hight, Mathf.Cos(randomAngle) * range);
        g.GetComponent<colorball>().color= c;
    }
}
