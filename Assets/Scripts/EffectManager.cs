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
        if(animatorJudge != null)
            animatorJudge.SetTrigger(judge);
    }
    public void NoteHitEffect()
    {
        if(animatorHit != null)
            animatorHit.SetTrigger(hit);
    }

}
