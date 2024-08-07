using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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

    public GameObject[] questions; // Array von GameObjects der Fragen
    private int currentIndex = 0; // Index des aktuell sichtbaren Sprites
    private int currentQuestion;
    private bool isSwitching = false; // Flag, um sicherzustellen, dass das Umschalten nicht mehrfach ausgelöst wird
    private bool endOfQuiz;
    public AchievementDoorController achievementDoorController;

    void Start()
    {
        UpdateUI();
        // Stellen Sie sicher, dass nur das erste Sprite angezeigt wird
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(i == currentIndex);
        }
    }

    void Update()
    {
        currentQuestion = currentIndex + 1;
        endOfQuiz = currentQuestion >= questions.Length;
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
            UpdateQuiz();
        }
        else if((isPlayerOnOption1 && correctOption != "option1") || (isPlayerOnOption2 && correctOption != "option2"))
        {
            score -= 10;
            incorrectAnswerAudio.Play();
            UpdateUI();
            UpdateQuiz();
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

    private void UpdateQuiz()
    {
        if (!isSwitching)
        {
            Debug.Log("Switch Quiz: " + currentQuestion + "/" + questions.Length + endOfQuiz);
            StartCoroutine(SwitchSprite());
        }
    }

    IEnumerator SwitchSprite()
    {
        isSwitching = true;

        // Blende das aktuelle Sprite aus
        questions[currentIndex].SetActive(false);

        // Warte 3 Sekunden
        yield return new WaitForSeconds(1f);

        if (!endOfQuiz)
        {
            // Bestimme das nächste Sprite im Array
            currentIndex = (currentIndex + 1);

            // Blende das nächste Sprite ein
            questions[currentIndex].SetActive(true);

            isSwitching = false;
        }
        else
        {
            achievementDoorController.SetQuizIsFinished(true);
        }

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