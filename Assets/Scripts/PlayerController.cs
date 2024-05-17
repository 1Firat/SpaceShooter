using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public bool gameOver;
    public bool isFire;
    private int position = 550;
    public ParticleSystem hitEffect;
    public ParticleSystem collectAmmoBoxEffect;

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
            collectAmmoBoxEffect.gameObject.SetActive(true);
            collectAmmoBoxEffect.Play();
            StartCoroutine(DestroyAfterEffect("ammo_box"));
            EventGame ammoBox = new(Constant.ammoBoxCollected, 0);
            GameEvent.Raise(ammoBox);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            hitEffect.gameObject.SetActive(true);
            hitEffect.Play();
            StartCoroutine(DestroyAfterEffect("player_hit"));
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

    private IEnumerator DestroyAfterEffect(string effectType)
    {
        if (effectType == "ammo_box")
        {
            float ammoBoxEffectDuration = collectAmmoBoxEffect.main.duration;

            yield return new WaitForSeconds(ammoBoxEffectDuration);
            Debug.Log("ammobox effecti kapatildi");
            collectAmmoBoxEffect.Stop();
            collectAmmoBoxEffect.gameObject.SetActive(false);
        }

        if (effectType == "player_hit")
        {
            float hitEffectDuration = hitEffect.main.duration;

            yield return new WaitForSeconds(hitEffectDuration);
            Debug.Log("playerhit effecti kapatildi");
            hitEffect.Stop();
            hitEffect.gameObject.SetActive(false);
        }

    }
}
