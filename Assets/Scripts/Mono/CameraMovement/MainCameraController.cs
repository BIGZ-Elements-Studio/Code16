using System.Collections;
using UnityEngine;

namespace oct.cameraControl
{
    public class MainCameraController : MonoBehaviour
    {
        public CameraMovementController Movement2d;
        public CameraMovement2dDialogue Npc2dController;
        public CameraMovementController Movement3d;
        public CameraShakeBehavior ShakeBehavior;
        private CameraMovementController currentMovementController;
        [SerializeField] Transform _CameraPosition;
        public Transform CameraTransform { get { return _CameraPosition; } }
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
            transform.position = currentMovementController.GetDesirePosition();
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
        public static void setToDialogueMode(Transform npc)
        {
            (Instance.Npc2dController).Npc = npc;
           Instance. currentMovementController = Instance.Npc2dController;
            Instance.StartCoroutine(Instance.ChangeFOV(Instance.Camera2.orthographicSize, Instance. targetFovInDialogue, 0.3f));
        }
       public float targetFovInDialogue;
        public static void EndDialogueMode()
        {
            Instance.currentMovementController = Instance.Movement2d;
            Instance.StartCoroutine(Instance. ChangeFOV(Instance.targetFovInDialogue, Instance.defautFieldOfView2d, 0.5f));
        }
        IEnumerator ChangeFOV(float start, float end, float duration)
        {
           float time = 0;
            while (time < duration)
            {
                float t = time / duration;
                Instance.Camera.orthographicSize = Mathf.Lerp(start, end, t);
                Instance.Camera2.orthographicSize = Mathf.Lerp(start, end, t);
                
                time += Time.deltaTime;
                yield return null;
            }
            // Ensure it ends at the exact 'end' value
            Instance.Camera.orthographicSize = end;
            Instance.Camera2.orthographicSize = end;
        
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