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
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreText.text = "HighScore:" + highScore;
        ScoreText.text="Score:"+0;
        PauseGame();
        Debug.Log(PlayerPrefs.GetInt("HighScore", 0));
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
        isPlayerAlive= false;
        UpdateHighScore();
        HighScoreText03.text = "HighScore:" + highScore;
        ScoreText03.text = "Score:" + score;
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
        Time.timeScale = 1f;
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
}
