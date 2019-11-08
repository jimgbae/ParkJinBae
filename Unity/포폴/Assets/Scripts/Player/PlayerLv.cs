using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerLv : MonoBehaviour
{
    //Player 레벨
    private int Level = 1;

    //Player 경험치
    private float MaxEXP = 100.0f;
    private float currEXP;

    //EXP Image, Text
    public Image expBar;
    public Text ExpText;

    private readonly Color initColor = new Vector4(0, 0.0f, 0.0f, 1.0f);
    private Color currColor;


    void Start()
    {
        Level = GameManager.instance.gameData.level;
        MaxEXP += (Level * 20);
        currEXP = GameManager.instance.gameData.exp;

        expBar.color = initColor;
        currColor = initColor;
    }
    
    void Update()
    {
    }

    public void KillEnemy()
    {
        currEXP += 10.0f;
        UpdateExpText();
        if (currEXP >= MaxEXP)
        {
            Level += 1;
            currEXP = 0;
            MaxEXP += (Level * 20);
        }
    }

    void DisplayExpbar()
    {
        currColor.g = (currEXP / MaxEXP) * 2.0f;
        expBar.color = currColor;   
        expBar.fillAmount = (currEXP / MaxEXP);
    }

    void UpdateExpText()
    {
        ExpText.text = string.Format("<color=#ff0000>{0}</color>/{1}", currEXP, MaxEXP);
    }
}
