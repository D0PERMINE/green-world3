using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementDoorController : MonoBehaviour
{

    private bool quizIsFinished;
    private string achievementScene;
    // Start is called before the first frame update
    void Start()
    {
        achievementScene = "Achievement";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hello Player!");
        if (collision.CompareTag("Player") && quizIsFinished)
        {
            Debug.Log("Go to achievement scene!");
            SceneManager.LoadScene(achievementScene);
        }
    }

    public void SetQuizIsFinished(bool quizIsFinished)
    {
        this.quizIsFinished = quizIsFinished;
    }
}
