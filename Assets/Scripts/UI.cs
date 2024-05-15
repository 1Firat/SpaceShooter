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
    public GameObject enableUI;
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

            EventGame playerDeath = new(Constant.playerDeath, 0);
            GameEvent.Raise(playerDeath);
            gameOver = true;
        }
        if (gameOver != false && winControl != true)
        {
            disableUI.gameObject.SetActive(false);
            enableUI.gameObject.SetActive(true);
            gameOverText.SetActive(true);
        }
        if (winControl != false && gameOver != false)
        {
            disableUI.gameObject.SetActive(false);
            enableUI.gameObject.SetActive(true);
            winText.SetActive(true);
        }
    }

    void EventListener(EventGame eg)
    {
        if (eg.type == Constant.gameTimer)
        {
            timerText.text = "Time: " + eg.value.ToString("0.##");
        }
        if (eg.type == Constant.gameTimeIsUP)
        {
            gameOver = true;
        }

        if (eg.type == Constant.changeScore)
        {
            scoreText.text = "Score: " + eg.value;
            scoreText2.text = "Your " + scoreText.text;
        }

        if (eg.type == Constant.playerHit)
        {
            heart -= 1;
        }

        if (eg.type == Constant.playerWin)
        {
            winControl = true;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
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