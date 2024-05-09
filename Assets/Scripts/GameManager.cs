using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float time;

    public bool timeControl = false;
    public bool winControl = false;
    public bool gameOverControl = false;
    public bool isFire = false;
    public bool ammoBoxControl = false;
    public bool menuStatus = false;
    public int playerScore;

    void Start()
    {
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
            EventGame gameOver = new("time_is_up", 0);
            GameEvent.Raise(gameOver);
        }
        if (playerScore >= DifficultySelect.selected.winScore)
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
            EventGame gameScore = new("game_score", 100);
            GameEvent.Raise(gameScore);
        }
        if (eg.type == "enemy_died")
        {
            EventGame gameScore = new("game_score", -200);
            GameEvent.Raise(gameScore);
        }
        if (eg.type == "ammo_box_collected")
        {
            EventGame collectedAmmoBox = new("collected_ammobox", 0);
            GameEvent.Raise(collectedAmmoBox);
        }
        if (eg.type == "score")
        {
            playerScore = (int)eg.value;
        }
    }
}
