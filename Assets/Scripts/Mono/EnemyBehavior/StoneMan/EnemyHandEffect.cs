using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyHandEffect : MonoBehaviour
{

    public float movementDuration = 2f; // Adjust this as needed
    public float Amplitude = 1f;  // Adjust upper curve amplitude
    Vector3 startTransform;
   public Vector3 endTransform;
    public float angle;
    [SerializeField]
    Renderer rd ;
    public void Move(Vector3 a, Vector3 b, float duration)
    {
       // StartCoroutine(MoveObject(a, b, duration));
    }
    public void fadeIn()
    {
        rd.enabled = true;
    }
    public void fadeOut()
    {
        rd.enabled = false;
    }
    public void PopIn()
    {
        rd.enabled = true;
    }

    public void PopOut()
    {
        rd.enabled = false;
    }
    private IEnumerator MoveObject(Vector3 start, Vector3 end, float duration)
    {
        movementDuration = duration;
        startTransform = start;
        endTransform = end;
        float elapsedTime = 0f;

        while (elapsedTime < movementDuration)
        {
            float t = Mathf.Clamp01(elapsedTime / movementDuration);
            gameObject.transform.position = SCurvePosition(t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the object reaches the end position exactly
        transform.position = end;
    }

    // Custom S-curve easing function with parameters
    private Vector3 SCurvePosition(float t)
    {
        // Use a parametric equation to calculate the S-curv position
        float x = Mathf.Lerp(startTransform.x, endTransform.x, t);
        float y = Mathf.Lerp(startTransform.y, endTransform.y, t);
        float z = Mathf.Lerp(startTransform.z, endTransform.z, t);

        // Apply the S-curve shape to the y-coordinate
        var magnitude = Mathf.Sin(2 * t * Mathf.PI) * Amplitude; // You can adjust the amplitude (2f) as needed

        float xOffset = Mathf.Sin(angle);
        float yOffset = Mathf.Cos(angle);
        Vector3 offset = new Vector3(xOffset, yOffset) * magnitude;
        return new Vector3(x, y, z) + offset;
    }

}