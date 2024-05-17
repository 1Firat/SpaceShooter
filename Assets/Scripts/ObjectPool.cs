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
        amountToPool = DifficultySelect.selected.currentAmmo;
        SpawnAmmo();
    }

    private void Update()
    {
        if (ammoBoxControl != false)
        {
            int deletedListObject = DifficultySelect.selected.maxAmmo - DifficultySelect.selected.currentAmmo;
            amountToPool = 100;
            SpawnAmmo();
            AmmoBoxControlCD();
            pooledObjects.RemoveRange(0, deletedListObject);
            ammoBoxControl = false;
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
        if (eg.type == Constant.collectedAmmoBox)
        {
            Debug.Log("ammo box control true");
            ammoBoxControl = true;
        }
    }

    IEnumerator AmmoBoxControlCD()
    {
        yield return new WaitForSeconds(5);
    }
}
