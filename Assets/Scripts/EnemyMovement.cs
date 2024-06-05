using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemySpeed;
    public bool gamePause;
    private float tdt;

    void OnEnable()
    {
        GameEvent.RegisterListener(EventListener);
    }

    void OnDisable()
    {
        GameEvent.UnregisterListener(EventListener);
    }

    void Update()
    {
        tdt = Time.deltaTime;
        if (gamePause)
        {
            return;
        }
        enemySpeed = DifficultySelect.selected.enemySpeed;
        transform.Translate(Vector3.forward * DifficultySelect.selected.enemySpeed * tdt);
    }

    void EventListener(EventGame eg)
    {
        if (eg.type == Constant.pauseGame)
        {
            gamePause = true;
        }
        if (eg.type == Constant.resumeGame)
        {
            gamePause = false;
        }
    }
}
