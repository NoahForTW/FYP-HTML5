using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class VariableMinigame : Minigame
{
    public static VariableMinigame Instance;
    [SerializeField] public GameObject slotsPieceParent;
    [SerializeField] public GameObject variablePieceParent;
    [SerializeField] public GameObject variableSlotParent;
    [SerializeField] public TMP_Text questionTextUI;

    [SerializeField] List<Variable_SO> variableQuestions;
    [SerializeField] public GameObject variableSlotPrefab;
    [SerializeField] public GameObject variablePiecePrefab;

    List<QuestionCompleted> questionsCompleted;
    List<VariableSlot> variableSlots;
    Variable_SO currentQuestion;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        questionsCompleted = new List<QuestionCompleted>();
        variableQuestions = ShuffleList(variableQuestions);
        foreach (Variable_SO variableQuestion in variableQuestions)
        {
            QuestionCompleted question = new QuestionCompleted();
            question.question = variableQuestion;
            question.completed = false;
            questionsCompleted.Add(question);
        }
        variableSlots = new List<VariableSlot>();
        SetQuestion(questionsCompleted[0].question);
    }

    void Update()
    {
        if (!isAllQuestionCompleted())
        {
            foreach (QuestionCompleted question in questionsCompleted)
            {
                // when current question is not completed
                if (!question.completed && currentQuestion == question.question)
                {
                    if (isAllSlotCorrect())
                    {
                        question.completed = true;

                        // set to the next question
                        int currentIndex = questionsCompleted.IndexOf(question);
                        if (currentIndex + 1 != questionsCompleted.Count
                            && !questionsCompleted[currentIndex + 1].completed)
                        {
                            SetQuestion(questionsCompleted[currentIndex + 1].question);
                        }
                    }
                }
            }
        }
        else
        {
            // completed
            if(!isCompleted)
                isCompleted = true;
        }
        

    }

    void SetQuestion(Variable_SO question)
    {
        //clear 
        variableSlots.Clear();
        ClearChild(variableSlotParent.transform);
        ClearChild(variablePieceParent.transform);

        currentQuestion = question;

        // set question text
        questionTextUI.text = question.question;

        char[] answerArray = question.answer.ToCharArray();
        char[] shuffledChar = ShuffleArray(RemoveRepetition((char[])answerArray.Clone()));

        foreach (char c in answerArray)
        {
            //set slot
            GameObject slot = Instantiate(variableSlotPrefab, variableSlotParent.transform);
            slot.GetComponent<VariableSlot>().letter = c;
            variableSlots.Add(slot.GetComponent<VariableSlot>());
        }
        foreach (char c in shuffledChar)
        {
            GameObject piece = Instantiate(variablePiecePrefab, variablePieceParent.transform);
            piece.GetComponentInChildren<VariablePiece>().SetText(c.ToString());
        }
    }
    bool isAllQuestionCompleted()
    {
        foreach (QuestionCompleted question in questionsCompleted)
        {
            if (!question.completed)
            {
                return false;
            }
        }
        return true;
    }
    bool isAllSlotCorrect()
    {
        foreach (VariableSlot slot in variableSlots)
        {
            if(!slot.isCorrect)
            {
                return false;
            }
        }
        return true;
    }

    public void ClearChild(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
    public void ResetSlots()
    {
        foreach (VariableSlot slot in variableSlots)
        {
            foreach(Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
    char[] RemoveRepetition(char[] array)
    {
        HashSet<char> uniqueChars = new HashSet<char>();
        List<char> resultList = new List<char>();

        foreach (char c in array)
        {
            if (!uniqueChars.Contains(c)) // Add only if it's not already in the set
            {
                uniqueChars.Add(c);
                resultList.Add(c);
            }
        }

        return resultList.ToArray();
    }
    List<T> ShuffleList<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }

        return list;
    }
    char[] ShuffleArray(char[] texts)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < texts.Length; t++)
        {
            char tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
        return texts;
    }
}


public class QuestionCompleted
{
    public Variable_SO question;
    public bool completed;
}
