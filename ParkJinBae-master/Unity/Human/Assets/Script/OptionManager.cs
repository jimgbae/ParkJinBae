﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OptionManager : MonoBehaviour
{

    public GameManager GMManager;
    const int EasyNumber = 5;
    const int NormalNumber = 15;
    const int HardNumber = 30;
    const float EasyMin = -74.0f;
    const float NormalMin = -111.0f;
    const float HardMin = -148.0f;
    const float EasyMax = 74.0f;
    const float NormalMax = 111.0f;
    const float HardMax = 148.0f;

    void Start()
    {
        GMManager = GameManager.GetInstance;
    }

    public void ButtonClick(string type)
    {
        switch (type)
        {
            case "EASY":
                GMManager.SettingDifficult(EasyNumber, EasyMin, EasyMax);
                break;
            case "NORMAL":
                GMManager.SettingDifficult(NormalNumber, NormalMin, NormalMax);
                break;
            case "HARD":
                GMManager.SettingDifficult(HardNumber, HardMin, HardMax);
                break;
            case "EXIT":
                SceneManager.LoadScene("Title");
                break;
        }

    }
}