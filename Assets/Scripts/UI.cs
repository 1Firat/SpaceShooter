using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI pauseTimeText;
    public TextMeshProUGUI scoreViewText;

    public GameObject endGameBackGround;
    public GameObject endGameUI;
    public GameObject gameOverText;
    public GameObject winText;
    public GameObject restartGameButton;
    public GameObject gameUI;
    public GameObject pauseObject;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject grayHeart1;
    public GameObject grayHeart2;
    public GameObject grayHeart3;

    public int heart = 3;
    public float score;
    public float scoreViewCurrentTime;
    public float scoreViewMaxTime = 0.2f;

    public bool gameOver;
    public bool winControl;
    public bool decrease;
    public bool plus;
    public bool changeScore;

    void OnEnable()
    {
        score = 0;
    }
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
        if (heart <= 0)
        {
            heart3.gameObject.SetActive(false);

            EventGame playerDeath = new(Constant.playerDeath, 0, 0);
            GameEvent.Raise(playerDeath);
            gameOver = true;
        }
        if (gameOver && !winControl)
        {
            endGameBackGround.SetActive(true);
            endGameUI.gameObject.SetActive(true);
            gameOverText.SetActive(true);
        }
        if (winControl && gameOver)
        {
            endGameBackGround.SetActive(true);
            endGameUI.gameObject.SetActive(true);
            winText.SetActive(true);
        }
        if (changeScore)
        {
            scoreViewCurrentTime += Time.deltaTime;
            if (scoreViewCurrentTime >= scoreViewMaxTime)
            {
                scoreText.color = Color.white;
                scoreText.text = "Score: " + score;
                scoreText2.text = "Your " + scoreText.text;
                scoreViewCurrentTime = 0;
                changeScore = false;
            }
        }
    }

    void EventListener(EventGame eg)
    {

        if (eg.type == Constant.gameTimer)
        {
            if (eg.value > 15)
            {
                timerText.text = "Time: " + eg.value.ToString("0");
            }
            if (eg.value >= 10 && eg.value <= 15)
            {
                timerText.text = "Time: " + eg.value.ToString("0");
                timerText.color = Color.yellow;
            }
            if (eg.value <= 10)
            {
                timerText.text = "Time: " + eg.value.ToString("0.##");
                timerText.color = Color.red;
            }
            if (eg.value <= 0)
            {
                timerText.text = "Time: 0";
            }
        }
        if (eg.type == Constant.gameTimeIsUP)
        {
            gameOver = true;
        }
        if (eg.type == Constant.changeScore)
        {
            score = eg.value;
            if (eg.value2 == 100)
            {
                scoreText.color = Color.green;
                scoreText.text = "Score: +100";
            }
            if (eg.value2 == -200)
            {
                scoreText.color = Color.red;
                scoreText.text = "Score: -200";
            }
            changeScore = true;
        }
        if (eg.type == Constant.decreaseHeart)
        {
            heart -= 1;
        }
        if (eg.type == Constant.playerWin)
        {
            winControl = true;
        }
        if (eg.type == Constant.pauseGame)
        {
            pauseObject.SetActive(true);
        }
        if (eg.type == Constant.gamePauseTime)
        {
            pauseObject.SetActive(false);
            pauseTimeText.gameObject.SetActive(true);
            pauseTimeText.text = eg.value.ToString("0");
            if (eg.value <= 1)
            {
                pauseTimeText.gameObject.SetActive(false);
            }
        }
        if (eg.type == Constant.plusScore)
        {
            plus = true;
        }
        if (eg.type == Constant.decreaseScore)
        {
            decrease = true;
        }
    }
}