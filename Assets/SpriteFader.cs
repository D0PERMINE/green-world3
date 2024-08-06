using System.Collections;
using UnityEngine;

public class SpriteFader : MonoBehaviour
{
    public SpriteRenderer[] sprites;  // Array von SpriteRenderern, die du verblassen lassen möchtest
    public float fadeDuration = 1f;   // Dauer, um von 0% auf 100% zu gehen
    public float delayBetweenFades = 0.5f; // Verzögerung zwischen dem Start der Fades

    void Start()
    {
        StartCoroutine(FadeInSprites());
    }

    IEnumerator FadeInSprites()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            yield return StartCoroutine(FadeIn(sprite));
            yield return new WaitForSeconds(delayBetweenFades);
        }
    }

    IEnumerator FadeIn(SpriteRenderer sprite)
    {
        Color color = sprite.color;
        float startAlpha = 0f;
        float endAlpha = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            sprite.color = color;
            yield return null;
        }

        // Am Ende sicherstellen, dass die Opazität auf 100% gesetzt ist
        color.a = endAlpha;
        sprite.color = color;
    }
}
