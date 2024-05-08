using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Difficulty globalDifficulty;

    public void SetDifficulty(Difficulty globalDifficulty)
    {
        this.globalDifficulty = globalDifficulty;
    }

    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public bool ammoBoxControl;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        GameEvent.RegisterListener(EventListener);
        SpawnAmmo();
    }

    private void Update()
    {
        if (ammoBoxControl != false)
        {
            globalDifficulty.amountToPool = 10;
            AmmoBoxControlCD();
            ammoBoxControl = false;
        }
    }

    public void SpawnAmmo()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < globalDifficulty.amountToPool; i++)
        {
            GameObject tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (ammoBoxControl != false)
        {
            GameObject newObj = Instantiate(objectToPool);
            newObj.SetActive(false);
            pooledObjects.Add(newObj);
            return newObj;
        }
        return null;
    }

    void EventListener(EventGame eg)
    {
        if (eg.type == "collected_ammobox")
        {
            ammoBoxControl = true;
        }
    }

    IEnumerator AmmoBoxControlCD()
    {
        yield return new WaitForSeconds(10);
    }
}
