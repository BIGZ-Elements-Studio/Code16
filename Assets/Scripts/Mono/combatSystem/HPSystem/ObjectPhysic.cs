using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPhysic : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent<GameObject> distoryed;
   public void onPoiseChange(float Remaining)
    {
        if (Remaining <= 0)
        {
            Destroy(gameObject);
            
        }
    }
}
