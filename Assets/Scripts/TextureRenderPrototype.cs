using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextureRenderPrototype : MonoBehaviour
{
    public Camera targetCamera; // The camera to switch rendering
    public RenderTexture renderTexture; // The Render Texture to switch to
    public GameObject rawImage;
    public GameObject choseAVGE;
    public GameObject choseGDT;

    void Start()
    {
        // Ensure the camera starts with default (rendering to screen)
        // targetCamera.targetTexture = null;
        // rawImage.SetActive(false);
        SetRenderTexture(null);
        choseAVGE.SetActive(false);
        choseGDT.SetActive(false);
    }

    void Update()
    {
        // Press 'R' to switch to Render Texture
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetRenderTexture(renderTexture);
            //rawImage.SetActive(true);
        }

        // Press 'D' to reset to default (rendering to screen)
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetRenderTexture(null); // Reset to default rendering

            //rawImage.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            choseAVGE.SetActive(true);
            choseGDT.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            choseAVGE.SetActive(false);
            choseGDT.SetActive(true);
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            
        }
    }

    void SetRenderTexture(RenderTexture texture)
    {
        targetCamera.targetTexture = texture;
        if (texture == null)
        {
            Debug.Log("Camera is now rendering to the screen (default framebuffer).");
        }
        else
        {
            Debug.Log("Camera is now rendering to the Render Texture.");
        }

        rawImage.SetActive(texture != null);

    }
}
