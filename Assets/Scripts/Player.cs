using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance;

    private PlayerController playerController;
    public Difficulty globalDifficulty;

    public void SetDifficulty(Difficulty globalDifficulty)
    {
        this.globalDifficulty = globalDifficulty;
    }

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Enemy"))
        {
            EventGame playerHit = new("player_hit", 0);
            GameEvent.Raise(playerHit);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

}
