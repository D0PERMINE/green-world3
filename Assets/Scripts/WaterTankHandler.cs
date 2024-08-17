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

    [SerializeField] float currentWater; // Aktuelles Wasser
    [SerializeField] float maxWater = 1f; // Aktuelles Wasser
    float blinkTimer;
    [SerializeField] float levelDuration; 
    [SerializeField] float blinkingDuration = 1;
    [SerializeField] bool isBlinking = false;
    [SerializeField] float addedWaterAmount = 0.25f;
    [SerializeField] float substractedWaterAmount = 0.01f;
    [SerializeField] float normalizedWaterTime = 0;
    [SerializeField] GameObject gameManager;
    private void Start()
    {
        originalColor = waterBarImage.color;
        currentWater = maxWater;
        SetLevelDuration();
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddWater();
        }
    }

    void SetLevelDuration()
    {
        if (gameManager.GetComponent<FirstLevelGameManager>() != null)
        {
            levelDuration = gameManager.GetComponent<FirstLevelGameManager>().GetTimer();
        }
        else if (gameManager.GetComponent<SecondLevelGameManager>() != null)
        {
            levelDuration = gameManager.GetComponent<SecondLevelGameManager>().GetTimer();
        }
        else
        {
            Debug.Log("Game Manager error ");
        }
    } 
    public void BlinkRed()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {

        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            Blinking();
            normalizedTime += Time.deltaTime / blinkingDuration;
            yield return null;
        }

        waterBarImage.color = originalColor;
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

    public void AddWater()
    {
        waterBarImage.fillAmount += addedWaterAmount;
        addWaterAudio.Play();
        normalizedWaterTime -= addedWaterAmount;
        if(normalizedWaterTime < 0)
        {
            normalizedWaterTime = 0;
        }
    }

    public void SubtractWater()
    {
        waterBarImage.fillAmount -= substractedWaterAmount;
        addWaterAudio.Play();
        normalizedWaterTime += substractedWaterAmount;
    }
    public void StartWaterTimer()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (normalizedWaterTime <= 1f)
        {

            normalizedWaterTime += Time.deltaTime / levelDuration;
            waterBarImage.fillAmount -= Time.deltaTime / levelDuration;
            yield return null;
        }

        Debug.Log("Lost");
        ShowLoseScreen();
    }

    void ShowLoseScreen()
    {
        if (gameManager.GetComponent<FirstLevelGameManager>() != null)
        {
            gameManager.GetComponent<FirstLevelGameManager>().ShowLoseScreen();
        }
        else if (gameManager.GetComponent<SecondLevelGameManager>() != null)
        {
            gameManager.GetComponent<SecondLevelGameManager>().ShowLoseScreen();
        }
        else
        {
            Debug.Log("Game Manager error ");
        }
    }
    //IEnumerator Timer()
    //{
    //    // leverDuration 100% 15 sec
    //    // 15/100 - 1% /100
    //    float normalizedTime = 0;
    //    while (normalizedTime <= 1f)
    //    {
    //        currentWater -= 0.1f;
    //        if (currentWater < 0)
    //        {
    //            currentWater = 0;
    //        }

    //        normalizedTime += Time.deltaTime/ levelDuration;
    //        waterBarImage.fillAmount = currentWater / maxWater;
    //        yield return null;
    //    }

    //    waterBarImage.color = originalColor;
    //}
}
