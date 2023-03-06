using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMenu : MonoBehaviour
{
    [SerializeField] GameObject Title;

    public void BtnBack()
    {
        Title.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void BtnPlay()
    {
        GameManager.instance.GameStart();
        this.gameObject.SetActive(false);
    }
}
