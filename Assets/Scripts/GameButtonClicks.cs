using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonClicks : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void RestartGame()
    {
        GameEvent.Release();
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    public void QuitRequest()
    {
        EventGame quitRequest = new(Constant.quitRequest, 0, 0);
        GameEvent.Raise(quitRequest);
    }
    public void CancelQuit()
    {
        EventGame cancelQuit = new(Constant.notQuit, 0, 0);
        GameEvent.Raise(cancelQuit);
    }
    public void ResumeGame()
    {
        EventGame resumed = new(Constant.playGame, 0, 0);
        GameEvent.Raise(resumed);
    }
}
