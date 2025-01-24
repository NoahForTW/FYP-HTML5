using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BooleanGame_SO : ScriptableObject
{
    [TextArea]
    public string Question;

    public bool Answer;

    [TextArea]
    public string Clue;
}
