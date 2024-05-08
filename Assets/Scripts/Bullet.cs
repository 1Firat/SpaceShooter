using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public Difficulty globalDifficulty;

    public void SetDifficulty(Difficulty globalDifficulty)
    {
        this.globalDifficulty = globalDifficulty;
    }

    public void GO(float k)
    {
        if (k == 0)
        {
            k = 1;
        }
        var r = GetComponent<Rigidbody>();
        r.velocity = new Vector3(0, 0, 1) * globalDifficulty.bulletSpeed * k;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            bullet.SetActive(false);
        }
        if (other.gameObject.CompareTag("ReloadBullet"))
        {
            ObjectPool.SharedInstance.GetPooledObject();
            bullet.SetActive(false);
        }
        if (other.gameObject.CompareTag("AmmoBox"))
        {
            EventGame ammoBox = new("ammo_box_collected",0);
            GameEvent.Raise(ammoBox);
            Destroy(other.gameObject);
        }

    }
}
