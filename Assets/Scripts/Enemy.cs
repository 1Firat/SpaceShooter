using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyEnemy"))
        {
            Destroy(gameObject);
            EnemyDied();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerHit();
        }

        if (other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            other.gameObject.SetActive(false);
            EnemyExploded();
        }
    }

    void EnemyDied()
    {
        EventGame enemyDie = new(Constant.enemyDied, 0);
        GameEvent.Raise(enemyDie);
    }

    void PlayerHit()
    {
        EventGame playerGetHit = new(Constant.playerHit, 0);
        GameEvent.Raise(playerGetHit);
    }

    void EnemyExploded()
    {
        EventGame enemyExplode = new(Constant.enemyExploded, 0);
        GameEvent.Raise(enemyExplode);
    }
}
