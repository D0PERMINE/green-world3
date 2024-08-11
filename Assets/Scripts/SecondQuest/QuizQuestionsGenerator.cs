using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizQuestionsGenerator : MonoBehaviour
{
    [SerializeField] QuizQuestionSO[] quizQuestions;

    [SerializeField] GameObject image_1;
    [SerializeField] GameObject text_1;


    [SerializeField] GameObject image_2;
    [SerializeField] GameObject text_2;


    private void Start()
    {
        SetCurrentQuiz();
    }

    public void SetCurrentQuiz()
    {
        image_1.GetComponent<Image>().sprite = quizQuestions[QuizUIManager.Instance.currentQuizIndex].Image_1;

        text_1.GetComponent<TMP_Text>().text = quizQuestions[QuizUIManager.Instance.currentQuizIndex].Description_1;

        image_2.GetComponent<Image>().sprite = quizQuestions[QuizUIManager.Instance.currentQuizIndex].Image_2;
        text_2.GetComponent<TMP_Text>().text = quizQuestions[QuizUIManager.Instance.currentQuizIndex].Description_2;


    }

    public QuizQuestionSO GetQuizQuestion()
    {
        return quizQuestions[QuizUIManager.Instance.currentQuizIndex];
    }
}


