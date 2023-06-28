using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeAtkObject : MonoBehaviour
{
    List<command> commands = new List<command>();
}

internal class command
{
    string content;
}