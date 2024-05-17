using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public bool gameOver;
    public bool isFire;
    private int position = 550;

    // Start is called before the first frame update
    void Start()
    {
        GameEvent.RegisterListener(EventListener);
    }

    void EventListener(EventGame eg)
    {
        if (eg.type == Constant.gameTimeIsUP || eg.type == Constant.playerDeath)
        {
            gameOver = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, 0f);
        if (horizontalInput == 0f)
        {
            moveDirection = Vector3.zero;
        }
        transform.Translate(moveDirection * DifficultySelect.selected.playerSpeed * Time.deltaTime, 0f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartFire();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopFire();
        }

        if (transform.position.x < -position)
        {
            transform.position = new Vector3(-position, transform.position.y, transform.position.z);
        }

        if (transform.position.x > position)
        {
            transform.position = new Vector3(position, transform.position.y, transform.position.z);
        }

        if (gameOver != false)
        {
            Destroy(gameObject);
        }
    }

    public void FireBullet()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            var script = bullet.GetComponent<Bullet>();
            script.GO(DifficultySelect.selected.bulletSpeed);
            bullet.SetActive(true);
        }
    }

    void StartFire()
    {
        if (gameOver != true)
        {
            isFire = true;
            StartCoroutine(FireRoutine());
        }
    }

    void StopFire()
    {
        if (gameOver != true)
        {
            isFire = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AmmoBox"))
        {
            EventGame ammoBox = new(Constant.ammoBoxCollected, 0);
            GameEvent.Raise(ammoBox);
        }
    }

    IEnumerator FireRoutine()
    {
        while (isFire)
        {
            FireBullet();
            yield return new WaitForSeconds(DifficultySelect.selected.fireRate);
        }
    }
}
