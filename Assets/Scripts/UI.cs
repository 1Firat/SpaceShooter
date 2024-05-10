using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;

    public GameObject gameOverText;
    public GameObject winText;
    public GameObject menuButton;
    public GameObject restartGameButton;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject grayHeart1;
    public GameObject grayHeart2;
    public GameObject grayHeart3;

    public int addScore = 100;
    public int decreaseScore = 200;
    public int heart = 3;
    public int score = 0;
    public bool gameOver;
    public bool winControl;

    void Start()
    {
        GameEvent.RegisterListener(EventListener);
    }

    private void Update()
    {
        if (heart == 3)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
        }
        if (heart == 2)
        {
            heart1.gameObject.SetActive(false);
        }
        if (heart == 1)
        {
            heart1.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
        }
        if (heart == 0)
        {
            heart3.gameObject.SetActive(false);

            EventGame playerDeath = new("player_death", 0);
            GameEvent.Raise(playerDeath);
            gameOver = true;
        }
        if (gameOver != false && winControl != true)
        {
            MenuControl();
            gameOverText.SetActive(true);
        }
        if (winControl != false && gameOver != false)
        {
            MenuControl();
            winText.SetActive(true);
        }
    }

    public void MenuControl()
    {
        heart1.gameObject.SetActive(false);
        heart2.gameObject.SetActive(false);
        heart3.gameObject.SetActive(false);
        grayHeart1.gameObject.SetActive(false);
        grayHeart2.gameObject.SetActive(false);
        grayHeart3.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        scoreText2.gameObject.SetActive(true);
        restartGameButton.SetActive(true);
    }

    void EventListener(EventGame eg)
    {
        if (eg.type == "game_time")
        {
            timerText.text = "Time: " + (int)eg.value;
        }
        if (eg.type == "time_is_up")
        {
            gameOver = true;
        }

        if (eg.type == "change_score")
        {
            scoreText.text = "Score: " + eg.value;
            scoreText2.text = "Your Score: " + eg.value;
            Debug.Log("yazılan score: " + eg.value);
        }
        //if (eg.type == "game_decrease_score")
        //{
        //    GameScore("decrease");
        //}


        if (eg.type == "player_hit")
        {
            heart -= 1;
        }

        if (eg.type == "player_win")
        {
            winControl = true;
        }
    }

    //void GameScore(string type)
    //{
    //    if (type == "add")
    //    {
    //        score += addScore;
    //        Debug.Log("eklenen score: "+score);
    //    }
    //    if (type == "decrease")
    //    {
    //        score -= decreaseScore;
    //    }
    //    scoreText.text = "Score: " + score;
    //    scoreText2.text = "Your Score: " + score;
    //    Debug.Log("yazılan score: " + score);
    //    EventGame pScore = new("score", score);
    //    GameEvent.Raise(pScore);
    //}

    public void MainMenu()
    {
        SceneManager.LoadScene("Game");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}