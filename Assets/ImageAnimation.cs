using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{
    public RawImage imageToScale; // Das Bild, das vergr��ert werden soll
    public Image imageToFade;  // Das Bild, dessen Opazit�t gesenkt werden soll
    public float duration = 15f; // Dauer der Animation in Sekunden

    void Start()
    {
        // Starte die Coroutine, um beide Animationen gleichzeitig auszuf�hren
        StartCoroutine(AnimateImages());
    }

    private IEnumerator AnimateImages()
    {
        // Speichere die Startwerte
        Vector3 initialScale = imageToScale.rectTransform.localScale;
        Color initialColor = imageToFade.color;

        // Endwerte
        Vector3 targetScale = initialScale * 1.1f; // Beispiel: Vergr��ern um 50%
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0); // Ziel-Opazit�t 0

        float elapsedTime = 0f;

        // Animation �ber die Dauer abspielen
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Lerp-Funktion, um den Zwischenwert f�r Skalierung und Opazit�t zu berechnen
            float t = elapsedTime / duration;

            imageToScale.rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            imageToFade.color = Color.Lerp(initialColor, targetColor, t);

            yield return null; // Warte einen Frame und f�hre die Schleife fort
        }

        // Stelle sicher, dass am Ende der Animation die Zielwerte gesetzt sind
        imageToScale.rectTransform.localScale = targetScale;
        imageToFade.color = targetColor;
    }
}
