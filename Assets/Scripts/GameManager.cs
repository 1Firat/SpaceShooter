using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

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
    private int maxAmmo;
    private int collectedAmmoBoxMaxAmmo;
    private int currentAmmo;
    private float ammoBoxEffectTime;
    private float ammoBoxEffectMaxTime = 5f;
    public ParticleSystem ammoBoxDeBuffEffect;
    public ParticleSystem fireWork;
    public GameObject endGame;
    public GameObject dLight;
    public GameObject redLight;
    public GameObject greenLight;

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
        time -= Time.deltaTime;

        EventGame gameTime = new(Constant.gameTimer, time);
        GameEvent.Raise(gameTime);

        if (time <= 0)
        {
            gameOver = true;
            EventGame timeIsUp = new(Constant.gameTimeIsUP, 0);
            GameEvent.Raise(timeIsUp);
        }
        if (gameOver && score >= DifficultySelect.selected.winScore)
        {
            winControl = true;
            EventGame gameStatus = new(Constant.playerWin, 0);
            GameEvent.Raise(gameStatus);
        }
        if (gameOver)
        {
            endGame.SetActive(true);
            dLight.SetActive(false);
            redLight.SetActive(true);
        }
        if (winControl)
        {
            fireWork.gameObject.SetActive(true);
            fireWork.Play();
            endGame.SetActive(true);
            dLight.SetActive(false);
            redLight.SetActive(false);
            greenLight.SetActive(true);
        }

        if (ammoBoxControl)
        {
            ammoBoxEffectTime += Time.deltaTime;
            if (currentAmmo >= collectedAmmoBoxMaxAmmo)
            {
                EventGame ammoMaxed = new(Constant.ammoMax, 0);
                GameEvent.Raise(ammoMaxed);
            }
            if (currentAmmo <= collectedAmmoBoxMaxAmmo)
            {
                EventGame ammoNotMaxed = new(Constant.ammoNotMax, 0);
                GameEvent.Raise(ammoNotMaxed);
            }
        }
        if (ammoBoxEffectTime >= ammoBoxEffectMaxTime)
        {
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

                EventGame ammoMaxed = new(Constant.ammoMax, 0);
                GameEvent.Raise(ammoMaxed);
            }
            if (currentAmmo <= maxAmmo)
            {
                EventGame ammoNotMaxed = new(Constant.ammoNotMax, 0);
                GameEvent.Raise(ammoNotMaxed);
            }
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
    }

    private IEnumerator DestroyAfterEffect()
    {
        float effectDuration = ammoBoxDeBuffEffect.main.duration;

        yield return new WaitForSeconds(effectDuration);

        ammoBoxDeBuffEffect.Stop();
        ammoBoxDeBuffEffect.gameObject.SetActive(false);
    }
}
