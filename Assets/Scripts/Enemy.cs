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
            EventGame enemyDied = new("enemy_died", 0);
            GameEvent.Raise(enemyDied);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            EventGame playerHit = new("player_hit", 0);
            GameEvent.Raise(playerHit);
        }

        if (other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            other.gameObject.SetActive(false);
            EventGame enemyExploded = new("enemy_exploded", 0);
            GameEvent.Raise(enemyExploded);
        }

    }
}
