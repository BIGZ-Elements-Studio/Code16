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

        // 检查当前相机位置是否匹配偏移值
        Vector3 currentOffset = cameraController.transform.position - cameraController.target.position;
        if (currentOffset != cameraController.offset)
        {
            buttonColor = Color.red; // 如果不匹配，将按钮颜色设为红色
        }
        else
        {
            buttonColor = Color.white; // 否则，将按钮颜色设为白色
        }

        GUI.backgroundColor = buttonColor; // 设置按钮颜色
        if (GUILayout.Button("设置偏移值"))
        {
            cameraController.SetOffset();
        }
        GUI.backgroundColor = Color.white; // 重置按钮颜色

        EditorGUILayout.Space();

        // 绘制模式切换按钮
        if (GUILayout.Button(cameraController.is2D ? "切换到3D模式" : "切换到2D模式"))
        {
            MainCameraController.ChangeMode(!cameraController.is2D);
        }
    }

}