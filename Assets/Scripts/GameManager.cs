using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    private float time;
    private float ammoBoxSkillTime;
    private float ammoBoxSkillMaxTime = 5f;
    private float pauseTime = 3.0f;
    private float tdt;

    public bool timeControl = false;
    public bool winControl = false;
    public bool gameOver = false;
    public bool isFire = false;
    public bool ammoBoxControl = false;
    public bool menuStatus = false;
    public bool gamePaused;
    public bool resumeGame;

    public int score;
    private int maxAmmo;
    private int collectedAmmoBoxMaxAmmo;
    private int currentAmmo;


    public ParticleSystem ammoBoxDeBuffEffect;
    public ParticleSystem fireWork;



    void OnEnable()
    {
        score = 0;
        GameEvent.RegisterListener(EventListener);
    }

    void OnDisable()
    {
        GameEvent.UnregisterListener(EventListener);
    }

    void Start()
    {
        maxAmmo = DifficultySelect.selected.startAmmo;
        collectedAmmoBoxMaxAmmo = DifficultySelect.selected.ammoBoxMaxAmmo;
        time = DifficultySelect.selected.time;
    }

    bool gameCountDown = false;

    private void Update()
    {
        tdt = Time.deltaTime;
        if (gameOver)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            bool lastState = gamePaused;
            gamePaused = !gamePaused;

            if (lastState)
            {
                // pause -> running
                gameCountDown = true;
            }
            else
            {
                // running -> pause
                EventGame gamePause = new(Constant.pauseGame, 0, 0);
                GameEvent.Raise(gamePause);
            }
        }

        if (gameCountDown)
        {
            PauseTime(tdt);
        }

        if (gamePaused)
        {
            return;
        }

        if (!gameOver)
        {
            GameTime(tdt);
        }

        if (time <= 0)
        {
            TimeControl(tdt);
        }

        if (gameOver)
        {
            EventGame gameOver = new(Constant.endGame, 0, 0);
            GameEvent.Raise(gameOver);
        }

        if (score >= DifficultySelect.selected.winScore)
        {
            WinControl();
        }

        if (winControl)
        {
            fireWork.gameObject.SetActive(true);
            fireWork.Play();
        }

        if (ammoBoxControl)
        {
            AmmoBoxControl(tdt);
        }
        if (ammoBoxSkillTime >= ammoBoxSkillMaxTime)
        {
            AmmoBoxSkillControl();
        }
        if (!ammoBoxControl)
        {
            AmmoBoxAmmoControl();
        }
    }

    void PauseTime(float td)
    {
        pauseTime -= td;
        Debug.Log(pauseTime);
        EventGame pausedTime = new(Constant.gamePauseTime, pauseTime, 0);
        GameEvent.Raise(pausedTime);
        if (pauseTime <= 1)
        {
            EventGame gameResume = new(Constant.resumeGame, 0, 0);
            GameEvent.Raise(gameResume);
            gameCountDown = false;
            pauseTime = 3.0f;
        }

    }

    void GameTime(float td)
    {
        time -= Time.deltaTime;
        EventGame gameTime = new(Constant.gameTimer, time, 0);
        GameEvent.Raise(gameTime);
        if (time <= 0)
        {
            timeControl = true;
        }
    }

    void TimeControl(float td)
    {
        gameOver = true;
        EventGame timeIsUp = new(Constant.gameTimeIsUP, 0, 0);
        GameEvent.Raise(timeIsUp);
    }

    void WinControl()
    {
        winControl = true;
        EventGame gameStatus = new(Constant.playerWin, 0, 0);
        GameEvent.Raise(gameStatus);
    }

    void AmmoBoxControl(float td)
    {
        ammoBoxSkillTime = td;
        if (currentAmmo >= collectedAmmoBoxMaxAmmo)
        {
            EventGame ammoMaxed = new(Constant.ammoMax, 0, 0);
            GameEvent.Raise(ammoMaxed);
        }
        if (currentAmmo <= collectedAmmoBoxMaxAmmo)
        {
            EventGame ammoNotMaxed = new(Constant.ammoNotMax, 0, 0);
            GameEvent.Raise(ammoNotMaxed);
        }
    }

    void AmmoBoxSkillControl()
    {
        EventGame ammoBoxDecrease = new(Constant.ammoBoxDecreased, 0, 0);
        GameEvent.Raise(ammoBoxDecrease);
        ammoBoxDeBuffEffect.gameObject.SetActive(true);
        ammoBoxDeBuffEffect.Play();
        StartCoroutine(DestroyAfterEffect());
        ammoBoxControl = false;
        ammoBoxSkillTime = 0;
    }

    void AmmoBoxAmmoControl()
    {
        if (currentAmmo >= maxAmmo)
        {

            EventGame ammoMaxed = new(Constant.ammoMax, 0, 0);
            GameEvent.Raise(ammoMaxed);
        }
        if (currentAmmo <= maxAmmo)
        {
            EventGame ammoNotMaxed = new(Constant.ammoNotMax, 0, 0);
            GameEvent.Raise(ammoNotMaxed);
        }
    }

    void EventListener(EventGame eg)
    {
        if (gameOver)
        {
            return;
        }
        if (eg.type == Constant.playerHit)
        {
            EventGame decreaseHeart = new(Constant.decreaseHeart, 0, 0);
            GameEvent.Raise(decreaseHeart);
        }
        if (eg.type == Constant.enemyExploded)
        {
            score += 100;
            EventGame gameScore = new(Constant.changeScore, score, 100);
            GameEvent.Raise(gameScore);
        }
        if (eg.type == Constant.enemyDied)
        {
            score -= 200;
            EventGame gameScore = new(Constant.changeScore, score, -200);
            GameEvent.Raise(gameScore);
        }
        if (eg.type == Constant.ammoBoxCollected)
        {
            EventGame ammoBoxCollected = new(Constant.collectedAmmoBox, 0, 0);
            GameEvent.Raise(ammoBoxCollected);
        }
        if (eg.type == Constant.playerDeath)
        {
            gameOver = true;
        }
        if (eg.type == Constant.useAmmo)
        {
            currentAmmo += 1;
        }
        if (eg.type == Constant.returnAmmo)
        {
            currentAmmo -= 1;
        }
        if (eg.type == Constant.ammoBoxCollected)
        {
            ammoBoxControl = true;
        }
        if (eg.type == Constant.playGame)
        {
            resumeGame = true;
        }

    }

    private IEnumerator DestroyAfterEffect()
    {
        float effectDuration = ammoBoxDeBuffEffect.main.duration;

        yield return new WaitForSeconds(effectDuration);

        ammoBoxDeBuffEffect.Stop();
        ammoBoxDeBuffEffect.gameObject.SetActive(false);
    }
}
