using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BooleanGame : Minigame
{
    [Tooltip("Assign a Game Parents")]
    public GameObject GearGameParent;
    public GameObject GearSlotParent;
    public GameObject GearPiecesParent;

    [Header("Questions")]
    public List<BooleanGame_SO> GameQuestions;

    [Header("Textboxes")]
    public TMP_Text boolValidation;
    public TMP_Text QuestionText;

    [Header("Lever")]
    public GameObject Lever;

    public static BooleanGame Instance;

    // List to keep track of all gear pieces and slots
    private List<GearPiece> gearPieces = new List<GearPiece>();
    private List<GearSlot> gearSlots = new List<GearSlot>();
    List<BooleanQuestionCompleted> questionsCompleted;
    BooleanQuestionCompleted CurrentQuestion;

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
        questionsCompleted = new List<BooleanQuestionCompleted>();
    }

    private void Start()
    {
        //StartMinigame();
    }
    void InitializeGame(BooleanQuestionCompleted booleanQuestion)
    {
        BooleanGame_SO question = booleanQuestion.Question_SO;
        currentClue = question.Clue;
        CurrentQuestion = booleanQuestion;
        // get questions from NPC
        // set question 
        gearPieces.Clear();
        gearSlots.Clear();
        boolValidation.text = "";
        QuestionText.text = question.Question;

        // Find and store all GearSlot components under the GearSlotParent
        GearSlot[] slots = GearSlotParent.GetComponentsInChildren<GearSlot>();
        gearSlots = slots.ToList();

        // Find and store all GearPiece components under the GearGameParent
        GearPiece[] pieces = GearPiecesParent.GetComponentsInChildren<GearPiece>();
        gearPieces = pieces.ToList();

        if (slots.Length != 0)
        {
            int randomIndex = Random.Range(0, slots.Length - 1);
            foreach (GearSlot slot in slots)
            {
                GearPiece piece = slot.GetComponentInChildren<GearPiece>();
                slot.enabled = slot == slots[randomIndex];
                piece.enabled = slot == slots[randomIndex];
                piece.GetComponentInChildren<TextMeshProUGUI>().text = "";

                if (slot != slots[randomIndex])
                    continue;
                
                foreach (Transform pieceParent in GearPiecesParent.transform)
                {
                    GearPiece pieceInParent = pieceParent.GetComponentInChildren<GearPiece>();
                    if (pieceInParent == null)
                    {
                        piece.transform.SetParent(pieceParent);
                        piece.parentAfterDrag = pieceParent;
                        piece.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                        piece.GetComponentInChildren<TextMeshProUGUI>().text = question.Answer.ToString();
                        slot.requiredGear = piece.gameObject;
                        //gearSlots.Add(slot);
                        gearPieces.Add(piece);
                    }
                    else {
                        pieceInParent.GetComponentInChildren<TextMeshProUGUI>().text = (!question.Answer).ToString();;
                        if (randomIndex % 2 == 0)
                        {
                            pieceParent.transform.SetAsFirstSibling();
                        }
          
                    }
                }
            }
        }



    }
    public void DisplayValidation(string message, float delay)
    {
        StopAllCoroutines(); // Stop any ongoing coroutine to avoid overlapping
        boolValidation.text = message;
        boolValidation.gameObject.SetActive(true); // Ensure the text is visible
        StartCoroutine(HideTextAfterDelay(delay));
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        boolValidation.gameObject.SetActive(false); // Hide the text after the delay
    }
    public override void StartMinigame()
    {
        base.StartMinigame();
        GameQuestions = MinigameManager.Instance.GetQuestions().OfType<BooleanGame_SO>().ToList();
        GameQuestions = ShuffleList(GameQuestions).Take(3).ToList();
        foreach (BooleanGame_SO booleanQuestion in GameQuestions)
        {
            BooleanQuestionCompleted question = new BooleanQuestionCompleted();
            question.Question_SO = booleanQuestion;
            question.completed = false;
            questionsCompleted.Add(question);
        }
        InitializeGame(questionsCompleted[0]);
    }

    public void ResetButton()
    {
        foreach (var gearPiece in gearPieces)
        {
            gearPiece.ResetPosition();
        }
    }
    
    private bool AllSlotsAreCorrect()
    {
        foreach (GearSlot slot in gearSlots)
        {
            if (!slot.enabled)
                continue;

            if (!slot.IsGearCorrect())
            {
                return false;
            }
        }
        return true;
    }

    bool isAllQuestionCompleted()
    {
        foreach (BooleanQuestionCompleted question in questionsCompleted)
        {
            if (!question.completed)
            {
                return false;
            }
        }
        return true;
    }

    public void QuestionIsAnswered()
    {
        Lever.GetComponent<Animator>()?.SetBool("Start", true);
        AudioManager.instance.PlaySoundOneShot(SoundType.Lever);
        StopCoroutine(ShowFeedBack(AllSlotsAreCorrect()));
        StartCoroutine(ShowFeedBack(AllSlotsAreCorrect()));
    }
    IEnumerator ShowFeedBack(bool isCorrect)
    {
        Debug.Log("checking");
        yield return new WaitForSeconds(1f);
        if (isCorrect)
            CurrentQuestion.completed = true;
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
    private void Update()
    {
        if (!isAllQuestionCompleted())
        {
            foreach (BooleanQuestionCompleted question in questionsCompleted)
            {
                // when current question is not completed
                if (CurrentQuestion == question && question.completed)
                {
                    // set to the next question
                    int currentIndex = questionsCompleted.IndexOf(question);
                    if (currentIndex + 1 != questionsCompleted.Count
                        && !questionsCompleted[currentIndex + 1].completed)
                    {
                        InitializeGame(questionsCompleted[currentIndex + 1]);
                    }


                }
            }
        }
        else
        {
            // completed
            if (!isCompleted)
                isCompleted = true;
        }
    }
}

public class BooleanQuestionCompleted
{
    public BooleanGame_SO Question_SO;
    public bool completed;
}