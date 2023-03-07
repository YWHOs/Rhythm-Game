using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Song
{
    public string name;
    public string composer;
    public int bpm;
    public Sprite sprite;
}
public class StageMenu : MonoBehaviour
{
    [SerializeField] Song[] songs;
    [SerializeField] Text textName;
    [SerializeField] Text textComposer;
    [SerializeField] Image imgDisk;

    [SerializeField] GameObject Title;

    int currentSong;

    void Start()
    {
        SetSong();
    }

    public void BtnNext()
    {
        AudioManager.instance.PlaySFX("Touch");
        if(++currentSong > songs.Length - 1)
        {
            currentSong = 0;
        }
        SetSong();
    }
    public void BtnPrior()
    {
        AudioManager.instance.PlaySFX("Touch");
        if (--currentSong < 0)
        {
            currentSong = songs.Length - 1;
        }
        SetSong();
    }
    void SetSong()
    {
        textName.text = songs[currentSong].name;
        textComposer.text = songs[currentSong].composer;
        imgDisk.sprite = songs[currentSong].sprite;

        AudioManager.instance.PlayBGM("BGM" + currentSong);
    }
    public void BtnBack()
    {
        Title.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void BtnPlay()
    {
        int bpm = songs[currentSong].bpm;

        GameManager.instance.GameStart(currentSong, bpm);
        this.gameObject.SetActive(false);
    }
}
