using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject[] goUI;
    [SerializeField] GameObject goTitle;
    public bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void GameStart()
    {
        for (int i = 0; i < goUI.Length; i++)
        {
            goUI[i].SetActive(true);
        }
        isStart = true;
    }

    public void MainMenu()
    {
        for (int i = 0; i < goUI.Length; i++)
        {
            goUI[i].SetActive(false);
        }
        goTitle.SetActive(true);
    }

}
