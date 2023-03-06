using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] Sound[] sfx;
    [SerializeField] Sound[] bgm;

    [SerializeField] AudioSource bgmAudio;
    [SerializeField] AudioSource[] sfxAudio;

    void Start()
    {
        instance = this;
    }
    public void PlayBGM(string _bgm)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if(_bgm == bgm[i].name)
            {
                bgmAudio.clip = bgm[i].clip;
                bgmAudio.Play();
            }
        }
    }
    public void StopBGM()
    {
        bgmAudio.Stop();
    }

    public void PlaySFX(string _sfx)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (_sfx == sfx[i].name)
            {
                for (int j = 0; j < sfxAudio.Length; j++)
                {
                    if (!sfxAudio[i].isPlaying)
                    {
                        sfxAudio[j].clip = sfx[i].clip;
                        sfxAudio[j].Play();
                        return;
                    }
                }
                return;
            }
        }
    }
}
