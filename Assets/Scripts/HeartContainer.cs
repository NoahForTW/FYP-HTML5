using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{
    public HeartContainer next;
    [Range(0, 1)] private float fill;
    [SerializeField] private Image fillImage;
    private Color originalColor; // Store the original colour of the heart
    [SerializeField] private Color flashColor = Color.red; // Default flash colour

    private void Awake()
    {
        originalColor = fillImage.color; // Save the original colour
    }

    public void SetHeart(float count)
    {
        fill = Mathf.Clamp01(count); // Ensure fill is between 0 and 1
        fillImage.fillAmount = fill;
        count--;
        if (next != null)
        {
            next.SetHeart(count);
        }
    }

    public void Flash(Color? overrideFlashColor = null, float duration = 0.1f)
    {
        // Use the provided flash colour, or default to `flashColor`
        Color flash = overrideFlashColor ?? flashColor;
        StartCoroutine(FlashRoutine(flash, duration));
    }

    private IEnumerator FlashRoutine(Color flash, float duration)
    {
        fillImage.color = flash; // Change to flash colour
        yield return new WaitForSeconds(duration);
        fillImage.color = originalColor; // Revert to original colour
    }
}
