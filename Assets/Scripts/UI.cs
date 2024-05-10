using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    public GameObject gameOverText;
    public GameObject winText;
    public GameObject menuButton;
    public GameObject restartGameButton;
    public GameObject disableUI;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject grayHeart1;
    public GameObject grayHeart2;
    public GameObject grayHeart3;

    public int heart = 3;
    public bool gameOver;
    public bool winControl;
    Vector3 startPos = new Vector3(-836f, 398f, 0f);
    Vector3 endPos = new Vector3(67.6f, 0f, 0f);
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
            disableUI.gameObject.SetActive(false);

            scoreText.transform.position = endPos;
            gameOverText.SetActive(true);
        }
        if (winControl != false && gameOver != false)
        {

            disableUI.gameObject.SetActive(false);
            winText.SetActive(true);
        }
    }

    void EventListener(EventGame eg)
    {
        if (eg.type == "game_time")
        {
            timerText.text = "Time: " + eg.value.ToString("0.##");
        }
        if (eg.type == "time_is_up")
        {
            gameOver = true;
        }

        if (eg.type == "change_score")
        {
            scoreText.text = "Score: " + eg.value;
        }

        if (eg.type == "player_hit")
        {
            heart -= 1;
        }

        if (eg.type == "player_win")
        {
            winControl = true;
        }
    }

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