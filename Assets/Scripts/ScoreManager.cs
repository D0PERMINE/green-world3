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

    public void AddScore(int points)
    {
        score += points;
        PlayAudioScore(points);
        scoreText.text = "Score: " + score;
    }

    private void PlayAudioScore(int points)
    {
        if (points > 0)
        {
            correctAudio.Play();
        } 
        else if( points < 0)
        {
            incorrectAudio.Play();
        }
    }
}