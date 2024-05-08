using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public static Difficulty selected;
    public List<Difficulty> difficulties;

    private void Awake()
    {
        Difficulty[] difficulties = Resources.FindObjectsOfTypeAll<Difficulty>();
        Debug.Log(difficulties.Length);
    }

    void Start()
    {
        
    }

    public void Easy()
    {
        selected = difficulties[0];
        SceneManager.LoadScene(1);
    }

    public void Medium()
    {
        selected = difficulties[1];
        SceneManager.LoadScene(1);
    }

    public void Hard()
    {
        selected = difficulties[2];
        SceneManager.LoadScene(1);
    }
}
