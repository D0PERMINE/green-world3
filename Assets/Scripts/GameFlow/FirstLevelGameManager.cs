using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FirstLevelGameManager : MonoBehaviour
{
    public static FirstLevelGameManager Instance { get; private set; }
    [SerializeField] GameObject introControls;
    [SerializeField] GameObject introStory;
    [SerializeField] ShowText showIntroStoryText;
    [SerializeField] GameObject endingStory;
    [SerializeField] GameObject loseStory;
    [SerializeField] float introTime = 5f;

    [SerializeField] int score = 0;
    [SerializeField] int pointsForRightAnswer = 10;
    [SerializeField] int pointsForWrongAnswer = 5;
    public bool wasFirstQuestSolved = false;
    [SerializeField] DoorController doorController;
    [SerializeField] int collectedTrash = 0;
    [SerializeField] int numberOfTrashToSpawn = 1;
    [SerializeField] int timerInSec = 10;
    [SerializeField] WaterTankHandler waterTankHandler;
    [SerializeField] GameObject pauseMenuCanvas;
    [SerializeField] FadeEffect fadeEffect;
    GameState prevGameState;

    public AudioSource willkommenAudio;
    public AudioSource einleitungAudio;

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
        GameStateHandler.Instance.GameState = GameState.introduction;
        introControls.SetActive(true);
        endingStory.SetActive(false);
        introStory.SetActive(false);
        loseStory.SetActive(false);
        BarsProgressManager.Instance.SetMaxTrashBarScore(numberOfTrashToSpawn);
        willkommenAudio.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameStateHandler.Instance.GameState == GameState.introduction)
        {
            introControls.SetActive(false);
            //StartCoroutine(ShowIntroStory());
            ShowIntroStory();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeSelf);
            //GameStateHandler.Instance.GameState = GameStateHandler.Instance.GameState == GameState.pause ? prevGameState : GameState.pause;
            //prevGameState = GameStateHandler.Instance.GameState;
            GameStateHandler.Instance.GameState = GameStateHandler.Instance.GameState == GameState.pause ? prevGameState : (prevGameState = GameStateHandler.Instance.GameState, GameState.pause).Item2;
            Debug.Log("game state: " + prevGameState);
        }


    }

    public float GetTimer()
    {
        return timerInSec;
    }
    //IEnumerator ShowIntroStory()
    //{
    //    introStory.SetActive(true);
    //    yield return new WaitForSeconds(introTime);
    //    GameStateHandler.Instance.GameState = GameState.game;
    //    waterTankHandler.StartWaterTimer();
    //    introStory.SetActive(false);
    //}

    public void ShowIntroStory()
    {
        introStory.SetActive(true);
        willkommenAudio.Stop();
        einleitungAudio.Play();
        //yield return new WaitForSeconds(introTime);
        if (Input.GetKeyDown(KeyCode.Space) && showIntroStoryText.isTyping)
        {
            Debug.Log("Text finished");
            einleitungAudio.Stop();
            GameStateHandler.Instance.GameState = GameState.game;
            //waterTankHandler.StartWaterTimer();
            introStory.SetActive(false);
        }
    }

    public void UpdateScrore(bool isRight)
    {
        if (isRight)
        {
            score += pointsForRightAnswer;          
        }
        else
        {
            //score -= pointsForWrongAnswer;
            waterTankHandler.BlinkRed();
            waterTankHandler.SubtractWater();
        }

        Debug.Log("HELLOOO");
        BarsProgressManager.Instance.UpdateTrashBarScore(score);
        ScoreManager.Instance.UpdateUIPoints(score, isRight);
        // ui points to update here


    }

    public void OnSolvedFirstQuest()
    {
        wasFirstQuestSolved = true;
        if (doorController == null) return;
        doorController.OpenDoor();
    }

    public void EndFirstLevel()
    {
        //endingStory.SetActive(true);
        // show tree as achievment 
        GameStateHandler.Instance.GameState = GameState.endOfFirstQuest;
        StartCoroutine(ShowEndScene());
        //ShowEndScene();
    }

    IEnumerator ShowEndScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        fadeEffect.StartFadeIn();
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(nextSceneIndex);

    }

    //public void ShowEndScene()
    //{
    //    fadeEffect.StartFadeIn();
    //    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    //    //    yield return new WaitForSeconds(introTime);
    //    SceneManager.LoadScene(nextSceneIndex);
    //}

    public void UpdateCollectedTrash()
    {
        collectedTrash++;
        if (collectedTrash >= numberOfTrashToSpawn)
        {     
            // it's solved
            OnSolvedFirstQuest();
        }
    }

    public int  GetNumberOfTrashToSpawn()
    {
        return numberOfTrashToSpawn;
    }

    public void ShowLoseScreen()
    {
        loseStory.SetActive(true);
        // show tree as achievment 
        GameStateHandler.Instance.GameState = GameState.endOfFirstQuest;
        StartCoroutine(ShowLoseScene());
    }

    IEnumerator ShowLoseScene()
    {
        yield return new WaitForSeconds(introTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
