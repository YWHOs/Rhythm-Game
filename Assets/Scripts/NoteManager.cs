using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm;
    double currentTime = 0d;

    [SerializeField] Transform noteAppear;
    [SerializeField] GameObject go;

    TimeManager timeManager;

    void Start()
    {
        timeManager = GetComponent<TimeManager>();
    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 60d / bpm)
        {
            if(go != null)
            {
                GameObject note = Instantiate(go, noteAppear.position, Quaternion.identity);
                note.transform.SetParent(this.transform);
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
            if(timeManager != null)
                timeManager.noteList.Remove(_collision.gameObject);
            Destroy(_collision.gameObject);
        }
    }
}
