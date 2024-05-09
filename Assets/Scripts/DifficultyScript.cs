using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyScript : MonoBehaviour
{
    public List<Difficulty> difficulties;

    public void Easy()
    {
        Difficulty easyDifficulty = difficulties.Find(difficulty => difficulty.difficultyType == "easy");
        Debug.Log("easyDifficulty: " + easyDifficulty);
        DifficultySelect.Medium(easyDifficulty);
    }
    public void Medium()
    {
        Difficulty mediumDifficulty = difficulties.Find(difficulty => difficulty.difficultyType == "medium");
        DifficultySelect.Medium(mediumDifficulty);
    }
    public void Hard()
    {
        Difficulty hardDifficulty = difficulties.Find(difficulty => difficulty.difficultyType == "hard");
        DifficultySelect.Medium(hardDifficulty);
    }
}
