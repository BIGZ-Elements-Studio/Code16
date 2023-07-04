using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff��")]
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
                    Debug.LogError("����ϵͳ����ʵ������Ϊ��" + BackPackData.Length);
                }
                return BackPackData[0].sprites;
            }
            return instance;
        }
    }

    public List<Sprite> sprites = new List<Sprite>();
}