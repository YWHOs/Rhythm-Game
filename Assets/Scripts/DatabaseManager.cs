using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public int[] score;

    void Start()
    {
        Load();
    }
    public void Save()
    {
        PlayerPrefs.SetInt("Score1", score[0]);
        PlayerPrefs.SetInt("Score2", score[1]);
        PlayerPrefs.SetInt("Score3", score[2]);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Score1"))
        {
            score[0] = PlayerPrefs.GetInt("Score1");
            score[1] = PlayerPrefs.GetInt("Score2");
            score[2] = PlayerPrefs.GetInt("Score3");
        }
    }
}
