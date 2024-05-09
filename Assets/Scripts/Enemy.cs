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
        if (other.gameObject != null && other.gameObject.CompareTag("DestroyEnemy"))
        {
            Destroy(gameObject);
            EventGame gameScore = new("enemy_died",0);
            GameEvent.Raise(gameScore);
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("ölmedim yaşıyom haha");
            Destroy(gameObject);
            EventGame gameScore = new("enemy_exploded", 0);
            GameEvent.Raise(gameScore);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            EventGame playerHit = new("player_hit", 0);
            GameEvent.Raise(playerHit);
        }
    }
}
