using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    private float speed;

    public void GO(float speed)
    {
        this.speed = speed;
    }

    private void Update()
    {
        Vector3 pos = gameObject.transform.position;
        gameObject.transform.position = new(pos.x, pos.y, pos.z + Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
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
