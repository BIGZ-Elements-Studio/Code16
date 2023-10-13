using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DialogueData", menuName = "DialogeSystem")]
public class dialogueData:ScriptableObject 
{
    public List<individualdialogue> data = new List<individualdialogue>();
}

[System.Serializable]
public struct individualdialogue
{
    public string content;
    public int character;
}