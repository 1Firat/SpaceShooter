using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public ParticleSystem enemyDiedEffect;

    private void Start()
    {

    }
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
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            enemyDiedEffect.gameObject.SetActive(true);
            enemyDiedEffect.Play();
            StartCoroutine(DestroyAfterEffect());
            other.gameObject.SetActive(false);
            EnemyExploded();
        }
    }

    void EnemyDied()
    {
        EventGame enemyDie = new(Constant.enemyDied, 0);
        GameEvent.Raise(enemyDie);
    }



    void EnemyExploded()
    {
        EventGame enemyExplode = new(Constant.enemyExploded, 0);
        GameEvent.Raise(enemyExplode);
    }

    private IEnumerator DestroyAfterEffect()
    {
        float effectDuration = enemyDiedEffect.main.duration;

        yield return new WaitForSeconds(0.07f);

        enemyDiedEffect.Stop();
        enemyDiedEffect.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
