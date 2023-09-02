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

        public Camera Camera; // 相机对象
        public Camera Camera2;
      //  public Transform target; // 跟随的目标
     //   public Vector3 offset; // 相机距离目标的偏移量
        public float dammping = 0.1f; // 相机移动的平滑度
                                         // 2D模式下，两个角度y轴差异
        public float DOffset;
        // 3D模式下，相机角度
        public float Angle;

        public float defautFieldOfView2d;
        public float defautFieldOfView3d;
        public float Z2d;
        public bool is2D { get; private set; } // 当前是否为2D模式
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
                // 切换为3D模式时，调整相机的旋转、偏移量和投影模式
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


            // 根据偏移量计算相机的目标位置
           
           // Vector3 desiredPosition = target.position + Actualoffset;
            if (is2D)
            {
              //  desiredPosition = new Vector3(desiredPosition.x, desiredPosition.y, Z2d);
            }
            Vector3 desiredPosition = currentMovementController.GetDesirePosition();
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, dammping*Time.timeScale);
           
            // 将相机的位置设置为平滑后的目标位置
            transform.position = smoothedPosition;
        }

    }
}