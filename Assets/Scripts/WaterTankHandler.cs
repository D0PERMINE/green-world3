using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterTankHandler : MonoBehaviour
{
    [SerializeField] Image waterBarImage;
    public AudioSource addWaterAudio;
    [SerializeField] Color originalColor; // Ursprüngliche Farbe des Balkens
    [SerializeField] Color blinkColor; // Farbe, in die geblinkt werden soll

    private float currentWater; // Aktuelles Wasser
    float blinkTimer;
    [SerializeField] bool isBlinking = false;
    private void Start()
    {
        originalColor = waterBarImage.color;
    }
    private void Update()
    {
        //RedBlink();
    }
    public void BlinkRed()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        isBlinking = true;
        
        Blinking();
        
        yield return new WaitForSeconds(3);
        isBlinking = false;
    }

    void Blinking()
    {
        blinkTimer += Time.deltaTime * 5;
        float t = Mathf.Abs(Mathf.Sin(blinkTimer));

        // Verwende Lerp, um zwischen originalColor und blinkColor zu interpolieren
        Color lerpedColor = Color.Lerp(originalColor, blinkColor, t);

        // Setze die Farbe des Wasserbalkens auf den interpolierten Wert
        waterBarImage.color = lerpedColor;
    }

    public void AddWater(float amount)
    {
        currentWater += amount;
        addWaterAudio.Play();
        if (currentWater > 100)
        {
            currentWater = 100;
        }
    }
}
