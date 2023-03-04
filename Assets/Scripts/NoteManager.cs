using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm;
    double currentTime = 0d;

    bool isNoteActive = true;

    [SerializeField] Transform noteAppear;

    TimeManager timeManager;
    EffectManager effectManager;
    ComboManager comboManager;

    void Start()
    {
        comboManager = FindObjectOfType<ComboManager>();
        timeManager = GetComponent<TimeManager>();
        effectManager = FindObjectOfType<EffectManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isNoteActive)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 60d / bpm)
            {
                GameObject note = ObjectPooling.instance.note.Dequeue();
                note.transform.position = noteAppear.position;
                note.SetActive(true);
                if (timeManager != null)
                    timeManager.noteList.Add(note);
                currentTime -= 60d / bpm;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Note"))
        {
            if (_collision.GetComponent<Note>().GetNoteFlag())
            {
                timeManager.MissRecord();
                effectManager.JudgeEffect(timeManager.timingBox.Length);
                comboManager.ResetCombo();
            }
            if(timeManager != null)
                timeManager.noteList.Remove(_collision.gameObject);

            ObjectPooling.instance.note.Enqueue(_collision.gameObject);
            _collision.gameObject.SetActive(false);
        }
    }

    public void RemoveNote()
    {
        isNoteActive = false;
        for (int i = 0; i < timeManager.noteList.Count; i++)
        {
            timeManager.noteList[i].SetActive(false);
            ObjectPooling.instance.note.Enqueue(timeManager.noteList[i]);
        }
    }
}
