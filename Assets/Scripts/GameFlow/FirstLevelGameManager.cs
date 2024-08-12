using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelGameManager : MonoBehaviour
{
    public static FirstLevelGameManager Instance { get; private set; }
    [SerializeField] GameObject introControls;
    [SerializeField] GameObject introStory;
    [SerializeField] float introTime = 5f;
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameStateHandler.Instance.GameState == GameState.introduction)
        {
            introControls.SetActive(false);
            StartCoroutine(ShowIntroStory());
        }
    }

    IEnumerator ShowIntroStory()
    {
        introStory.SetActive(true);
        yield return new WaitForSeconds(introTime);
        GameStateHandler.Instance.GameState = GameState.game;
        introStory.SetActive(false);
    }
}
