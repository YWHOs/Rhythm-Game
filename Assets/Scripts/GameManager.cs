using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject[] goUI;
    [SerializeField] GameObject goTitle;
    public bool isStart;

    TimeManager timeManager;
    ScoreManager scoreManager;
    ComboManager comboManager;
    StatusManager statusManager;
    PlayerController player;
    StageManager stage;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        comboManager = FindObjectOfType<ComboManager>();
        timeManager = FindObjectOfType<TimeManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        statusManager = FindObjectOfType<StatusManager>();
        player = FindObjectOfType<PlayerController>();
        stage = FindObjectOfType<StageManager>();
    }

    public void GameStart()
    {
        for (int i = 0; i < goUI.Length; i++)
        {
            goUI[i].SetActive(true);
        }
        stage.RemoveStage();
        stage.StageSetting();
        comboManager.ResetCombo();
        timeManager.Initialized();
        scoreManager.Initialized();
        statusManager.Initialized();
        player.Initialized();
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
