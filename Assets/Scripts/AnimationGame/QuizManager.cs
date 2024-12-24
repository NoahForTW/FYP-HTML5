using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> _qNa;
    public GameObject[] options;
    public int _currentQuestion;

    public TextMeshProUGUI _questionTxt;
    public TextMeshProUGUI _scoreTxt;

    int _totalQuestions = 4;
    public int _score;

    public GameObject _quizPanel;
    public GameObject _gOPanel;
    // Start is called before the first frame update
    private void Start()
    {
        generateQuestion();
    }

    void GameOver()
    {
        _quizPanel.SetActive(false);
        _gOPanel.SetActive(true);
        _scoreTxt.text = _score + "/" + _totalQuestions;
    }

    public void retry()
    {
        //Retry code here
    }
    public void correct()
    {
        _score += 1;
        _qNa.RemoveAt(_currentQuestion);
        generateQuestion();
    }
    public void wrong()
    {
        _qNa.RemoveAt(_currentQuestion);
        StartCoroutine(waitForNext());
    }

    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(1);
        generateQuestion();
    }
    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScripts>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _qNa[_currentQuestion].Answers[i];

            if (_qNa[_currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScripts>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (_qNa.Count > 0)
        {
            _currentQuestion = Random.Range(0, _qNa.Count);

            _questionTxt.text = _qNa[_currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            Debug.Log("Out Of Questions");
            GameOver();
        }
    }
}
