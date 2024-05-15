using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float time;

    public bool timeControl = false;
    public bool winControl = false;
    public bool gameOver = false;
    public bool isFire = false;
    public bool menuStatus = false;
    public int score;

    void OnEnable()
    {
        score = 0;
    }

    void Start()
    {
        time = DifficultySelect.selected.time;
        GameEvent.RegisterListener(EventListener);
    }

    private void Update()
    {
        time -= Time.deltaTime;

        EventGame gameTime = new(Constant.gameTimer, time);
        GameEvent.Raise(gameTime);

        if (time <= 0)
        {
            EventGame timeIsUp = new(Constant.gameTimeIsUP, 0);
            GameEvent.Raise(timeIsUp);
            gameOver = true;
        }
        if (gameOver != false && score >= DifficultySelect.selected.winScore)
        {
            winControl = true;
            EventGame gameStatus = new(Constant.playerWin, 0);
            GameEvent.Raise(gameStatus);
        }
    }
    void EventListener(EventGame eg)
    {
        if (eg.type == Constant.enemyExploded)
        {
            score += 100;

            EventGame gameScore = new(Constant.changeScore, score);
            GameEvent.Raise(gameScore);
        }
        if (eg.type == Constant.enemyDied)
        {
            score -= 200;
            EventGame gameScore = new(Constant.changeScore, score);
            GameEvent.Raise(gameScore);
        }
        if (eg.type == Constant.ammoBoxCollected)
        {
            EventGame ammoBoxCollected = new(Constant.collectedAmmoBox, 0);
            GameEvent.Raise(ammoBoxCollected);
        }
    }
}
