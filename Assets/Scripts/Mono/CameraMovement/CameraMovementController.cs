using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraMovementController: MonoBehaviour
{
    public abstract Vector3 GetDesirePosition();
}
