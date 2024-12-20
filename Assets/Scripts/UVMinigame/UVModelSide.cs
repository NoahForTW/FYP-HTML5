using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVModelSide : MonoBehaviour
{
    [SerializeField] public Texture texture;
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

    public void CompletedVFX(ParticleSystem particleSystem, float duration)
    {
        // play particle for a fe seconds ??
        ParticleSystem particle = Instantiate(particleSystem, transform);

        Destroy(particle, duration);
    }


}
