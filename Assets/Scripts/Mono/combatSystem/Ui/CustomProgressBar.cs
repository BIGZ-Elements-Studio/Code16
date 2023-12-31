using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CustomProgressBar : MonoBehaviour
{
    
    [SerializeField] Image fillImg;
    [SerializeField] Image bufferFillImg;
    public Gradient color;
    public float smoothTime;
    public float defaultValue;
    [SerializeField]
    private float targetValue;
    [SerializeField]
    private float currentValue;
    [SerializeField]
    private float bufferValue;
    public float value { set { SetValue(value); } }
    private void Start()
    {
        currentValue = defaultValue;
        targetValue = defaultValue;
        bufferValue = defaultValue;

        fillImg.fillAmount = currentValue;
        bufferFillImg.fillAmount = bufferValue;
    }

    private void Update()
    {
        bufferValue = Mathf.Lerp(bufferValue, targetValue, (Time.deltaTime / smoothTime)+0.01f);

        fillImg.fillAmount = targetValue; fillImg.color= color.Evaluate(targetValue);
        bufferFillImg.fillAmount = bufferValue;
    }

    public virtual void SetValue(float newValue)
    {
        if (newValue> targetValue)
        {
            bufferValue = newValue;
            
        }
        
        targetValue = Mathf.Clamp01(newValue);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CustomProgressBar))]
public class CustomProgressBarEditor : Editor
{
    private float targetValue;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CustomProgressBar progressBar = (CustomProgressBar)target;

        GUILayout.Space(10);

        GUILayout.Label("Set Target Value:", EditorStyles.boldLabel);
        targetValue = EditorGUILayout.Slider("Value", targetValue, 0f, 1f);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Set Value"))
        {
            progressBar.SetValue(targetValue);
            Repaint();
        }

        GUILayout.EndHorizontal();
    }
}
#endif