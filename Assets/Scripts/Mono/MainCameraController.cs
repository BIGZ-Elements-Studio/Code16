using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public static MainCameraController Instance { get; private set; }

    public Camera Camera; // �������
    public Transform target; // �����Ŀ��
    public Vector3 offset; // �������Ŀ���ƫ����
    public float smoothSpeed = 0.1f; // ����ƶ���ƽ����
    // 2Dģʽ�£������Ƕ�y�����
    public float DOffset;
    // 3Dģʽ�£�����Ƕ�
    public float Angle;

    private Vector3 Actualoffset; // ʵ��ʹ�õ�ƫ����
    public bool is2D { get; private set; } // ��ǰ�Ƿ�Ϊ2Dģʽ

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
        }
        else
        {
            // �л�Ϊ3Dģʽʱ�������������ת��ƫ������ͶӰģʽ
            Instance.transform.rotation = Quaternion.Euler(Instance.Angle, 0, 0);
            Instance.Actualoffset = Instance.offset;
            Instance.Camera.orthographic = false;
        }
        Instance.is2D = targetIs2D;
    }

    private void LateUpdate()
    {
        // ����ƫ�������������Ŀ��λ��
        Vector3 desiredPosition = target.position + Actualoffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // �������λ������Ϊƽ�����Ŀ��λ��
        transform.position = smoothedPosition;
    }
}