using UnityEngine;
using UnityEngine.UI;

public class CanvasAspectController : MonoBehaviour
{
    public Canvas canvas;
    public float targetAspectWidth = 16.0f;
    public float targetAspectHeight = 9.0f;

    void Start()
    {
        AdjustCanvas();
    }

    void Update()
    {
        AdjustCanvas();
    }

    void AdjustCanvas()
    {
        // Berechne das gewünschte Seitenverhältnis
        float targetAspect = targetAspectWidth / targetAspectHeight;

        // Aktuelles Fenster-Seitenverhältnis
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // Berechne das Skalierungsverhältnis der Höhe
        float scaleHeight = windowAspect / targetAspect;

        CanvasScaler canvasScaler = canvas.GetComponent<CanvasScaler>();

        // Wenn das aktuelle Fenster-Seitenverhältnis größer als das gewünschte ist
        if (scaleHeight < 1.0f)
        {
            canvasScaler.matchWidthOrHeight = 0; // Passe die Breite an
        }
        else // Wenn das aktuelle Fenster-Seitenverhältnis kleiner oder gleich dem gewünschten ist
        {
            canvasScaler.matchWidthOrHeight = 1; // Passe die Höhe an
        }
    }
}
