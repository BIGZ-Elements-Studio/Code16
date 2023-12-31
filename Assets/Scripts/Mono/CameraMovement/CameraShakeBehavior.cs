using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace oct.cameraControl
{
    public class CameraShakeBehavior : MonoBehaviour
    {
        [SerializeField]
        GameObject cam;

        public struct ShakeInfo
        {
            public Vector2 displacement;
            public float duration;
        }

        private List<ShakeInfo> activeShakes = new List<ShakeInfo>();
        private Vector3 originalPosition;

        // Call this method to add a new shake effect to the camera
        public void AddShake(ShakeInfo shake)
        {
            activeShakes.Add(shake);
        }

        // Call this method to trigger a random shake with a given magnitude
        public void RandomShake(float magnitude)
        {
            ShakeInfo shake;
            shake.displacement = new Vector2(Random.Range(-magnitude, magnitude), Random.Range(-magnitude, magnitude));
            shake.duration = 0.5f; // You can adjust the duration as needed
            activeShakes.Add(shake);
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 totalDisplacement = Vector2.zero;

            // Iterate through the active shakes and calculate the total displacement
            for (int i = activeShakes.Count - 1; i >= 0; i--)
            {
                ShakeInfo shake = activeShakes[i];
                totalDisplacement += shake.displacement;

                shake.duration -= Time.deltaTime;
                if (shake.duration <= 0f)
                {
                    activeShakes.RemoveAt(i);
                }
                else
                {
                    activeShakes[i] = shake;
                }
            }

            // Apply the total displacement to the camera's position
            Vector3 cameraPosition = cam.transform.position;
            cameraPosition.x += totalDisplacement.x;
            cameraPosition.y += totalDisplacement.y;
            cam.transform.position = cameraPosition;
        }

        // Call this method to reset the camera position to its original value
        public void ResetCameraPosition()
        {
            cam.transform.position = originalPosition;
        }

        private void OnEnable()
        {
            originalPosition = cam.transform.position;
        }
    }
}