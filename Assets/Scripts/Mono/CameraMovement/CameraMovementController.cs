using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace oct.cameraControl
{
    public abstract class CameraMovementController : MonoBehaviour
    {
        public abstract Vector3 GetDesirePosition();
    }
}