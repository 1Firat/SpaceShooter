using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public ParticleSystem ammoBoxDiedEffect;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            ammoBoxDiedEffect.gameObject.SetActive(true);
            ammoBoxDiedEffect.Play();
            StartCoroutine(DestroyAfterEffect());
            EventGame ammoReturned = new(Constant.returnAmmo, 0);
            GameEvent.Raise(ammoReturned);
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
        float effectDuration = ammoBoxDiedEffect.main.duration;

        yield return new WaitForSeconds(0.07f);

        ammoBoxDiedEffect.Stop();
        ammoBoxDiedEffect.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}