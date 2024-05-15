using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemySpeed;

    void Update()
    {
        enemySpeed = DifficultySelect.selected.enemySpeed;
        transform.Translate(Vector3.forward * DifficultySelect.selected.enemySpeed * Time.deltaTime);
    }
}
