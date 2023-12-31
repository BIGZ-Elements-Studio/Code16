using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEffectController : MonoBehaviour
{
    [SerializeField] Light _SceneMainLight;
    public static SceneEffectController Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }
    public static Light SceneMainLight
    {
        get { return Instance._SceneMainLight; }
    }
    
}
