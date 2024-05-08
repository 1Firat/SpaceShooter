using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemySpeed;

    void Update()
    {
        enemySpeed = Singleton.selected.enemySpeed;
        transform.Translate(Vector3.back * Singleton.selected.enemySpeed * Time.deltaTime);
    }
}
