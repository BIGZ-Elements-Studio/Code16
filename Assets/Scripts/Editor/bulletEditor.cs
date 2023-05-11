using UnityEngine;
using UnityEditor;
using CombatSystem;

[CustomEditor(typeof(Bullet))]
public class GizmoWireBoxEditor : Editor
{
    private bool showWireBox = false; // Flag to indicate whether to show the wire box or not

    private void OnEnable()
    {
        showWireBox=true;
    }

    private void OnDisable()
    {
        showWireBox = false;
    }


    private void OnSceneGUI()
    {
        // Only draw the wire box in the scene view if the showWireBox flag is true
        if (showWireBox)
        {
            Bullet wireBox = (Bullet)target;
            Vector3 center = wireBox.transform.TransformPoint(Vector3.zero);
            Vector3 size = wireBox.size;
            Vector3 extents = size * 0.5f;

            Handles.color = Color.green;
            Handles.DrawWireCube(center, size);
        }
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw default inspector properties
        DrawDefaultInspector();

        // Reference to target script
        Bullet bullet = (Bullet)target;

        // Draw timeline slider
        DrawTimeline(bullet);

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawTimeline(Bullet bullet)
    {
        if (bullet.interval<0.1)
        {
            return;
        }
        // Calculate timeline start and end points based on bullet's delay, interval, and last properties
        float totalTime = bullet.delay + bullet.time + bullet.last;
        float timelineStart = Time.realtimeSinceStartup - bullet.delay;
        float timelineEnd = timelineStart + totalTime;

        // Calculate the height of the timeline marker
        float markerHeight = EditorGUIUtility.singleLineHeight * 0.5f;

        // Draw timeline background
        Rect timelineRect = GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth, markerHeight, GUILayout.ExpandWidth(true));
        EditorGUI.DrawRect(timelineRect, Color.grey);

        // Draw damage call markers on the timeline
        float damageStartTime = timelineStart + bullet.delay;
        float damageEndTime = timelineStart + bullet.delay + bullet.time;
        float damageInterval = bullet.interval;
        float damageTime = damageStartTime;
        while (damageTime <= damageEndTime)
        {
            float damageProgress = Mathf.Clamp01((damageTime - timelineStart) / totalTime);
            Rect markerRect = new Rect(timelineRect.x + timelineRect.width * damageProgress, timelineRect.y, 2f, timelineRect.height);
            EditorGUI.DrawRect(markerRect, Color.red);
            damageTime += damageInterval;
        }
    }
}
