using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScripts : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Correct!");
            quizManager.correct();
        }
        else
        {
            Debug.Log("Wrong!");
            quizManager.correct();
        }
    }
}
