using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] GameObject go;

    public void BtnPlay()
    {
        go.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
