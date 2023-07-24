using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int highScore=0;
    int score = 0;
    bool isScoreUpdatable = true;
    bool isPlayerAlive = true;
    float timeIncrementInterval = 10f;
    float currentTimeSpeed = 1f;

    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI HighScoreText02;
    public TextMeshProUGUI HighScoreText03;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ScoreText02;
    public TextMeshProUGUI ScoreText03;


    public GameObject pauseMenuPanel;
    public GameObject gameFailedPanel;


    private void OnEnable()
    {
        PlayerController.PlayerDeadAction += GameFailed;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentTimeSpeed = 1f;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreText.text = "HighScore:" + highScore;
        ScoreText.text="Score:"+0;
        PauseGame();
        StartCoroutine(TimeSpeedIncrement());

    }

    // Update is called once per frame
    void Update()
    {
        if(isScoreUpdatable&&isPlayerAlive)
        {
            isScoreUpdatable=false;
            StartCoroutine(UpdateScore());
            
        }
        
    }
    void UpdateHighScore()
    {
        if(highScore>PlayerPrefs.GetInt("HighScore",0))
        {

            PlayerPrefs.SetInt("HighScore", score);
        }
    }
    IEnumerator UpdateScore()
    {
        score++;
        ScoreText.text = "Score:" + score;
        if (highScore < score)
        {
            highScore= score;
            HighScoreText.text = "HighScore:" + highScore;
        }
        yield return new WaitForSeconds(1);
        isScoreUpdatable = true;
    }
    void GameFailed()
    {
        isPlayerAlive = false;
        UpdateHighScore();
        HighScoreText03.text = "HighScore:" + highScore;
        ScoreText03.text = "Score:" + score;
        
        StartCoroutine(GameFailedHelper());
        
    }
    IEnumerator GameFailedHelper()
    {
        
        yield return new WaitForSeconds(1.8f);
        Time.timeScale = 0f;
        gameFailedPanel.SetActive(true);
    }
    public void PauseGame()
    {
        
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
        UpdatePauseMenuScores();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Play()
    {
        Time.timeScale = currentTimeSpeed;
        pauseMenuPanel.SetActive(false);
    }
    void UpdatePauseMenuScores()
    {
        HighScoreText02.text = "HighScore:" + highScore;
        ScoreText02.text = "Score:" + score;
    }
    private void OnDisable()
    {
        PlayerController.PlayerDeadAction -= GameFailed;
    }
    IEnumerator TimeSpeedIncrement()
    {

        yield return new WaitForSeconds(timeIncrementInterval);
        currentTimeSpeed += currentTimeSpeed / 10;
        StartCoroutine(TimeSpeedIncrement());
    }
}
