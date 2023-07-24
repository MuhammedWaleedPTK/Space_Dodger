using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int highScore = 0;
    int score = 0;
    bool isScoreUpdatable = true;
    bool isPlayerAlive = true;
    float timeIncrementInterval = 10f;
    float currentTimeSpeed = 1f;

    // UI Elements
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI HighScoreText02;
    public TextMeshProUGUI HighScoreText03;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ScoreText02;
    public TextMeshProUGUI ScoreText03;

    // Game Over Panels
    public GameObject pauseMenuPanel;
    public GameObject gameFailedPanel;

    private void OnEnable()
    {
        PlayerController.PlayerDeadAction += GameFailed;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize time speed and high score
        currentTimeSpeed = 1f;
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Update UI with initial high score and score
        HighScoreText.text = "HighScore:" + highScore;
        ScoreText.text = "Score:" + 0;

        // Pause the game at the beginning
        PauseGame();

        // Start incrementing time speed at intervals
        StartCoroutine(TimeSpeedIncrement());
    }

    // Update is called once per frame
    void Update()
    {
        // Update the score at intervals if score is updatable and player is alive
        if (isScoreUpdatable && isPlayerAlive)
        {
            isScoreUpdatable = false;
            StartCoroutine(UpdateScore());
        }
    }

    // Save high score in PlayerPrefs
    void UpdateHighScore()
    {
        if (highScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    // Coroutine to update the score at intervals
    IEnumerator UpdateScore()
    {
        score++;
        ScoreText.text = "Score:" + score;

        // Update high score if the current score surpasses it
        if (highScore < score)
        {
            highScore = score;
            HighScoreText.text = "HighScore:" + highScore;
        }

        // Wait for 1 second before score can be updated again
        yield return new WaitForSeconds(1);
        isScoreUpdatable = true;
    }

    // Function to be called when the player dies
    void GameFailed()
    {
        isPlayerAlive = false;

        // Update high score and display game over panels
        UpdateHighScore();
        HighScoreText03.text = "HighScore:" + highScore;
        ScoreText03.text = "Score:" + score;

        // Wait for 1.8 seconds before displaying the game over panel
        StartCoroutine(GameFailedHelper());
    }

    // Coroutine to display the game over panel
    IEnumerator GameFailedHelper()
    {
        yield return new WaitForSeconds(1.8f);
        Time.timeScale = 0f;
        gameFailedPanel.SetActive(true);
    }

    // Function to pause the game
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
        UpdatePauseMenuScores();
    }

    // Function to exit the game
    public void ExitGame()
    {
        Application.Quit();
    }

    // Function to restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Function to resume the game
    public void Play()
    {
        Time.timeScale = currentTimeSpeed;
        pauseMenuPanel.SetActive(false);
    }

    // Update the scores in the pause menu
    void UpdatePauseMenuScores()
    {
        HighScoreText02.text = "HighScore:" + highScore;
        ScoreText02.text = "Score:" + score;
    }

    private void OnDisable()
    {
        PlayerController.PlayerDeadAction -= GameFailed;
    }

    // Coroutine to increment the time speed at intervals
    IEnumerator TimeSpeedIncrement()
    {
        yield return new WaitForSeconds(timeIncrementInterval);
        currentTimeSpeed += currentTimeSpeed / 10;
        StartCoroutine(TimeSpeedIncrement());
    }
}
