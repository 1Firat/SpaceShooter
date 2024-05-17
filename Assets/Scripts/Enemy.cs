using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public ParticleSystem enemyDiedEffect;
    public ParticleSystem playerHitEffect;
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

        if (other.gameObject.CompareTag("Bullet"))
        {
            Vector3 effectPos = transform.position;
            // var effect = (Instantiate(enemyDiedEffect, ), Quaternion.identity);
            enemyDiedEffect.gameObject.SetActive(true);
            Debug.Log("enemyDied: " + enemyDiedEffect);
            Destroy(gameObject);
            EffectCD();
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
    IEnumerator EffectCD()
    {
        yield return new WaitForSeconds(1);
    }
}
