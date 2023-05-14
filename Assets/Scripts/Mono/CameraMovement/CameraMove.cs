using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    Vector2 shiftVector;
    [SerializeField]
    float time;
    [SerializeField]
    float backTime;
    public void shake(bool i)
    {
        MainCameraController.shiftInfo shift = new MainCameraController.shiftInfo();
        shift.desireShift = shiftVector;
        MainCameraController.DoShake(shift, time, backTime);

    }
}
