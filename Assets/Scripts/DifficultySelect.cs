using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DifficultySelect 
{
    public static Difficulty selected;
    public static List<Difficulty> difficulties;




    public static void Easy()
    {
        selected = difficulties[0];
        SceneManager.LoadScene(1);
    }

    public static void Medium()
    {
        selected = difficulties[1];
        SceneManager.LoadScene(1);
    }

    public static void Hard()
    {
        selected = difficulties[2];
        SceneManager.LoadScene(1);
    }
}
