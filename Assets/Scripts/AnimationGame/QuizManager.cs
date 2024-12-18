using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> _qNa;
    public GameObject[] options;
    public int currentQuestion;

    public TextMeshProUGUI QuestionTxt;
    // Start is called before the first frame update
    private void Start()
    {
        generateQuestion();
    }

    public void correct()
    {
        _qNa.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScripts>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _qNa[currentQuestion].Answers[i];

            if (_qNa[currentQuestion].CorrectAnswer == i)
            {
                options[i].GetComponent<AnswerScripts>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (_qNa.Count > 0)
        {
            currentQuestion = Random.Range(0, _qNa.Count);

            QuestionTxt.text = _qNa[currentQuestion].Question;
            SetAnswers();

            _qNa.RemoveAt(currentQuestion);
        }
        else
        {
            Debug.Log("Out Of Questions");
        }
    }
}
