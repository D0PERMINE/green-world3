using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondLevelGameManager : MonoBehaviour
{
    public static SecondLevelGameManager Instance { get; private set; }
    [SerializeField] GameObject introStory;
    [SerializeField] GameObject endingStory;
    [SerializeField] GameObject loseStory;
    [SerializeField] float introTime = 5f;

    [SerializeField] int score = 0;
    [SerializeField] int pointsForRightAnswer = 10;
    [SerializeField] int pointsForWrongAnswer = 5;
    [SerializeField] bool wasSecondQuestSolved = false;
    [SerializeField] QuizQuestionsGenerator quizQuestionsGenerator;
    [SerializeField] int timerInSec = 10;
    [SerializeField] WaterTankHandler waterTankHandler;
    [SerializeField] FadeToTransparentEffect fadeToTransparentEffect;
    [SerializeField] CameraController cameraController;
    [SerializeField] private GameObject playerMenuCanvas;

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

    void Start()
    {
        fadeToTransparentEffect.StartFadeOut();
        StartCoroutine(ZoomOut());
        waterTankHandler.StartWaterTimer();

        Debug.Log("game state1: " + GameStateHandler.Instance.GameState);
        GameStateHandler.Instance.GameState = GameState.game;
        Debug.Log("game state2: " + GameStateHandler.Instance.GameState);
       
        //endingStory.SetActive(false);
        //introStory.SetActive(true);
        loseStory.SetActive(false);
        //StartCoroutine(ShowIntroStory());
        BarsProgressManager.Instance.SetMaxQuizBarScore(quizQuestionsGenerator.GetQuizQuestionsArray().Length);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameStateHandler.Instance.GameState == GameState.game)
        {
            playerMenuCanvas.SetActive(true);
            GameStateHandler.Instance.GameState = GameState.pause;
        }
    }

    IEnumerator ZoomOut()
    {
        yield return new WaitForSeconds(3f);
        cameraController.ZoomOutCamera();
    }

    //IEnumerator ShowIntroStory()
    //{
    //    introStory.SetActive(true);
    //    yield return new WaitForSeconds(introTime);
    //    introStory.SetActive(false);
    //    waterTankHandler.StartWaterTimer();
    //}

    public void UpdateScrore(bool isRight)
    {
        if (isRight)
        {
            score += pointsForRightAnswer;
            Debug.Log("score: " + score);
        }
        else
        {
            //score -= pointsForWrongAnswer;
            Debug.Log("score: " + score);
            waterTankHandler.BlinkRed();
            waterTankHandler.SubtractWater();
        }
        ScoreManager.Instance.UpdateUIPoints(score, isRight);
        BarsProgressManager.Instance.UpdateQuizhBarScore(score);

    }


    public void EndFirstLevel()
    {
        wasSecondQuestSolved = true;
        //endingStory.SetActive(true);
        GameStateHandler.Instance.GameState = GameState.endOfSecondQuest;
        StartCoroutine(ShowEndScene());

    }

    IEnumerator ShowEndScene()
    {
        yield return new WaitForSeconds(introTime);
        //SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void ShowLoseScreen()
    {
        loseStory.SetActive(true);
        // show tree as achievment 
        GameStateHandler.Instance.GameState = GameState.endOfFirstQuest;
        //StartCoroutine(ShowLoseScene());
    }

    //IEnumerator ShowLoseScene()
    //{
    //    yield return new WaitForSeconds(introTime);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    public float GetTimer()
    {
        return timerInSec;
    }
}
