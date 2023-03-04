using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlate : MonoBehaviour
{
    AudioSource audioSource;
    NoteManager note;

    Result result;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        note = FindObjectOfType<NoteManager>();
        result = FindObjectOfType<Result>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            PlayerController.canPress = false;
            note.RemoveNote();
            result.ShowResult();
        }
    }
}
