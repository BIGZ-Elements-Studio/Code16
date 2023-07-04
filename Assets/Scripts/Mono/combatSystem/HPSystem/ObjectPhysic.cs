using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPhysic : MonoBehaviour
{
    // Start is called before the first frame update
   public void onPoiseChange(string s, float Remaining)
    {
        if (Remaining <= 0)
        {
            Destroy(gameObject);
        }
    }
}
