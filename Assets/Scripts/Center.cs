using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    bool isStart;


    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (!isStart)
        {
            if (_collision.CompareTag("Note"))
            {
                isStart = true;
            }
        }

    }
}
