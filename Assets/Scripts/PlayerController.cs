using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;
    public bool gameOver;
    public bool isFire;
    private bool ammoMaxed;
    private bool gamePaused;
    private int position = 550;
    public ParticleSystem hitEffect;
    public ParticleSystem collectAmmoBoxEffect;

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
        if (eg.type == Constant.ammoMax)
        {
            ammoMaxed = true;
        }
        if (eg.type == Constant.ammoNotMax)
        {
            ammoMaxed = false;
        }
        if (eg.type == Constant.pauseGame)
        {
            gamePaused = true;
        }
        if (eg.type == Constant.resumeGame)
        {
            gamePaused = false;
        }

    }
    void Update()
    {
        if (gamePaused)
        {
            return;
        }
        if (gameOver)
        {
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-40, 90, -180), Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-130, 90, -180), Time.deltaTime * speed);
        }
        if (!Input.GetKey(KeyCode.LeftArrow) || !Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.RightArrow) || !Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90, 90, -180), Time.deltaTime * 5);
        }
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, 0f);
        if (horizontalInput == 0f)
        {
            moveDirection = Vector3.zero;
        }
        transform.Translate(moveDirection * DifficultySelect.selected.playerSpeed * Time.deltaTime, 0f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ammoMaxed)
            {
                return;
            }
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

        if (gameOver)
        {
            Destroy(gameObject);
        }
    }

    public void FireBullet()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            EventGame ammoUsed = new(Constant.useAmmo, 0);
            GameEvent.Raise(ammoUsed);
            bullet.transform.position = transform.position;
            var script = bullet.GetComponent<Bullet>();
            script.GO(DifficultySelect.selected.bulletSpeed);
            bullet.SetActive(true);
        }
    }

    void StartFire()
    {
        if (!gameOver)
        {
            isFire = true;
            StartCoroutine(FireRoutine());
        }
    }

    void StopFire()
    {
        if (!gameOver)
        {
            isFire = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AmmoBox"))
        {
            collectAmmoBoxEffect.gameObject.SetActive(true);
            collectAmmoBoxEffect.Play();
            StartCoroutine(DestroyAfterEffect("ammo_box"));
            EventGame ammoBox = new(Constant.ammoBoxCollected, 0);
            GameEvent.Raise(ammoBox);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerHit();
            hitEffect.gameObject.SetActive(true);
            hitEffect.Play();
            StartCoroutine(DestroyAfterEffect("player_hit"));
        }
    }
    void PlayerHit()
    {
        EventGame playerGetHit = new(Constant.playerHit, 0);
        GameEvent.Raise(playerGetHit);
    }

    IEnumerator FireRoutine()
    {
        while (isFire)
        {
            FireBullet();
            yield return new WaitForSeconds(DifficultySelect.selected.fireRate);
        }
    }

    private IEnumerator DestroyAfterEffect(string effectType)
    {
        if (effectType == "ammo_box")
        {
            float ammoBoxEffectDuration = collectAmmoBoxEffect.main.duration;

            yield return new WaitForSeconds(ammoBoxEffectDuration);
            collectAmmoBoxEffect.Stop();
            collectAmmoBoxEffect.gameObject.SetActive(false);
        }

        if (effectType == "player_hit")
        {
            float hitEffectDuration = hitEffect.main.duration;
            yield return new WaitForSeconds(hitEffectDuration);
            hitEffect.Stop();
            hitEffect.gameObject.SetActive(false);
        }

    }
}
