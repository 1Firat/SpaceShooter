using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    private AudioSource[] audioSources;
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        GameEvent.RegisterListener(EventListener);
    }

    void EventListener(EventGame eg)
    {
        if (eg.type == Constant.enemyExploded)
        {
            audioSources[0].Play();
        }
        if (eg.type == Constant.playerHit)
        {
            audioSources[1].Play();
        }
        if (eg.type == Constant.ammoBoxCollected)
        {
            audioSources[2].Play();
        }
        if (eg.type == Constant.ammoBoxDecreased)
        {
            audioSources[3].Play();
        }
    }
}
