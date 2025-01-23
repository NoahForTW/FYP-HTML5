using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UVTextureMinigame : Minigame
{
    public static UVTextureMinigame Instance;
    public GameObject UVTexturePrefab;
    public GameObject UVTexturePalette;
    public GameObject UVTextureGameObject;
    public GameObject SampleModelParent;

    public float rotationSpeed;
    public bool canModelMove = false;
    public bool canModelRotate = false;
    public bool canCheckTexture = true;

    List<UVModelSide> ModelSides;
    List<UVTextureUI> UVTextures;
    UVQuestionCompleted currentModelParameters;

    [SerializeField] List<UVGame_SO> modelParameters;
    [SerializeField] GameObject modelParent;
    [SerializeField] UVModelTools uVModelTools;

    [SerializeField] GameObject FeedbackGO;
    [SerializeField] TMP_Text FeedbackText;

    List<UVQuestionCompleted> questionsCompleted;
    public UnityEvent<float> UVToolsZoomEvent;


    [DllImport("__Internal")]
    private static extern void requestFullscreen();

    [DllImport("__Internal")]
    private static extern void resizeCanvas();

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
        /*#if UNITY_WEBGL && !UNITY_EDITOR
                //resizeCanvas();
        #endif*/

        questionsCompleted = new List<UVQuestionCompleted>();
        modelParameters = new List<UVGame_SO>();
    }

    public override void StartMinigame()
    {
        base.StartMinigame();

        modelParameters = MinigameManager.Instance.GetQuestions().OfType<UVGame_SO>().ToList();
        modelParameters = ShuffleList(modelParameters).Take(3).ToList();
        foreach (UVGame_SO UVQuestion in modelParameters)
        {
            UVQuestionCompleted question = new UVQuestionCompleted();
            question.Question_SO = UVQuestion;
            question.completed = false;
            questionsCompleted.Add(question);
        }
        //variableSlots = new List<VariableSlot>();
        SetQuestion(questionsCompleted[0]);
    }
    private void Start()
    {
        

        
    }
    void Update()
    {

        if (!isAllQuestionCompleted())
        {
            foreach (UVQuestionCompleted question in questionsCompleted)
            {
                // when current question is completed
                if (currentModelParameters == question && question.completed)
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
            if (!isCompleted)
                isCompleted = true;
        }

        foreach (var side in ModelSides)
        {
            // when texture placed is correct
            if (side.IsCurrentTextureCorrect() && canCheckTexture)
            {
                Debug.Log(side.gameObject.name + "-> DONE");
            }
        }
        canModelMove = uVModelTools.selectedTool == UVTools.Move;
        canModelRotate = uVModelTools.selectedTool == UVTools.Rotate;

        foreach (var uVtexture in UVTextures)
        {
            if (uVtexture.canDrag)
            {
                canModelRotate = false;
                canModelMove = false;
            }
        }

        if (AllSidesAreCorrect())
        {
            ModelFinished();
        }

    }

    void SetQuestion(UVQuestionCompleted uVQuestion)
    {
        MinigameManager.Instance.ClearChild(SampleModelParent.transform);
        MinigameManager.Instance.ClearChild(modelParent.transform);
        MinigameManager.Instance.ClearChild(UVTexturePalette.transform);
        uVModelTools.SelectTool(UVTools.None);

        currentModelParameters = uVQuestion;

        UVGame_SO game_SO = uVQuestion.Question_SO;
        // instantiate sample model
        GameObject sampleModel = Instantiate(game_SO.modelSample, SampleModelParent.transform);
/*        UVModelSide[] sampleModelSides = sampleModel.GetComponentsInChildren<UVModelSide>();

        foreach (UVModelSide side in sampleModelSides)
        {
            side.GetComponent<Renderer>().material.mainTexture = side.texture;
        }*/

        // instantiate model
        GameObject model = Instantiate(game_SO.UVModelPrefab, modelParent.transform);
        ModelSides = new List<UVModelSide>();
        ModelSides = model.GetComponentsInChildren<UVModelSide>().ToList();

        //instantiate textures
        UVTextures = new List<UVTextureUI>();
        foreach (Texture texture in game_SO.UVTextures)
        {
            GameObject textureUI = Instantiate(UVTexturePrefab, UVTexturePalette.transform);
            UVTextureUI uVTextures = textureUI.GetComponentInChildren<UVTextureUI>();
            uVTextures.texture = texture;
            UVTextures.Add(uVTextures);
        }
    }

    public bool AllSidesAreCorrect()
    {
        foreach (var side in ModelSides)
        {
             if (!side.IsCurrentTextureCorrect())
            {
                return false;
            }
        }
        return true;
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
    bool isAllQuestionCompleted()
    {
        foreach (UVQuestionCompleted question in questionsCompleted)
        {
            if (!question.completed)
            {
                return false;
            }
        }
        return true;
    }

    public void TextureIsPlaced(bool isCorrect)
    {
        StopCoroutine(ShowFeedBack(isCorrect));
        StartCoroutine(ShowFeedBack(isCorrect));
    }
    public void ModelFinished()
    {
        StopCoroutine(ShowFeedBack());
        StartCoroutine(ShowFeedBack());
    }
    IEnumerator ShowFeedBack(bool isCorrect)
    {
        // show feedback
        FeedbackGO.SetActive(true);
        FeedbackText.text = isCorrect ? "Correct!" : "Try Again";
        FeedbackText.color = isCorrect ? Color.green : Color.red;
        yield return new WaitForSeconds(0.5f);
        HideFeedback();
    }
    IEnumerator ShowFeedBack()
    {
        // show feedback
        FeedbackGO.SetActive(true);
        FeedbackText.text = "Done!";
        FeedbackText.color = Color.white;
        yield return new WaitForSeconds(1f);
        currentModelParameters.completed = true;
        HideFeedback();
    }
    void HideFeedback()
    {
        // hide feedback
        FeedbackGO.SetActive(false);
    }
    public void RequestFullScreen()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        requestFullscreen();
        resizeCanvas();
#endif
    }
}

public class UVQuestionCompleted
{
    public UVGame_SO Question_SO;
    public bool completed;
}


