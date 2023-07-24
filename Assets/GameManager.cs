using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int highScore=0;
    int score = 0;
    bool isScoreUpdatable = true;
    bool isPlayerAlive = true;


    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI ScoreText;

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
    }
}
