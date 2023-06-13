using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public static MainCameraController Instance { get; private set; }

    public Camera Camera; // �������
    public Camera Camera2;
    public Transform target; // �����Ŀ��
    public Vector3 offset; // �������Ŀ���ƫ����
    public float smoothSpeed = 0.1f; // ����ƶ���ƽ����
    // 2Dģʽ�£������Ƕ�y�����
    public float DOffset;
    // 3Dģʽ�£�����Ƕ�
    public float Angle;

    public float defautFieldOfView2d;
    public float defautFieldOfView3d;
    private Vector3 Actualoffset; // ʵ��ʹ�õ�ƫ����

    public Vector3 shakeOffset;
    public float Z2d;
    public bool is2D { get; private set; } // ��ǰ�Ƿ�Ϊ2Dģʽ

    private void Awake()
    {
        GameModeController.ModeChangediFTo2D += ChangeMode;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Instance.Camera.orthographicSize = Instance.defautFieldOfView2d;
        Instance.Camera2.orthographicSize = Instance.defautFieldOfView2d;
    }

    public void SetOffset()
    {
        // ���������ƫ����Ϊ���λ����Ŀ��λ��֮��Ĳ�ֵ
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
            // �л�Ϊ2Dģʽʱ�����������ƫ��������ת��ͶӰģʽ
            Instance.Actualoffset = new Vector3(Instance.Actualoffset.x, Instance.Actualoffset.y - Instance.DOffset, Instance.Actualoffset.z);
            Instance.transform.rotation = Quaternion.identity;
            Instance.Camera.orthographic = true;
            Instance.Camera2.orthographic = true;
        }
        else
        {
            // �л�Ϊ3Dģʽʱ�������������ת��ƫ������ͶӰģʽ
            Instance.transform.rotation = Quaternion.Euler(Instance.Angle, 0, 0);
            Instance.Actualoffset = Instance.offset;
            Instance.Camera.orthographic = false;
            Instance.Camera2.orthographic = false;
            Instance.Camera.fieldOfView = Instance.defautFieldOfView3d;
            Instance.Camera2.fieldOfView = Instance.defautFieldOfView3d;

        }
        Instance.is2D = targetIs2D;
    }

    private void Update()
    {
        // ����ƫ�������������Ŀ��λ��
        
        Vector3 desiredPosition = target.position + Actualoffset;
        if (is2D)
        {
            desiredPosition = new Vector3(desiredPosition.x, desiredPosition. y, Z2d);
        }
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // �������λ������Ϊƽ�����Ŀ��λ��
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
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the camera parameters to their final values
        Instance.shakeOffset = shiftObj.desireShift;
        elapsedTime = 0;
        while (elapsedTime < backTime)
        {
            float t = elapsedTime / backTime;
            Instance.shakeOffset = Vector3.Lerp(shiftObj.desireShift,Vector3.zero, t);
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
