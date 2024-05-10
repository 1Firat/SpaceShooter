using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject ammoBox;
    public Transform enemySpawnPoint;
    private bool gameOver;
    private int spawnPos = 550;
    private float ammoBoxSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        GameEvent.RegisterListener(EventListener);

        ammoBoxSpawnTime = DifficultySelect.selected.ammoBoxSpawnTime;
        enemySpawnPoint = GameObject.FindWithTag("SpawnManager").transform;
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver != false)
        {
            Destroy(gameObject);
        }

        ammoBoxSpawnTime -= Time.deltaTime;

        if (ammoBoxSpawnTime <= 0)
        {
            Vector3 randomPos = new Vector3(Random.Range(-spawnPos, spawnPos), 12.8f, 250);
            GameObject a = Instantiate(ammoBox, randomPos, enemySpawnPoint.rotation);
            EnemyMovement script = a.GetComponent<EnemyMovement>();
            script.enemySpeed = DifficultySelect.selected.enemySpeed;
            ammoBoxSpawnTime = DifficultySelect.selected.ammoBoxSpawnTime;
        }

        if (gameOver != false)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies == null)
            {
                return;
            }
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            if (bullets == null)
            {
                return;
            }
            foreach (GameObject bullet in bullets)
            {
                bullet.SetActive(false);
            }
            GameObject[] ammoBoxs = GameObject.FindGameObjectsWithTag("AmmoBox");
            if (ammoBoxs == null)
            {
                return;
            }
            foreach (GameObject ammoBox in ammoBoxs)
            {
                Destroy(ammoBox);
            }
        }
        
    }

    void EventListener(EventGame eg)
    {
        if (eg.type == "player_death" || eg.type == "time_is_up")
        {
            gameOver = true;
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (!gameOver)
        {
            GameObject randomSpawnEnemy = enemy[Random.Range(0, enemy.Length)];
            Vector3 randomPos = new Vector3(Random.Range(-spawnPos, spawnPos), 32f, 800);

            GameObject o = Instantiate(randomSpawnEnemy, randomPos, enemySpawnPoint.rotation);
            EnemyMovement script = o.GetComponent<EnemyMovement>();
            script.enemySpeed = DifficultySelect.selected.enemySpeed;


            yield return new WaitForSeconds(DifficultySelect.selected.enemySpawnCD);
        }
    }
}
