using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    public Image blackScreen;  // Reference to the Image component
    public float fadeDuration = 1f;  // Duration of the fade

    // Call this to start fading the image to full opacity
    public void StartFadeIn()
    {
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        Color color = blackScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);  // Gradually increase alpha from 0 to 1
            blackScreen.color = color;
            yield return null;  // Wait for the next frame
        }

        // Ensure the alpha is exactly 1 after the fade is complete
        color.a = 1f;
        blackScreen.color = color;
    }
}
