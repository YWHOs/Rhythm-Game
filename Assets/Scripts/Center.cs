using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    bool isStart;

    public string BGMName;
    public void ResetMusic()
    {
        isStart = false;
    }
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (!isStart)
        {
            if (_collision.CompareTag("Note"))
            {
                AudioManager.instance.PlayBGM(BGMName);
                isStart = true;
            }
        }

    }
}
