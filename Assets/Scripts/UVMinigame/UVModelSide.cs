using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UVModelSide : MonoBehaviour
{
    [SerializeField] public Texture texture;

    public bool IsCurrentTextureCorrect()
    {
        List<Material> materials = GetComponent<MeshRenderer>().materials.ToList();
        foreach (Material mat in materials)
        {
            if (mat.HasProperty("_MainTex") && mat.mainTexture == texture)
                return true;
        }
        return false;
    }
    public void PromptFeedback()
    {
        UVTextureMinigame.Instance.TextureIsPlaced(IsCurrentTextureCorrect());
    }
    public void CompletedVFX(ParticleSystem particleSystem, float duration)
    {
        // play particle for a fe seconds ??
        ParticleSystem particle = Instantiate(particleSystem, transform);

        Destroy(particle, duration);
    }


}
