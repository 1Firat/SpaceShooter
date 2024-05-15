using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            other.gameObject.SetActive(false);
            EventGame enemyExploded = new(Constant.ammoBoxCollected, 0);
            GameEvent.Raise(enemyExploded);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            EventGame enemyExploded = new(Constant.ammoBoxCollected, 0);
            GameEvent.Raise(enemyExploded);
        }
        if (other.gameObject.CompareTag("DestroyEnemy"))
        {
            Destroy(gameObject);
        }
    }
}