using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff±í")]
[System.Serializable]
public class BuffTable : ScriptableObject
{
    public static List<Sprite> icons;
    public List<Sprite> sprites = new List<Sprite>();
}