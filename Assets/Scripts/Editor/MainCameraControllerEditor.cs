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

        // 绘制模式切换按钮
        if (GUILayout.Button(cameraController.is2D ? "切换到3D模式" : "切换到2D模式"))
        {
            MainCameraController.ChangeMode(!cameraController.is2D);
        }
    }

}