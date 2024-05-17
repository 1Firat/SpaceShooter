using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public ParticleSystem enemyDiedEffect;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            enemyDiedEffect.gameObject.SetActive(true);
            enemyDiedEffect.Play();
            StartCoroutine(DestroyAfterEffect());
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            EventGame collectedAmmoBox = new(Constant.ammoBoxCollected, 0);
            GameEvent.Raise(collectedAmmoBox);
        }
        if (other.gameObject.CompareTag("DestroyEnemy"))
        {
            Destroy(gameObject);
        }
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