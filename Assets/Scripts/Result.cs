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

    int currentSong = 0;
    public void SetCurrentSong(int _song) { currentSong = _song; }

    ScoreManager scoreManager;
    ComboManager comboManager;
    TimeManager timeManager;
    DatabaseManager DBM;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        comboManager = FindObjectOfType<ComboManager>();
        timeManager = FindObjectOfType<TimeManager>();
        DBM = FindObjectOfType<DatabaseManager>();
    }

    public void ShowResult()
    {
        FindObjectOfType<Center>().ResetMusic();
        AudioManager.instance.StopBGM();
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

        if(currentScore > DBM.score[currentSong])
        {
            DBM.score[currentSong] = currentScore;
            DBM.Save();
        }

    }

    public void BtnMainMenu()
    {
        goUI.SetActive(false);
        GameManager.instance.MainMenu();
        comboManager.ResetCombo();
    }
}
