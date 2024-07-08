using UnityEngine;

public class Option2Controller : MonoBehaviour
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
            quizManager.PlayerOnOption2(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            quizManager.PlayerOnOption2(false);
        }
    }
}