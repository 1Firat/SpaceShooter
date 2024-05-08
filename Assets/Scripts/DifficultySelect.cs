using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelect : MonoBehaviour
{
    public void Easy()
    {
        EventGame gameDifficulty = new("easy", 0);
        GameEvent.Raise(gameDifficulty);

    }

    public void Medium()
    {
        EventGame gameDifficulty = new("medium", 0);
        GameEvent.Raise(gameDifficulty);
    }

    public void Hard()
    {
        EventGame gameDifficulty = new("hard", 0);
        GameEvent.Raise(gameDifficulty);
    }

}
