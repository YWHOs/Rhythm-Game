using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public List<GameObject> noteList = new List<GameObject>();

    [SerializeField] Transform center;
    [SerializeField] RectTransform[] timing;
    Vector2[] timingBox;
    // Start is called before the first frame update
    void Start()
    {
        timingBox = new Vector2[timing.Length];
        for (int i = 0; i < timing.Length; i++)
        {
            timingBox[i].Set(center.localPosition.x - timing[i].rect.width / 2, center.localPosition.x + timing[i].rect.width / 2);
        }
    }

    public void CheckTiming()
    {
        for (int i = 0; i < noteList.Count; i++)
        {
            float noteX = noteList[i].transform.localPosition.x;
            for (int x = 0; x < timingBox.Length; x++)
            {
                if(timingBox[x].x <= noteX && noteX <= timingBox[x].y)
                {
                    noteList[i].GetComponent<Note>().HideNote();
                    noteList.RemoveAt(i);
                    return;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
