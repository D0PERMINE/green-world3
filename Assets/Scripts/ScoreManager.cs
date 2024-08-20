using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    private int score = 0;

    public AudioSource correctAudio;
    public AudioSource incorrectAudio;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateUIPoints(int score, bool isRight)
    {
        PlayAudioScore(isRight);
        scoreText.text = "Score: " + score;
    }

    private void PlayAudioScore(bool isRight)
    {
        if (isRight)
        {
            correctAudio.Play();
        } 
        else 
        {
            incorrectAudio.Play();
        }
    }
}