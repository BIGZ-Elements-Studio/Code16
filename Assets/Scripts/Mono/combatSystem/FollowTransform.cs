using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
  public  Transform target;
   public Vector3 offset;
    private void LateUpdate()
    {
        if (target!=null)
        {
            transform.position = target.position+offset;
        }
    }
}
