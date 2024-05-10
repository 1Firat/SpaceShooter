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
    public bool ammoBoxControl = false;
    public bool menuStatus = false;
    public int score;

    void OnEnable()
    {
        score = 0;
    }
    void Start()
    {
        score = 0;
        time = DifficultySelect.selected.time;
        GameEvent.RegisterListener(EventListener);
    }

    private void Update()
    {
        time -= Time.deltaTime;

        EventGame gameTime = new("game_time", time);
        GameEvent.Raise(gameTime);

        if (time <= 0)
        {
            EventGame timeIsUp = new("time_is_up", 0);
            GameEvent.Raise(timeIsUp);
            gameOver = true;
        }
        if (gameOver != false && score >= DifficultySelect.selected.winScore)
        {
            winControl = true;
            EventGame gameStatus = new("player_win", 0);
            GameEvent.Raise(gameStatus);
        }
    }

    void EventListener(EventGame eg)
    {
        if (eg.type == "enemy_exploded")
        {
            score += 100;

            EventGame gameScore = new("change_score", score);
            GameEvent.Raise(gameScore);
        }
        if (eg.type == "enemy_died")
        {
            score -= 200;
            EventGame gameScore = new("change_score", score);
            GameEvent.Raise(gameScore);
        }
        if (eg.type == "ammo_box_collected")
        {
            EventGame collectedAmmoBox = new("collected_ammo_box", 0);
            GameEvent.Raise(collectedAmmoBox);
        }
    }
}
