using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyBoxCollider : MonoBehaviour
{
    private BoxCollider boxCollider;
    private bool gameOver;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        GameEvent.RegisterListener(EventListener);
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
