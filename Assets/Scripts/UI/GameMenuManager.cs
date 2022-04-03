using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenuManager : MonoBehaviour
{
    // References

    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private GameObject StartMenu;
    [SerializeField] private TextMeshProUGUI ScoreText;

    public static GameMenuManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        CheckForPause();
        UpdateScore();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TogglePauseMenu()
    {
        if(PauseMenu.activeInHierarchy == true)
        {
            GameManager.Instance.UpdateGameState(GameState.Running);
            PauseMenu.SetActive(false);
        }
        else
        {
            PauseMenu.SetActive(true);
        }
    }
    public void ToggleGameOverMenu()
    {
        if (GameOverMenu.activeInHierarchy == true)
        {
            GameOverMenu.SetActive(false);
        }
        else
        {
            GameOverMenu.SetActive(true);
        }
    }
    public void ToggleStartMenu()
    {
        if (StartMenu.activeInHierarchy == true)
        {
            GameManager.Instance.UpdateGameState(GameState.Running);
            StartMenu.SetActive(false);
        }
        else
        {
            StartMenu.SetActive(true);
        }
    }
    private void UpdateScore()
    {
        int score = (int)GameManager.Instance.GameScore;
        if (score % 10 == 0)
        {
            ScoreText.text = "Score: " + score;
        }
    }

    private void CheckForPause()
    {
        if (GameManager.Instance.State == GameState.Running)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log(GameManager.Instance.State);
                Debug.Log(GameState.Start);
                Debug.Log(GameManager.Instance.State == GameState.Start);
                GameManager.Instance.UpdateGameState(GameState.Pause);
            }
        }
    }
}
