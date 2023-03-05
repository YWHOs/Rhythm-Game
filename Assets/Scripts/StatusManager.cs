using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    bool isBlink;
    bool isDead;
    public bool IsDead() { return isDead; }
    int maxHP = 3;
    int currentHP;

    int maxShield = 3;
    int currentShield;
    int shieldByCombo = 5;
    int currentShieldCombo;
    [SerializeField] UnityEngine.UI.Image shiledGauge;

    [SerializeField] GameObject[] hpImage;
    [SerializeField] GameObject[] shieldImage;

    [SerializeField] float blinkSpeed = 0.1f;
    [SerializeField] int blinkCount = 10;
    int currentBlinkCount;

    Result result;
    NoteManager noteManager;
    [SerializeField] MeshRenderer mesh;

    private void Start()
    {
        currentHP = maxHP;
        result = FindObjectOfType<Result>();
        noteManager = FindObjectOfType<NoteManager>();
    }
    public void DecreaseHP(int _num)
    {
        if (!isBlink)
        {
            if (currentShield > 0)
                DecreaseShield(_num);
            else
            {
                currentHP -= _num;
                if (currentHP <= 0)
                {
                    result.ShowResult();
                    noteManager.RemoveNote();
                }
                else
                    StartCoroutine(BlinkCoroutine());
                SettingHPImage();
            }

        }
    }
    public void IncreaseHP(int _num)
    {
        currentHP += _num;
        if (currentHP >= maxHP)
            currentHP = maxHP;
        SettingHPImage();
    }
    void SettingHPImage()
    {
        for (int i = 0; i < hpImage.Length; i++)
        {
            if (i < currentHP)
            {
                hpImage[i].SetActive(true);
            }
            else
                hpImage[i].SetActive(false);
        }
    }
    public void CheckShield()
    {
        currentShieldCombo++;
        if(currentShieldCombo >= shieldByCombo)
        {
            currentShieldCombo = 0;
            IncreaseShield();
        }
        shiledGauge.fillAmount = (float)currentShieldCombo / shieldByCombo;
    }
    public void ResetShieldCombo()
    {
        currentShieldCombo = 0;
        shiledGauge.fillAmount = (float)currentShieldCombo / shieldByCombo;
    }
    public void IncreaseShield()
    {
        currentShield++;
        if (currentShield >= maxShield)
            currentShield = maxShield;
        SettingShieldImage();
    }
    public void DecreaseShield(int _num)
    {
        currentShield -= _num;
        if (currentShield <= 0)
            currentShield = 0;
        SettingShieldImage();
    }
    void SettingShieldImage()
    {
        for (int i = 0; i < shieldImage.Length; i++)
        {
            if (i < currentShield)
            {
                shieldImage[i].SetActive(true);
            }
            else
                shieldImage[i].SetActive(false);
        }
    }
    IEnumerator BlinkCoroutine()
    {
        isBlink = true;
        while(currentBlinkCount <= blinkCount)
        {
            mesh.enabled = !mesh.enabled;
            yield return new WaitForSeconds(blinkSpeed);
            currentBlinkCount++;
        }
        mesh.enabled = true;
        currentBlinkCount = 0;
        isBlink = false;
    }
}
