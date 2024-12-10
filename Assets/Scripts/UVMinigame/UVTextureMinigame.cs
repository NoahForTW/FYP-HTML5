using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UVTextureMinigame : MonoBehaviour
{
    public static UVTextureMinigame Instance;
    public GameObject UVTexturePrefab;
    public GameObject UVTexturePalette;

    public float rotationSpeed;
    public bool canModelMove = false;
    public bool canModelRotate = false;
    public bool canCheckTexture = true;

    UVModelSide[] ModelSides;
    List<UVTextureUI> UVTextures;

    [SerializeField] UVGame_SO currentModelParameters;
    [SerializeField] GameObject modelParent;
    [SerializeField] UVModelTools uVModelTools;


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
#if UNITY_WEBGL && !UNITY_EDITOR
        //resizeCanvas();
#endif
    }

    private void Start()
    {
        // instantiate model
        GameObject model = Instantiate(currentModelParameters.UVModelPrefab, modelParent.transform);
        ModelSides = model.GetComponentsInChildren<UVModelSide>();

        //instantiate textures
        UVTextures = new List<UVTextureUI>();
        foreach(Texture texture in currentModelParameters.UVTextures)
        {
            GameObject textureUI = Instantiate(UVTexturePrefab, UVTexturePalette.transform);
            UVTextureUI uVTextures = textureUI.GetComponentInChildren<UVTextureUI>();
            uVTextures.texture = texture;
            UVTextures.Add(uVTextures);
        }
    }
    void Update()
    {

        foreach (var side in ModelSides)
        {
            // when texture placed is correct
            if (side.IsCurrentTextureCorrect() && canCheckTexture)
            {
                side.SetCanChangeTexture(false);
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

    }

    public void RequestFullScreen()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        requestFullscreen();
        resizeCanvas();
#endif
    }
}
