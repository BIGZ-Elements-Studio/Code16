using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAfterSecond : MonoBehaviour
{
  public  MonoBehaviour target;
    public float time;
    private void OnEnable()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(time);
        target.enabled = true;
    }
}
