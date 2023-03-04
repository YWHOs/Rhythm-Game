using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject go;
    [SerializeField] UnityEngine.UI.Text comboText;

    int currentCombo;
    int maxCombo;
    public int GetMaxCombo() { return maxCombo; }
    public int CurrentCombo() { return currentCombo; }

    // Start is called before the first frame update
    void Start()
    {
        if (comboText != null && go != null)
        {
            comboText.gameObject.SetActive(false);
            go.SetActive(false);
        }
    }
    public void IncreaseCombo(int _num = 1)
    {
        currentCombo += _num;
        comboText.text = string.Format("{0:#,##0}", currentCombo);

        if(maxCombo < currentCombo)
        {
            maxCombo = currentCombo;
        }
        if(currentCombo > 2)
        {
            if(comboText != null && go != null)
            {
                comboText.gameObject.SetActive(true);
                go.SetActive(true);
            }
        }
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        if (comboText != null && go != null)
        {
            comboText.text = "0";
            comboText.gameObject.SetActive(false);
            go.SetActive(false);
        }
    }
}
