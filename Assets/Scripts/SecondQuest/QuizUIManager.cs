using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizUIManager : MonoBehaviour
{
    public static QuizUIManager Instance { get; private set; }
    [SerializeField] SecondQuestOptionHandler selectedOption;
    public int currentQuizIndex = 0;
    //[SerializeField] bool endOfQuiz = false;

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
        //if (currentQuizIndex >= GetComponent<QuizQuestionsGenerator>().GetQuizQuestionsArray().Length-1) { endOfQuiz = true; return; }
        int rightOptionId = (int)GetComponent<QuizQuestionsGenerator>().GetQuizQuestion().rightOption;
        if(rightOptionId == selectedOption.GetOptionId())
        {
            Debug.Log("Was right option selected");
            SecondLevelGameManager.Instance.UpdateScrore(true);
        }
        else
        {
            Debug.Log("Was wrong option selected");
            SecondLevelGameManager.Instance.UpdateScrore(false);
        }
        UpdateQuiz();
    }

    //void UpdateProgressBar()
    //{
    //    float progress = (float)score / maxPoints; // Berechne den Fortschritt
    //    progressBar.fillAmount = progress; // Setze den Fortschritt des Bildes
    //}

    private void UpdateQuiz()
    {
       
            Debug.Log("Switch Question");
            StartCoroutine(SwitchSprite());
        
    }

    IEnumerator SwitchSprite()
    {     
        yield return new WaitForSeconds(0f);

        if (currentQuizIndex < GetComponent<QuizQuestionsGenerator>().GetQuizQuestionsArray().Length-1)
        {
            currentQuizIndex++;
            GetComponent<QuizQuestionsGenerator>().SetCurrentQuiz();
        }
        else
        {
            //endOfQuiz = true;
            SecondLevelGameManager.Instance.EndFirstLevel();
        }

    }


}