using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.Events;


public class VariableMinigame : Minigame
{
    public static VariableMinigame Instance;
    // instances
    [SerializeField] GameObject variablePieceParent;
    [SerializeField] TMP_Text QuestionTextUI;
    [SerializeField] GameObject FeedbackGO;
    [SerializeField] TMP_Text FeedbackText;

    [Header("Questions")]
    [SerializeField] List<Variable_SO> variableQuestions;

    // prefabs
    //[SerializeField] public GameObject variableSlotPrefab;
    [SerializeField] public GameObject variablePiecePrefab;

    List<QuestionCompleted> questionsCompleted;
    //List<VariableSlot> variableSlots;
    QuestionCompleted CurrentQuestion;
    public UnityEvent<bool> QuestionAnswered;
    protected void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        questionsCompleted = new List<QuestionCompleted>();
        QuestionAnswered.AddListener(QuestionIsAnswered);

       
    }

        
    void Update()
    {
        if (!isAllQuestionCompleted())
        {
            foreach (QuestionCompleted question in questionsCompleted)
            {
                // when current question is not completed
                if (CurrentQuestion == question && question.completed)
                {
                    // set to the next question
                    int currentIndex = questionsCompleted.IndexOf(question);
                    if (currentIndex + 1 != questionsCompleted.Count
                        && !questionsCompleted[currentIndex + 1].completed)
                    {
                        SetQuestion(questionsCompleted[currentIndex + 1]);
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

    public override void StartMinigame()
    {
        base.StartMinigame();
        variableQuestions = MinigameManager.Instance.GetQuestions().OfType<Variable_SO>().ToList();
        variableQuestions = ShuffleList(variableQuestions);
        foreach (Variable_SO variableQuestion in variableQuestions)
        {
            QuestionCompleted question = new QuestionCompleted();
            question.Question_SO = variableQuestion;
            question.completed = false;
            questionsCompleted.Add(question);
        }
        //variableSlots = new List<VariableSlot>();
        SetQuestion(questionsCompleted[0]);
    }

    void SetQuestion(QuestionCompleted questionCompleted)
    {
        //clear 
        //variableSlots.Clear();
        //ClearChild(variableSlotParent.transform);
        MinigameManager.Instance.ClearChild(variablePieceParent.transform);
        CurrentQuestion = questionCompleted;
        Variable_SO question_SO = questionCompleted.Question_SO;
        // set question text
        QuestionTextUI.text = question_SO.question;

        //char[] answerArray = question.answer.ToCharArray();
        //char[] shuffledChar = ShuffleArray(RemoveRepetition((char[])answerArray.Clone()));

        /*        foreach (char c in answerArray)
                {
                    //set slot
                    GameObject slot = Instantiate(variableSlotPrefab, variableSlotParent.transform);
                    slot.GetComponent<VariableSlot>().letter = c;
                    variableSlots.Add(slot.GetComponent<VariableSlot>());
                }*/
        /*        foreach (char c in shuffledChar)
                {
                    GameObject piece = Instantiate(variablePiecePrefab, variablePieceParent.transform);
                    piece.GetComponentInChildren<VariablePiece>().SetText(c.ToString());
                }*/

        foreach (VariableTypeOptions option in question_SO.options)
        {
            GameObject piece = Instantiate(variablePiecePrefab, variablePieceParent.transform);
            piece.GetComponentInChildren<VariablePiece>().SetVariable(option);

        }

    }

    void QuestionIsAnswered(bool isCorrect)
    {
        StopCoroutine(ShowFeedBack(isCorrect));
        StartCoroutine(ShowFeedBack(isCorrect));
    }
    IEnumerator ShowFeedBack(bool isCorrect)
    {
        // show feedback
        FeedbackGO.SetActive(true);
        FeedbackText.text = isCorrect ? "Correct!" : "Try Again";
        FeedbackText.color = isCorrect ? Color.green : Color.red;
        yield return new WaitForSeconds(1f);
        if (isCorrect)
            CurrentQuestion.completed = true;
        HideFeedback();
    }
    void HideFeedback()
    {
        // hide feedback
        FeedbackGO.SetActive(false);
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
/*    bool isAllSlotCorrect()
    {
        foreach (VariableSlot slot in variableSlots)
        {
            if(!slot.isCorrect)
            {
                return false;
            }
        }
        return true;
    }*/


/*    public void ResetSlots()
    {
        foreach (VariableSlot slot in variableSlots)
        {
            foreach(Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }*/
/*    char[] RemoveRepetition(char[] array)
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
    }*/
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
/*    char[] ShuffleArray(char[] texts)
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
    }*/
}


public class QuestionCompleted
{
    public Variable_SO Question_SO;
    public bool completed;
}
