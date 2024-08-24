using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    introduction,
    game,
    gameDelay,
    pause,
    cameraTransition,
    sceneTransition,
    firstQuizIsSolved,
    endOfFirstQuest,
    endOfSecondQuest
}
public class GameStateHandler : MonoBehaviour
{
    public static GameStateHandler Instance { get; private set; }
    public GameState GameState { get => this._gameState; set => this._gameState = value; }
    [SerializeField]
    private GameState _gameState = 0;

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
}
