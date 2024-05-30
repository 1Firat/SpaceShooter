using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyBoxCollider : MonoBehaviour
{
    private BoxCollider boxCollider;
    private bool gameOver;

    void OnEnable()
    {
        GameEvent.RegisterListener(EventListener);
    }

    void OnDisable()
    {
        GameEvent.UnregisterListener(EventListener);
    }
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    void Update()
    {
        if (gameOver)
        {
            Destroy(boxCollider);
        }
    }
    void EventListener(EventGame eg)
    {
        if (eg.type == Constant.gameTimeIsUP || eg.type == Constant.playerDeath)
        {
            gameOver = true;
        }
    }
}
