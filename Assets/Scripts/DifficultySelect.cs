using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class DifficultySelect
{
    public static Difficulty selected;

    public static void Easy(Difficulty easy)
    {
        selected = easy;
        SceneManager.LoadScene("Game");
    }

    public static void Medium(Difficulty medium)
    {
        selected = medium;
        SceneManager.LoadScene("Game");
    }

    public static void Hard(Difficulty hard)
    {
        selected = hard;
        SceneManager.LoadScene("Game");
    }
}
