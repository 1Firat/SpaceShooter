using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    private float time;
    private float ammoBoxEffectTime;
    private float ammoBoxEffectMaxTime = 5f;
    private float pauseTime = 3.0f;

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
    }

    void Start()
    {
        maxAmmo = DifficultySelect.selected.startAmmo;
        collectedAmmoBoxMaxAmmo = DifficultySelect.selected.ammoBoxMaxAmmo;
        time = DifficultySelect.selected.time;
        GameEvent.RegisterListener(EventListener);
    }

    private void Update()
    {
        if (!gameOver && !gamePaused && Input.GetKeyDown(KeyCode.Escape))
        {
            EventGame gamePause = new(Constant.pauseGame, 0, 0);
            GameEvent.Raise(gamePause);
            gamePaused = true;
        }
        if (resumeGame)
        {
            pauseTime -= Time.deltaTime;
            EventGame pausedTime = new(Constant.gamePauseTime, pauseTime, 0);
            GameEvent.Raise(pausedTime);
            if (pauseTime <= 1)
            {
                EventGame gameResume = new(Constant.resumeGame, 0, 0);
                GameEvent.Raise(gameResume);
                pauseTime = 3.0f;
                gamePaused = false;
                resumeGame = false;
            }
        }
        // GAME TIME
        if (gamePaused)
        {
            return;
        }
        if (!gameOver)
        {
            time -= Time.deltaTime;
            EventGame gameTime = new(Constant.gameTimer, time, 0);
            GameEvent.Raise(gameTime);
            if (time <= 0)
            {
                timeControl = true;
            }
        }


        if (time <= 0)
        {
            gameOver = true;
            EventGame timeIsUp = new(Constant.gameTimeIsUP, 0, 0);
            GameEvent.Raise(timeIsUp);
        }

        // END GAME

        if (gameOver && score >= DifficultySelect.selected.winScore && timeControl)
        {
            winControl = true;
            EventGame gameStatus = new(Constant.playerWin, 0, 0);
            GameEvent.Raise(gameStatus);
        }
        if (gameOver)
        {
            EventGame gameOver = new(Constant.endGame, 0, 0);
            GameEvent.Raise(gameOver);
        }
        if (winControl)
        {
            fireWork.gameObject.SetActive(true);
            fireWork.Play();
        }

        // AMMO BOX 

        if (ammoBoxControl)
        {
            ammoBoxEffectTime += Time.deltaTime;
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
        if (ammoBoxEffectTime >= ammoBoxEffectMaxTime)
        {
            EventGame ammoBoxDecrease = new(Constant.ammoBoxDecreased, 0, 0);
            GameEvent.Raise(ammoBoxDecrease);
            ammoBoxDeBuffEffect.gameObject.SetActive(true);
            ammoBoxDeBuffEffect.Play();
            StartCoroutine(DestroyAfterEffect());
            ammoBoxControl = false;
            ammoBoxEffectTime = 0;
        }
        if (!ammoBoxControl)
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
