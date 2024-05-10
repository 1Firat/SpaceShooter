using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            other.gameObject.SetActive(false);
            EventGame enemyExploded = new("ammo_box_collected", 0);
            GameEvent.Raise(enemyExploded);
        }

    }
}
