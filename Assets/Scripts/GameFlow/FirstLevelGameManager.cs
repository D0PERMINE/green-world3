using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FirstLevelGameManager : MonoBehaviour
{
    public static FirstLevelGameManager Instance { get; private set; }
    [SerializeField] private GameObject introControls;
    [SerializeField] private GameObject introStory;
    [SerializeField] private ShowText showIntroStoryText;
    [SerializeField] private GameObject endingStory;
    [SerializeField] private GameObject loseStory;
    [SerializeField] private float introTime = 5f;

    [SerializeField] private int score = 0;
    [SerializeField] private int pointsForRightAnswer = 10;
    [SerializeField] private int pointsForWrongAnswer = 5;
    public bool wasFirstQuestSolved = false;
    [SerializeField] private DoorController doorController;
    [SerializeField] private int collectedTrash = 0;
    [SerializeField] private int numberOfTrashToSpawn = 1;
    [SerializeField] private int timerInSec = 10;
    [SerializeField] private WaterTankHandler waterTankHandler;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject playerMenuCanvas;
    [SerializeField] private FadeEffect fadeEffect;
    GameState prevGameState;

    [SerializeField] private AudioSource willkommenAudio;
    [SerializeField] private AudioSource einleitungAudio;
    [SerializeField] private ShowText showText;


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
        // Fange mit Intro an und zeige die Steuerung
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
            ShowIntroStory();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameStateHandler.Instance.GameState == GameState.game)
        {
            playerMenuCanvas.SetActive(true);
            GameStateHandler.Instance.GameState = GameState.pause;
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
        if (Input.GetKeyDown(KeyCode.Space) && showIntroStoryText.IsTyping)
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
        GameStateHandler.Instance.GameState = GameState.firstQuizIsSolved;
        endingStory.SetActive(true);

        // Starte die Coroutine, um zu warten, bis der Text vollständig getippt ist
        StartCoroutine(WaitForTextToFinish());
    }

    private IEnumerator WaitForTextToFinish()
    {
        while (!showText.TextIsFinished)
        {
            if (Input.GetKeyDown(KeyCode.Space) && showText.IsTyping)
            {
                break;  // Breche die schleife ab, da die Story beendet wurde
            }

            yield return null; // Warte einen Frame und überprüfe dann erneut
        }

        endingStory.SetActive(false);
        wasFirstQuestSolved = true;
    }

    public void EndFirstLevel()
    {
        GameStateHandler.Instance.GameState = GameState.endOfFirstQuest;
        Debug.Log("GAME State: " + GameStateHandler.Instance.GameState);
        StartCoroutine(ShowEndScene());
    }

    IEnumerator ShowEndScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        fadeEffect.StartFadeIn();
        GameStateHandler.Instance.GameState = GameState.sceneTransition;
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(nextSceneIndex);

    }

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
        GameStateHandler.Instance.GameState = GameState.endOfFirstQuest;
        //StartCoroutine(ShowLoseScene());
    }

    //IEnumerator ShowLoseScene()
    //{
    //    yield return new WaitForSeconds(introTime);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}
}
