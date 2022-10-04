using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// has information about the dialogue

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3,10)] // min, max

    public string[] sentences; 
}
