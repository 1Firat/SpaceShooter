using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemySpeed;
    public bool gamePause;

    void Start()
    {
        GameEvent.RegisterListener(EventListener);
    }
    void Update()
    {
        if (gamePause)
        {
            return;
        }
        enemySpeed = DifficultySelect.selected.enemySpeed;
        transform.Translate(Vector3.forward * DifficultySelect.selected.enemySpeed * Time.deltaTime);
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
