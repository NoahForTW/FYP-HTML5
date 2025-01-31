using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Variable_SO : ScriptableObject
{
    [TextArea]
    public string question;

    public List<VariableTypeOptions> options;

}

[Serializable]
public class VariableTypeOptions
{
    public string option;
    public bool isCorrectOption;
}

