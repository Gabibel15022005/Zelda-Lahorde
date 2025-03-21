using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScDialogue
{
    public string[] Name;
    public Sprite[] Face;

    [TextArea(3,10)]
    public string[] Sentence;
}
