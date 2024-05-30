using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using Unity.VisualScripting;
using System;

#if UNITY_EDITOR
[CustomEditor(typeof(Difficulty))]
public class CustomDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Difficulty data = (Difficulty)target;

        // Start recording changes for undo functionality
        //Undo.RecordObject(data, "Modified Difficulty Values");

        // Display and edit sliders
        data.enemySpeed = EditorGUILayout.Slider("Enemy Speed", data.enemySpeed, 0f, 2000f);
        data.playerSpeed = EditorGUILayout.Slider("Player Speed", data.playerSpeed, 0f, 2000f);
        data.bulletSpeed = EditorGUILayout.Slider("Bullet Speed", data.bulletSpeed, 0f, 2000f);

        // Mark the object as dirty to ensure changes are saved
        if (GUI.changed)
        {
            EditorUtility.SetDirty(data);
        }

        // Draw the default inspector below the sliders
        DrawDefaultInspector();
    }
}
#endif

[CreateAssetMenu(fileName = "New Difficulty Type", menuName = "Difficulty Type")]
public class Difficulty : ScriptableObject
{
    public DifficultyType difficultyType;
    [HideInInspector]
    public float enemySpeed;
    [HideInInspector]
    public float playerSpeed;
    [HideInInspector]
    public float bulletSpeed;
    public float enemySpawnCD;
    public float ammoBoxSpawnTime;
    public float time;
    public float fireRate;
    [HideInInspector]
    public int m_Points;
    public int winScore;
    [HideInInspector]
    public int score;
    public int maxAmmo;
    public int startAmmo;
    public int ammoBoxMaxAmmo;
    public int life;
}

[Serializable]
public enum DifficultyType
{
    Easy,
    Medium,
    Hard
}