using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    public GameObject quitBackGround;
    public GameObject quitYesButton;
    public GameObject quitNoButton;
    public GameObject quitText;

    void OnEnable()
    {
        GameEvent.RegisterListener(EventListener);
    }
    void OnDisable()
    {
        GameEvent.UnregisterListener(EventListener);
    }

    void EventListener(EventGame eg)
    {
        if (eg.type == Constant.notQuit)
        {
            quitBackGround.SetActive(false);
            quitYesButton.SetActive(false);
            quitNoButton.SetActive(false);
            quitText.SetActive(false);
        }
        if (eg.type == Constant.quitRequest)
        {
            quitBackGround.SetActive(true);
            quitYesButton.SetActive(true);
            quitNoButton.SetActive(true);
            quitText.SetActive(true);
        }
    }
}
