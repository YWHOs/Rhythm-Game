using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
