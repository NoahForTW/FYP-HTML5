using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModelTransparency : MonoBehaviour
{
    [SerializeField] float alphaValue;

    MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    IEnumerator LerpAlpha(float duration, float targetAlpha)
    {
        Material[] materials = meshRenderer.materials;
        Color currentColor = materials[0].color;

        float startAlpha = currentColor.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {

            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            currentColor.a = newAlpha; // set alpha   
            materials[0].color = currentColor; // set to mesh renderer
            meshRenderer.materials = materials;
            yield return null;
        }

        currentColor.a = targetAlpha; // set alpha   
        materials[0].color = currentColor; // set to mesh renderer
        meshRenderer.materials = materials;
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LerpAlpha(0.5f, alphaValue));
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(LerpAlpha(0.5f, 1));
    }

}
