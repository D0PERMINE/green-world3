using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToTransparentEffect : MonoBehaviour
{
    public Image blackScreen;  // Reference to the Image component
    public float fadeDuration = 1f;  // Duration of the fade

    // Call this to start fading the image to full transparency
    public void StartFadeOut()
    {
        StartCoroutine(FadeToTransparent());
    }

    private IEnumerator FadeToTransparent()
    {
        float elapsedTime = 0f;
        Color color = blackScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));  // Gradually decrease alpha from 1 to 0
            blackScreen.color = color;
            yield return null;  // Wait for the next frame
        }

        // Ensure the alpha is exactly 0 after the fade is complete
        color.a = 0f;
        blackScreen.color = color;
    }
}
