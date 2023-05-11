using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistoryAfterSeconds : MonoBehaviour
{
    [SerializeField]
    float waitTime;
    [SerializeField]
    bool UseUnfixTime;
    [SerializeField]
    float targetScale;
    [SerializeField]
    float MaxTime;
    void Start()
    {
        StartCoroutine(wait());

    }

    IEnumerator wait()
    {
        Vector3 vector3 = transform.localScale;
        Vector3 final = transform.localScale* targetScale;
        float time = 0;
        while (time < MaxTime)
        {
            yield return null;
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(vector3, final, time/MaxTime);
        }
        time = 0;
        while (time < waitTime-MaxTime)
        {
            yield return null;
            transform.localScale = Vector3.Lerp(vector3, Vector3 .one/1.1f, time/(waitTime - MaxTime));
            time += Time.deltaTime;
        }
        Destroy(gameObject);
    }


}
