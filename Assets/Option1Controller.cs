using UnityEngine;

public class Option1Controller : MonoBehaviour
{
    private QuizManager quizManager;

    void Start()
    {
        quizManager = FindObjectOfType<QuizManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            quizManager.PlayerOnOption1(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            quizManager.PlayerOnOption1(false);
        }
    }
}
