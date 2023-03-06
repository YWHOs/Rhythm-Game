using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] int increaseScore = 10;
    [SerializeField] int bonusScore = 10;
    int currentScore = 0;
    public int GetCurrentScore() { return currentScore; }
    [SerializeField] float[] weights;

    Animator anim;
    ComboManager comboManager;

    // Start is called before the first frame update
    void Start()
    {
        comboManager = FindObjectOfType<ComboManager>();
        anim = GetComponent<Animator>();
        currentScore = 0;
        if(scoreText != null)
            scoreText.text = "0";
    }
    public void Initialized()
    {
        currentScore = 0;
        scoreText.text = "0";
    }
    public void IncreaseScore(int _index)
    {
        // ÄÞº¸
        comboManager.IncreaseCombo();
        int combo = comboManager.CurrentCombo();
        int bonus = (combo / 10) * bonusScore;

        // Á¡¼ö
        int increase = increaseScore + bonus;

        increase = (int)(increase * weights[_index]);

        currentScore += increase;
        if (scoreText != null)
            scoreText.text = string.Format("{0:#,##0}", currentScore);

        anim.SetTrigger("Score");
    }


}
