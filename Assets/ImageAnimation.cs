using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{
    public RawImage imageToScale; // Das Bild, das vergrößert werden soll
    public Image imageToFade;  // Das Bild, dessen Opazität gesenkt werden soll
    public float duration = 15f; // Dauer der Animation in Sekunden

    void Start()
    {
        // Starte die Coroutine, um beide Animationen gleichzeitig auszuführen
        StartCoroutine(AnimateImages());
    }

    private IEnumerator AnimateImages()
    {
        // Speichere die Startwerte
        Vector3 initialScale = imageToScale.rectTransform.localScale;
        Color initialColor = imageToFade.color;

        // Endwerte
        Vector3 targetScale = initialScale * 1.1f; // Beispiel: Vergrößern um 50%
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0); // Ziel-Opazität 0

        float elapsedTime = 0f;

        // Animation über die Dauer abspielen
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Lerp-Funktion, um den Zwischenwert für Skalierung und Opazität zu berechnen
            float t = elapsedTime / duration;

            imageToScale.rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            imageToFade.color = Color.Lerp(initialColor, targetColor, t);

            yield return null; // Warte einen Frame und führe die Schleife fort
        }

        // Stelle sicher, dass am Ende der Animation die Zielwerte gesetzt sind
        imageToScale.rectTransform.localScale = targetScale;
        imageToFade.color = targetColor;
    }
}
