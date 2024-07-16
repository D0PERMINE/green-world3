using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    //public GameObject option1; // Das erste Bild
    //public GameObject option2; // Das zweite Bild
    public TextMeshProUGUI scoreText; // Text für den Punktestand
    public Image progressBar; // Fortschrittsleiste
    public Transform player; // Referenz zum Spieler

    private int score = 0;
    private bool isPlayerOnOption1 = false;
    private bool isPlayerOnOption2 = false;
    private string correctOption = "option1"; // Die richtige Antwort

    private int maxPoints = 100; // Maximalpunktzahl für 100% Fortschritt

    public AudioSource correctAnswerAudio;
    public AudioSource incorrectAnswerAudio;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckAnswer();
        }
    }

    void CheckAnswer()
    {
        if ((isPlayerOnOption1 && correctOption == "option1") || (isPlayerOnOption2 && correctOption == "option2"))
        {
            score += 10;
            correctAnswerAudio.Play();
            UpdateUI();
        }
        else if((isPlayerOnOption1 && correctOption != "option1") || (isPlayerOnOption2 && correctOption != "option2"))
        {
            score -= 10;
            incorrectAnswerAudio.Play();
            UpdateUI();
        }
        else
        {
            Debug.Log("INVALID AREA");
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        UpdateProgressBar();
    }

    public void PlayerOnOption1(bool isOn)
    {
        isPlayerOnOption1 = isOn;
    }

    public void PlayerOnOption2(bool isOn)
    {
        isPlayerOnOption2 = isOn;
    }

    void UpdateProgressBar()
    {
        float progress = (float)score / maxPoints; // Berechne den Fortschritt
        progressBar.fillAmount = progress; // Setze den Fortschritt des Bildes
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log("drin");
    //    if (other.CompareTag("Szenario1"))
    //    {
    //        isPlayerOnOption1 = true;
    //        Debug.Log("1");
    //    }
    //    else if (other.CompareTag("Szenario2"))
    //    {
    //        isPlayerOnOption2 = true;
    //        Debug.Log("2");
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    Debug.Log("raus");
    //    if (other.CompareTag("Szenario1"))
    //    {
    //        isPlayerOnOption1 = false;
    //        Debug.Log("1Q");
    //        Destroy(gameObject);
    //    }
    //    else if (other.CompareTag("Szenario2"))
    //    {
    //        isPlayerOnOption2 = false;
    //        Debug.Log("2Q");
    //    }
    //}
}