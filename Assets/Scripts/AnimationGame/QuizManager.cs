using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : Minigame
{
    public List<QuestionAndAnswers> _qNa;
    public Image questionImage;
    public GameObject[] options;
    public int _currentQuestion;

    public TextMeshProUGUI _questionTxt;
    public TextMeshProUGUI _scoreTxt;

    int _totalQuestions = 0;
    public int _score;

    public GameObject _quizPanel;
    public GameObject _gOPanel;
    // Start is called before the first frame update
    private void Start()
    {
        _totalQuestions = _qNa.Count;
        _gOPanel.SetActive(false);
        _questionTxt.text = "Choose the correct frame";
        generateQuestion();
    }

    void GameOver()
    {
        _quizPanel.SetActive(false);
        _gOPanel.SetActive(true);
        _scoreTxt.text = _score + "/" + _totalQuestions;
        StartCoroutine(EndMinigame());
    }

    public void retry()
    {
        //Retry code here
    }
    public void correct()
    {
        _score += 1;
        _qNa.RemoveAt(_currentQuestion);
        AudioManager.instance.PlaySoundOneShot(SoundType.Correct);
        generateQuestion();
    }
    public void wrong()
    {
        _qNa.RemoveAt(_currentQuestion);
        AudioManager.instance.PlaySoundOneShot(SoundType.Wrong);
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
            questionImage.sprite = _qNa[_currentQuestion].QuestionSprite;
            AnswerScripts currentOption = options[i].GetComponent<AnswerScripts>();
            currentOption.SetDefaultColor();
            currentOption.SetChildImageSprite(_qNa[_currentQuestion].option[i].AnswerSprite);
            currentOption.isCorrect = _qNa[_currentQuestion].option[i].CorrectAnswer;
            
        }
    }

    void generateQuestion()
    {
        if (_qNa.Count > 0)
        {
            _currentQuestion = Random.Range(0, _qNa.Count);

            SetAnswers();
        }
        else
        {
            Debug.Log("Out Of Questions");
            GameOver();
        }
    }

    IEnumerator EndMinigame()
    {
        yield return new WaitForSeconds(1);
        isCompleted = true;
    }
}
