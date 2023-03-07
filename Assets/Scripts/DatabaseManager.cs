using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;

public class DatabaseManager : MonoBehaviour
{
    public int[] score;
    //void Start()
    //{
    //    Load();
    //}
    public void Save()
    {
        Param param = new Param();
        param.Add("Scores", score);

        BackendReturnObject bro = Backend.GameData.Insert("Score", param);
        if (bro.IsSuccess())
        {
            // Debug.Log("»ðÀÔ ¼º°ø");
        }
    }

    public void Load()
    {
        var bro = Backend.GameData.GetMyData("Score", new Where());
        JsonData data = bro.GetReturnValuetoJSON();
        if(data.Count > 0)
        {
            JsonData list = data["rows"][0]["Scores"]["L"];
            for (int i = 0; i < list.Count; i++)
            {
                var value = list[i]["N"];
                score[i] = int.Parse(value.ToString());
            }
        }
        else
        {

        }
    }
}
