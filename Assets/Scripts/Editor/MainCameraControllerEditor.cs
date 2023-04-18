using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MainCameraController))]
public class MainCameraControllerEditor : Editor
{
    private Color buttonColor = Color.white; // Color of the button

    public override void OnInspectorGUI()
    {
        MainCameraController cameraController = (MainCameraController)target;

        DrawDefaultInspector();

        EditorGUILayout.Space();

        // ��鵱ǰ���λ���Ƿ�ƥ��ƫ��ֵ
        Vector3 currentOffset = cameraController.transform.position - cameraController.target.position;
        if (currentOffset != cameraController.offset)
        {
            buttonColor = Color.red; // �����ƥ�䣬����ť��ɫ��Ϊ��ɫ
        }
        else
        {
            buttonColor = Color.white; // ���򣬽���ť��ɫ��Ϊ��ɫ
        }

        GUI.backgroundColor = buttonColor; // ���ð�ť��ɫ
        if (GUILayout.Button("����ƫ��ֵ"))
        {
            cameraController.SetOffset();
        }
        GUI.backgroundColor = Color.white; // ���ð�ť��ɫ

        EditorGUILayout.Space();

        // ����ģʽ�л���ť
        if (GUILayout.Button(cameraController.is2D ? "�л���3Dģʽ" : "�л���2Dģʽ"))
        {
            MainCameraController.ChangeMode(!cameraController.is2D);
        }
    }

}