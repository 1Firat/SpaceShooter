using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip enemyExplode;
    public AudioClip playerHit;
    public AudioClip ammoBoxCollected;
    public AudioClip ammoBoxDecreased;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        GameEvent.RegisterListener(EventListener);
    }

    void OnDisable()
    {
        GameEvent.UnregisterListener(EventListener);
    }

    void EventListener(EventGame eg)
    {
        if (eg.type == Constant.enemyExploded)
        {
            audioSource.clip = enemyExplode;
            audioSource.Play();
        }
        if (eg.type == Constant.playerHit)
        {
            audioSource.clip = playerHit;
            audioSource.Play();
        }
        if (eg.type == Constant.ammoBoxCollected)
        {
            audioSource.clip = ammoBoxCollected;
            audioSource.Play();
        }
        if (eg.type == Constant.ammoBoxDecreased)
        {
            audioSource.clip = ammoBoxDecreased;
            audioSource.Play();
        }
    }
}