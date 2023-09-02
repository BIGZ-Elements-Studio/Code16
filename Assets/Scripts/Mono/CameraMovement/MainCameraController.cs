using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace oct.cameraControl
{
    public class MainCameraController : MonoBehaviour
    {
        public CameraMovementController Movement2d;
        public CameraMovementController Movement3d;
        public CameraShakeBehavior ShakeBehavior;
        private CameraMovementController currentMovementController;  
        public static MainCameraController Instance { get; private set; }

        public Camera Camera; // �������
        public Camera Camera2;
      //  public Transform target; // �����Ŀ��
     //   public Vector3 offset; // �������Ŀ���ƫ����
        public float dammping = 0.1f; // ����ƶ���ƽ����
                                         // 2Dģʽ�£������Ƕ�y�����
        public float DOffset;
        // 3Dģʽ�£�����Ƕ�
        public float Angle;

        public float defautFieldOfView2d;
        public float defautFieldOfView3d;
        public float Z2d;
        public bool is2D { get; private set; } // ��ǰ�Ƿ�Ϊ2Dģʽ
        private Vector3 velocity = Vector3.zero;
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
            currentMovementController = Movement2d;
        }


        private void Start()
        {
            //Actualoffset = offset;
        }

        public static void ChangeMode(bool targetIs2D)
        {
            if (targetIs2D)
            {

                Instance.transform.rotation = Quaternion.identity;
                Instance.Camera.orthographic = true;
                Instance.Camera2.orthographic = true;
                Instance.currentMovementController = Instance.Movement2d;
            }
            else
            {
                // �л�Ϊ3Dģʽʱ�������������ת��ƫ������ͶӰģʽ
                Instance.transform.rotation = Quaternion.Euler(Instance.Angle, 0, 0);
                Instance.Camera.orthographic = false;
                Instance.Camera2.orthographic = false;
                Instance.Camera.fieldOfView = Instance.defautFieldOfView3d;
                Instance.Camera2.fieldOfView = Instance.defautFieldOfView3d;
                Instance.currentMovementController = Instance.Movement3d;
            }
            Instance.is2D = targetIs2D;
        }

        private void Update()
        {


            // ����ƫ�������������Ŀ��λ��
           
           // Vector3 desiredPosition = target.position + Actualoffset;
            if (is2D)
            {
              //  desiredPosition = new Vector3(desiredPosition.x, desiredPosition.y, Z2d);
            }
            Vector3 desiredPosition = currentMovementController.GetDesirePosition();
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, dammping*Time.timeScale);
           
            // �������λ������Ϊƽ�����Ŀ��λ��
            transform.position = smoothedPosition;
        }

    }
}