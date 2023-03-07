using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
public class Login : MonoBehaviour
{
    [SerializeField] InputField id;
    [SerializeField] InputField pw;

    DatabaseManager DBM;
    // Start is called before the first frame update
    void Start()
    {
        DBM = FindObjectOfType<DatabaseManager>();
        Backend.Initialize(true);
    }

    public void BtnRegist()
    {
        string _id = id.text;
        string _pw = pw.text;

        BackendReturnObject bro = Backend.BMember.CustomSignUp(_id, _pw, "Rythm Game");

        if (bro.IsSuccess())
        {
            this.gameObject.SetActive(false);
        }
    }

    public void BtnLogin()
    {
        string _id = id.text;
        string _pw = pw.text;

        BackendReturnObject bro = Backend.BMember.CustomLogin(_id, _pw);

        if (bro.IsSuccess())
        {
            DBM.Load();
            this.gameObject.SetActive(false);
        }
    }
}
