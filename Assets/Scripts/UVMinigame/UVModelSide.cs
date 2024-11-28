using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVModelSide : MonoBehaviour
{
    [SerializeField] Texture texture;
    bool canChangeTexture = true;
    public bool IsCurrentTextureCorrect()
    {
        return GetComponent<MeshRenderer>().material.mainTexture == texture;
    }

    public bool GetCanChangeTexture()
    {
        return canChangeTexture;
    }
    public void SetCanChangeTexture(bool newBool)
    {
        canChangeTexture = newBool;
    }
}
