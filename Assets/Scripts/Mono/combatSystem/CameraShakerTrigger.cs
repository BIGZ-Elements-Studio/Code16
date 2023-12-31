using EZCameraShake;
using UnityEngine;
namespace oct.cameraControl
{

    public class CameraShakerTrigger : MonoBehaviour
    {
        [SerializeField]
        float magnitude;
        [SerializeField]
        float roughness;
        [SerializeField]
        float fadeInTime;
        [SerializeField]
        float fadeOutTime;

        public void doShake()
        {
            CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
        }
    }
}