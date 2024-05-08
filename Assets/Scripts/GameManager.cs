using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public Difficulty globalDifficulty;

    public void SetDifficulty(Difficulty globalDifficulty)
    {
        this.globalDifficulty = globalDifficulty;
    }

    private float time;

    public bool timeControl = false;
    public bool winControl = false;
    public bool gameOverControl = false;
    public bool isFire = false;
    public bool ammoBoxControl = false;
    public bool menuStatus = false;

    void Start()
    {
        GameEvent.RegisterListener(EventListener);
    }

    private void Update()
    {
        time = globalDifficulty.time;
        time -= Time.deltaTime;

        EventGame gameTime = new("game_time",time);
        GameEvent.Raise(gameTime);

        if (time <= 0)
        {
            EventGame gameOver = new("time_is_up", 0);
            GameEvent.Raise(gameOver);
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
    }
}
