using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator animatorHit;
    string hit = "Hit";

    [SerializeField] Animator animatorJudge;
    [SerializeField] UnityEngine.UI.Image judgeImage;
    [SerializeField] Sprite[] judgeSprites;
    string judge = "Judge";


    public void JudgeEffect(int _num)
    {
        judgeImage.sprite = judgeSprites[_num];
        animatorJudge.SetTrigger(judge);
    }
    public void NoteHitEffect()
    {
        animatorHit.SetTrigger(hit);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
