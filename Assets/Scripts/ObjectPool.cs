using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public bool ammoBoxControl;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        GameEvent.RegisterListener(EventListener);
        amountToPool = DifficultySelect.selected.amountToPool;
        SpawnAmmo();
    }

    private void Update()
    {
        if (ammoBoxControl != false)
        {
            amountToPool = 100;
            SpawnAmmo();
            AmmoBoxControlCD();
            ammoBoxControl = false;
        }
        if (ammoBoxControl != true)
        {
            amountToPool = DifficultySelect.selected.amountToPool;
        }
    }

    public void SpawnAmmo()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
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
        if (eg.type == Constant.ammoBoxCollected)
        {
            ammoBoxControl = true;
        }
    }

    IEnumerator AmmoBoxControlCD()
    {
        yield return new WaitForSeconds(5);
    }
}
