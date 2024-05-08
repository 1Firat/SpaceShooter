using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Difficulty globalDifficulty;
    public float enemySpeed;

    public void SetDifficulty(Difficulty globalDifficulty)
    {
        this.globalDifficulty = globalDifficulty;
    }

    void Update()
    {
        enemySpeed = globalDifficulty.enemySpeed;
        transform.Translate(Vector3.back * globalDifficulty.enemySpeed * Time.deltaTime);
    }
}
