using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public List<GameObject> noteList = new List<GameObject>();

    int[] judgeRecord = new int[4];

    [SerializeField] Transform center;
    [SerializeField] RectTransform[] timing;
    public Vector2[] timingBox;

    EffectManager effectManager;
    ScoreManager scoreManager;
    ComboManager comboManager;
    StageManager stageManager;
    PlayerController playerController;
    StatusManager statusManager;

    // Start is called before the first frame update
    void Start()
    {
        effectManager = FindObjectOfType<EffectManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        comboManager = FindObjectOfType<ComboManager>();
        stageManager = FindObjectOfType<StageManager>();
        playerController = FindObjectOfType<PlayerController>();
        statusManager = FindObjectOfType<StatusManager>();

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

                    if (CanNextPlate())
                    {
                        // 점수
                        scoreManager.IncreaseScore(x);
                        // 발판
                        stageManager.ShowNextPlate();
                        effectManager.JudgeEffect(x);
                        // 판정 기록
                        judgeRecord[x]++;
                        // 쉴드
                        statusManager.CheckShield();
                    }
                    else
                    {
                        effectManager.JudgeEffect(timingBox.Length);
                    }

                    return true;
                }
            }
        }
        comboManager.ResetCombo();
        effectManager.JudgeEffect(timingBox.Length);
        MissRecord();
        return false;
    }

    // 정확한 위치로 이동했을 때 발판 생기는지
    bool CanNextPlate()
    {
        if(Physics.Raycast(playerController.destination, Vector3.down, out RaycastHit hit, 1.1f))
        {
            if (hit.transform.CompareTag("Basic_Plate"))
            {
                BasicPlate plate = hit.transform.GetComponent<BasicPlate>();
                if (plate.flag)
                {
                    plate.flag = false;
                    return true;
                }
            }
        }
        return false;
    }

    public int[] GetJudgeRecord()
    {
        return judgeRecord;
    }
    public void MissRecord()
    {
        judgeRecord[timingBox.Length]++;
        statusManager.ResetShieldCombo();
    }
}
