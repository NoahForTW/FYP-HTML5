using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class VariablePiece : MonoBehaviour
{
    TMP_Text text;
    VariableTypeOptions variable;
    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();

    }
    public void SetVariable(VariableTypeOptions variable)
    {
        this.variable = variable;
        text.text = variable.option;
    }
    public bool GetIsCorrentOption()
    {
        return variable.isCorrectOption;
    }

    public void OptionClicked()
    {
        VariableMinigame.Instance.QuestionAnswered.Invoke(GetIsCorrentOption());
    }
}
