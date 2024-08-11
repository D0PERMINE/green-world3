using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizUIManager : MonoBehaviour
{
    public static QuizUIManager Instance { get; private set; }
    [SerializeField] SecondQuestOptionHandler selectedOption;
    public int currentQuizIndex = 0;


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSelectedAnswer();
        }
    }
    public void SetCurrentSelectedOption(SecondQuestOptionHandler selectedOption)
    {
        this.selectedOption = selectedOption;
    }

    public void CheckSelectedAnswer()
    {
        if (selectedOption == null) return;
        int rightOptionId = (int)GetComponent<QuizQuestionsGenerator>().GetQuizQuestion().rightOption;
        if(rightOptionId == selectedOption.GetOptionId())
        {
            Debug.Log("Was right option selected");
        }
        else
        {
            Debug.Log("Was wrong option selected");
        }
    }

}