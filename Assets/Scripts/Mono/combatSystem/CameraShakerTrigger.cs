using EZCameraShake;
using oct.cameraControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraShakeBehavior;

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