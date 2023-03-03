using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public List<GameObject> noteList = new List<GameObject>();

    [SerializeField] Transform center;
    [SerializeField] RectTransform[] timing;
    public Vector2[] timingBox;

    EffectManager effectManager;
    ScoreManager scoreManager;
    ComboManager comboManager;

    // Start is called before the first frame update
    void Start()
    {
        effectManager = FindObjectOfType<EffectManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        comboManager = FindObjectOfType<ComboManager>();

        timingBox = new Vector2[timing.Length];
        for (int i = 0; i < timing.Length; i++)
        {
            timingBox[i].Set(center.localPosition.x - timing[i].rect.width / 2, center.localPosition.x + timing[i].rect.width / 2);
        }
    }

    public bool CheckTiming()
    {
        for (int i = 0; i < noteList.Count; i++)
        {
            float noteX = noteList[i].transform.localPosition.x;
            for (int x = 0; x < timingBox.Length; x++)
            {
                if(timingBox[x].x <= noteX && noteX <= timingBox[x].y)
                {
                    // 플레이어 입력시 노트 제거
                    noteList[i].GetComponent<Note>().HideNote();
                    noteList.RemoveAt(i);

                    // 이펙트
                    if (x < timingBox.Length - 1)
                        effectManager.NoteHitEffect();
                    effectManager.JudgeEffect(x);

                    // 점수
                    scoreManager.IncreaseScore(x);
                    return true;
                }
            }
        }
        comboManager.ResetCombo();
        effectManager.JudgeEffect(timingBox.Length);
        return false;
    }

}
