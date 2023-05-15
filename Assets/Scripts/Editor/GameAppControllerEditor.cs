
using UnityEngine;
using UnityEditor;
namespace NBGame.editor
{
    [CustomEditor(typeof(GameAppController))]
    public class GameAppControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            GameAppController script = (GameAppController)target;
            base.OnInspectorGUI();
             if (GUILayout.Button("set Frame Rate 30"))
             {
                script.ChangeFrameRate(GameAppController.frameRate.Thirty);
              }
            if (GUILayout.Button("set Frame Rate 60"))
            {
                script.ChangeFrameRate(GameAppController.frameRate.Sixty);
            }
            if (GUILayout.Button("set Frame Rate 120"))
            {
                script.ChangeFrameRate(GameAppController.frameRate.OneTwenty);
            }
            if (GUILayout.Button("set window 1"))
            {
                script.ChangeScreenSize(GameAppController.Level.one);
            }
            if (GUILayout.Button("set window 2"))
            {
                script.ChangeScreenSize(GameAppController.Level.two);
            }
            if (GUILayout.Button("set window 3"))
            {
                script.ChangeScreenSize(GameAppController.Level.three);
            }
            if (GUILayout.Button("set window 4"))
            {
                script.ChangeScreenSize(GameAppController.Level.full);
            }
            if (GUILayout.Button("set window "))
            {
                script.SetResizeavkeWindow();
            }
            if (GUILayout.Button("Pause"))
            {
                script.pause();
            }
            if (GUILayout.Button("resume"))
            {
                script.resume();
            }
        }
    }
}