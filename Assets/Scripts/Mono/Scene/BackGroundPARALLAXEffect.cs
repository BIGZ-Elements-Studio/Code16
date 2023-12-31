using oct.cameraControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundPARALLAXEffect : MonoBehaviour
{
   public List<GameObject> background;
    public float parallaxSpeed;
    public Transform center;
    public Transform MostBackPosition;
    public Transform MostFrontPosition;
    float range;
    private void Awake()
    {
        range = MostBackPosition.position.z - MostFrontPosition.position.z;
    }
    private void Update()
    {
        findShift(MainCameraController.Instance.CameraTransform.position- center.position);
    }
    public void findShift(Vector3 playerPosition)
    {
        foreach (GameObject bg in background)
        {
            // Calculate the distance of the background object to the camera
            float DistanceFactor = getDistanceFactor(bg);

            // Calculate the modified parallax speed based on the distance
            float diplacementFactot = DistanceFactor * parallaxSpeed;

            // Calculate the new position for the background object
            Vector3 newPosition = new Vector3(
                playerPosition.x * diplacementFactot,
                playerPosition.y * diplacementFactot,
                bg.transform.localPosition.z
            );

            // Apply the new position
            bg.transform.localPosition = newPosition;
        }
    }

    // Calculate the distance of an object to the camera
    public float getDistanceFactor(GameObject target)
    {
        return ((target.transform.position.z - MostFrontPosition.position.z) /range);
    }
}
