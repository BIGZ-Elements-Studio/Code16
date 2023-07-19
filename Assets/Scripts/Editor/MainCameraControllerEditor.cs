using UnityEngine;
using UnityEditor;
using oct.cameraControl;
[CustomEditor(typeof(MainCameraController))]
public class MainCameraControllerEditor : Editor
{
    private Color buttonColor = Color.white; // Color of the button

    public override void OnInspectorGUI()
    {
        MainCameraController cameraController = (MainCameraController)target;

        DrawDefaultInspector();

        EditorGUILayout.Space();


        EditorGUILayout.Space();

        // ����ģʽ�л���ť
        if (GUILayout.Button(cameraController.is2D ? "�л���3Dģʽ" : "�л���2Dģʽ"))
        {
            MainCameraController.ChangeMode(!cameraController.is2D);
        }
    }

}