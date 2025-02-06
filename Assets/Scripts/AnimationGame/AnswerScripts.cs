using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScripts : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    Color startColor;
    public Image image;
    public Image childImage;
    private void OnEnable()
    {
        image = GetComponent<Image>();
        childImage = transform.GetChild(0).GetComponent<Image>();
        startColor = image.color;
    }

    public void Answer()
    {
        SetImageColor(isCorrect ? Color.green : Color.red);
        if (isCorrect)
        {
            quizManager.correct();
        }
        else
        {
            quizManager.wrong();
        }
    }

    public void SetImageColor(Color color)
    {
        image.color = color;
    }
    public void SetChildImageSprite(Sprite sprite)
    {
        childImage.sprite = sprite;
    }

    public void SetDefaultColor()
    {
        image.color = startColor;
    }
}
