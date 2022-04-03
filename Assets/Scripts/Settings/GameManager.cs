using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

    public float GameScore;

    //public static event Action<GameState> OnGameStateChange;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.Start);
        GameScore = 0;
    }

    public void UpdateGameState(GameState State)
    {
        this.State = State;

        switch (State)
        {
            case GameState.Start:
                HandleStartGame();
                break;
            case GameState.Running:
                HandleRunGame();
                break;
            case GameState.Pause:
                HandlePauseGame();
                break;
            case GameState.GameOver:
                HandleEndGame();
                break;
            default:
                break;
        }
        //OnGameStateChange?.Invoke(State);
    }

    // Update is called once per frame
    void Update()
    {
        if (State != GameState.Start)
        {
            GameScore += Time.deltaTime * 100;
        }
    }
    private void HandleStartGame()
    {
        Time.timeScale = 0; //0
        Cursor.lockState = CursorLockMode.None;
        GameMenuManager.Instance.ToggleStartMenu();
    }
    private void HandleRunGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void HandlePauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GameMenuManager.Instance.TogglePauseMenu();
    }
    private void HandleEndGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GameMenuManager.Instance.ToggleGameOverMenu();
    }

}

public enum GameState
{
    Start,
    Running,
    Pause,
    GameOver
}
