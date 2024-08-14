using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  UnityEngine.SceneManagement;



public class BarsProgressManager : MonoBehaviour
{
    public static BarsProgressManager Instance { get; private set; }
    [SerializeField] Image trashBarFilling;
    [SerializeField] Image quizBarFilling;

    static int trashBarScore = 0;
    static int quizBarScore = 0;
    static float trashBarAmount = 0;
    static float quizBarAmount = 0;

    [SerializeField] int maxTrashBarScore = 0;
    [SerializeField] int maxQuizBarScore = 0;
    [SerializeField] float percentageOfSingleCollectedTrash = 0;
    [SerializeField] float percentageOfSingleQuestion = 0;
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

    private void Start()
    {
        trashBarFilling.fillAmount = trashBarAmount;
        quizBarFilling.fillAmount = quizBarAmount;
        Debug.Log("trashBarScore: " + trashBarScore);
        Debug.Log("quizBarScore: " + quizBarScore);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void UpdateTrashBarScore(int newScore)
    {
        trashBarScore = newScore;
        trashBarFilling.fillAmount += (percentageOfSingleCollectedTrash / 100);
        trashBarAmount = trashBarFilling.fillAmount;
    }

    public void UpdateQuizhBarScore(int newScore)
    {
        quizBarScore = newScore;
        // 10 - 100%; 1 - 10%
        // 100 /10, 10/100
        quizBarFilling.fillAmount += (percentageOfSingleQuestion/100);
        quizBarAmount = quizBarFilling.fillAmount;
    }

    public void SetMaxTrashBarScore( int maxScore)
    {
        maxTrashBarScore = maxScore;
        //calculate percent of single collected trash
        percentageOfSingleCollectedTrash = 100 / maxTrashBarScore;
    }

    public void SetMaxQuizBarScore(int maxScore)
    {
        maxQuizBarScore = maxScore;
        percentageOfSingleQuestion = 100 / maxQuizBarScore;

    }
}
