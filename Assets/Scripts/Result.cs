using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject goUI;
    [SerializeField] Text[] textCounts;
    [SerializeField] Text textCoin;
    [SerializeField] Text textScore;
    [SerializeField] Text textMaxCombo;

    ScoreManager scoreManager;
    ComboManager comboManager;
    TimeManager timeManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        comboManager = FindObjectOfType<ComboManager>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    public void ShowResult()
    {
        goUI.SetActive(true);
        for (int i = 0; i < textCounts.Length; i++)
        {
            textCounts[i].text = "0";
        }
        textCoin.text = "0";
        textScore.text = "0";
        textMaxCombo.text = "0";

        int[] judge = timeManager.GetJudgeRecord();
        int currentScore = scoreManager.GetCurrentScore();
        int maxCombo = comboManager.GetMaxCombo();
        int coin = currentScore / 50;

        for (int i = 0; i < textCounts.Length; i++)
        {
            textCounts[i].text = string.Format("{0:#,##0}", judge[i]);
        }
        textScore.text = string.Format("{0:#,##0}", currentScore);
        textMaxCombo.text = string.Format("{0:#,##0}", maxCombo);
        textCoin.text = string.Format("{0:#,##0}", coin);
    }
}
