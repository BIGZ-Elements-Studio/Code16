using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public static MainCameraController Instance { get; private set; }

    public Camera Camera; // 相机对象
    public Camera Camera2;
    public Transform target; // 跟随的目标
    public Vector3 offset; // 相机距离目标的偏移量
    public float smoothSpeed = 0.1f; // 相机移动的平滑度
    // 2D模式下，两个角度y轴差异
    public float DOffset;
    // 3D模式下，相机角度
    public float Angle;

    public float defautFieldOfView;

    private Vector3 Actualoffset; // 实际使用的偏移量

    public Vector3 shakeOffset;
    public bool is2D { get; private set; } // 当前是否为2D模式

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetOffset()
    {
        // 设置相机的偏移量为相机位置与目标位置之间的差值
        offset = transform.position - target.position;
    }

    private void Start()
    {
        Actualoffset = offset;
    }

    public static void ChangeMode(bool targetIs2D)
    {
        if (targetIs2D)
        {
            // 切换为2D模式时，调整相机的偏移量、旋转和投影模式
            Instance.Actualoffset = new Vector3(Instance.Actualoffset.x, Instance.Actualoffset.y - Instance.DOffset, Instance.Actualoffset.z);
            Instance.transform.rotation = Quaternion.identity;
            Instance.Camera.orthographic = true;
            Instance.Camera2.orthographic = true;
        }
        else
        {
            // 切换为3D模式时，调整相机的旋转、偏移量和投影模式
            Instance.transform.rotation = Quaternion.Euler(Instance.Angle, 0, 0);
            Instance.Actualoffset = Instance.offset;
            Instance.Camera.orthographic = false;
            Instance.Camera2.orthographic = false;

        }
        Instance.is2D = targetIs2D;
    }

    private void Update()
    {
        // 根据偏移量计算相机的目标位置
        Vector3 desiredPosition = target.position + Actualoffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 将相机的位置设置为平滑后的目标位置
        transform.position = smoothedPosition;
        Camera.transform.localPosition = shakeOffset;
    }
    private static Coroutine changeCoroutine;
    private static shiftInfo originalShiftObj;
    private static float originalDuration;

    public static void DoShake(shiftInfo shiftObj, float duration,float backTime)
    {
        if (changeCoroutine != null)
        {
            Instance.StopCoroutine(changeCoroutine);
        }
        // Start the new shift coroutine
        changeCoroutine = Instance.StartCoroutine(Instance.DoChangeCoroutine(shiftObj, duration, backTime));
    }

    private IEnumerator DoChangeCoroutine(shiftInfo shiftObj, float duration,float backTime)
    {
        float elapsedTime = 0f;
        // Lerp the camera parameters over time
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Instance.shakeOffset = Vector3.Lerp(shiftObj.desireShift, shiftObj.desireShift,t);
         //   Instance.Angle = Mathf.Lerp(shiftObj.desireAngle, shiftObj.desireAngle, t);
          //  Instance.Camera.fieldOfView = Mathf.Lerp(shiftObj.additionalFieldOfView, Instance.defautFieldOfView + shiftObj.additionalFieldOfView, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the camera parameters to their final values
        Instance.shakeOffset = shiftObj.desireShift;
      //  Instance.Angle = shiftObj.desireAngle;
    //    Instance.Camera.fieldOfView = Instance.defautFieldOfView + shiftObj.additionalFieldOfView;
      //  changeCoroutine = null;
        elapsedTime = 0;
        while (elapsedTime < backTime)
        {
            float t = elapsedTime / backTime;
            Instance.shakeOffset = Vector3.Lerp(shiftObj.desireShift,Vector3.zero, t);
         //   Instance.Angle = Mathf.Lerp(startShiftObj.desireAngle, shiftObj.desireAngle, t);
          //  Instance.Camera.fieldOfView = Mathf.Lerp(startShiftObj.additionalFieldOfView, Instance.defautFieldOfView + shiftObj.additionalFieldOfView, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public class shiftInfo
    {
        public float additionalFieldOfView;
        public Vector2 desireShift;
        public float desireAngle;

    }
}
