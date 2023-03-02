using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm;
    double currentTime = 0d;

    [SerializeField] Transform noteAppear;

    TimeManager timeManager;
    EffectManager effectManager;

    void Start()
    {
        timeManager = GetComponent<TimeManager>();
        effectManager = FindObjectOfType<EffectManager>();
    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 60d / bpm)
        {
            GameObject note = ObjectPooling.instance.note.Dequeue();
            note.transform.position = noteAppear.position;
            note.SetActive(true);
            if (timeManager != null)
                timeManager.noteList.Add(note);
            currentTime -= 60d / bpm;
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Note"))
        {
            if(_collision.GetComponent<Note>().GetNoteFlag())
                effectManager.JudgeEffect(timeManager.timingBox.Length);
            if(timeManager != null)
                timeManager.noteList.Remove(_collision.gameObject);

            ObjectPooling.instance.note.Enqueue(_collision.gameObject);
            _collision.gameObject.SetActive(false);
        }
    }
}
