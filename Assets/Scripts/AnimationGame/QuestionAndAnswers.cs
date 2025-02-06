
using Ink.Parsed;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class QuestionAndAnswers: ScriptableObject
{
    public Sprite QuestionSprite;
    public List<OptionAndAnswer> option;
}

[Serializable]
public class OptionAndAnswer
{
    public Sprite AnswerSprite;
    public bool CorrectAnswer;
}
