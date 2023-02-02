using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/New Dialogue")]
public class DialogueData : ScriptableObject
{
    [TextArea(4, 10)]
    public string[] sentences;

}
