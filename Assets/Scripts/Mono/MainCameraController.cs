using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public static MainCameraController Instance { get; private set; }

    public Camera Camera; // 相机对象
    public Transform target; // 跟随的目标
    public Vector3 offset; // 相机距离目标的偏移量
    public float smoothSpeed = 0.1f; // 相机移动的平滑度
    // 2D模式下，两个角度y轴差异
    public float DOffset;
    // 3D模式下，相机角度
    public float Angle;

    private Vector3 Actualoffset; // 实际使用的偏移量
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
        }
        else
        {
            // 切换为3D模式时，调整相机的旋转、偏移量和投影模式
            Instance.transform.rotation = Quaternion.Euler(Instance.Angle, 0, 0);
            Instance.Actualoffset = Instance.offset;
            Instance.Camera.orthographic = false;
        }
        Instance.is2D = targetIs2D;
    }

    private void LateUpdate()
    {
        // 根据偏移量计算相机的目标位置
        Vector3 desiredPosition = target.position + Actualoffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 将相机的位置设置为平滑后的目标位置
        transform.position = smoothedPosition;
    }
}