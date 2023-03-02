using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] int increaseScore = 10;
    int currentScore = 0;
    [SerializeField] float[] weights;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentScore = 0;
        scoreText.text = "0";
    }

    public void IncreaseScore(int _index)
    {
        int increase = increaseScore;

        increase = (int)(increase * weights[_index]);

        currentScore += increase;
        scoreText.text = string.Format("{0:#,##0}", currentScore);

        anim.SetTrigger("Score");
    }
}
