using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBarController : MonoBehaviour
{
    public Image waterBarImage; // Referenz zum UI-Image
    public float maxWater = 100f; // Maximales Wasser
    private float currentWater; // Aktuelles Wasser

    public float blinkSpeedNormal = 5.0f; // Geschwindigkeit des Blinkens
    public float blinkSpeedFast; // // Schnellere Geschwindigkeit des Blinkens

    private bool isBlinking = false;
    private Color originalColor; // Urspr�ngliche Farbe des Balkens
    private Color blinkColor; // Farbe, in die geblinkt werden soll
    private float blinkTimer = 0f;

    public AudioSource addWaterAudio;



    void Start()
    {
        currentWater = maxWater;

        if (waterBarImage == null)
        {
            Debug.LogError("Water Bar Image is not assigned.");
        }

        blinkSpeedFast = blinkSpeedNormal + 7.5f;

        originalColor = waterBarImage.color;

        blinkColor = new Color(1f, 0.514f, 0.427f);
    }

    void Update()
    {
        if (waterBarImage == null)
        {
            Debug.LogError("Water Bar Image is not assigned.");
        }
        
        // Reduziere das Wasser mit der Zeit
        currentWater -= Time.deltaTime * 5; // Wasser reduziert sich um 5 Einheiten pro Sekunde
        
        
        if (currentWater < 0)
        {
            currentWater = 0;
        }

        // Aktualisiere die Wasseranzeige
        waterBarImage.fillAmount = currentWater / maxWater;

        // �berpr�fe, ob der Wasserstand unter 30% gefallen ist
        if (currentWater < 30f)
        {
            if (!isBlinking)
            {
                StartBlinking();
            }
        }
        else
        {
            StopBlinking();
        }

        // Blinklogik ausf�hren, wenn isBlinking true ist
        if (isBlinking)
        {
            // Berechne den Alpha-Wert basierend auf der Blinkgeschwindigkeit
            blinkTimer += Time.deltaTime * GetCurrentBlinkSpeed();

            float t = Mathf.Abs(Mathf.Sin(blinkTimer));

            // Verwende Lerp, um zwischen originalColor und blinkColor zu interpolieren
            Color lerpedColor = Color.Lerp(originalColor, blinkColor, t);

            // Setze die Farbe des Wasserbalkens auf den interpolierten Wert
            waterBarImage.color = lerpedColor;
        }

    }

    // Methode zum Hinzuf�gen von Wasser (kann von anderen Scripts aufgerufen werden)
    public void AddWater(float amount)
    {
        currentWater += amount;
        addWaterAudio.Play();
        if (currentWater > maxWater)
        {
            currentWater = maxWater;
        }
    }

    void StartBlinking()
    {
        isBlinking = true;
        blinkTimer = 0f;
    }

    void StopBlinking()
    {
        isBlinking = false;
        waterBarImage.color = originalColor; // Setze die Farbe auf die urspr�ngliche Farbe zur�ck
    }

    // Funktion, die die aktuelle Blinkgeschwindigkeit basierend auf dem Zustand zur�ckgibt
    float GetCurrentBlinkSpeed()
    {
        // Je nach Zustand (normal oder schnell) die entsprechende Blinkgeschwindigkeit zur�ckgeben
        if (currentWater < 20f)
        {
            return blinkSpeedFast;
        }
        else
        {
            return blinkSpeedNormal;
        }
    }
}
