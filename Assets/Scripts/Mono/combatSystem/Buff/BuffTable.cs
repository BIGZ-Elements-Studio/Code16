using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff表")]
[System.Serializable]
public class BuffTable : ScriptableObject
{
    private static List<Sprite> instance;
    public static List<Sprite> icons
    {
        get
        {
            if (instance == null)
            {
                BuffTable[] BackPackData = Resources.FindObjectsOfTypeAll<BuffTable>();
                if (BackPackData.Length != 1)
                {
                    Debug.LogError("背包系统错误，实例数量为：" + BackPackData.Length);
                }
                return BackPackData[0].sprites;
            }
            return instance;
        }
    }

    public List<Sprite> sprites = new List<Sprite>();
}