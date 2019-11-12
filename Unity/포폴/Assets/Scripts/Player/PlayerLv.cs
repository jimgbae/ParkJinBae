using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerLv : MonoBehaviour
{
    //Player 레벨
    public int Level;

    //Player 경험치
    private float MaxEXP = 100.0f;
    private float SaveEXP;
    public float currEXP;

    //Enemy 처치시 경험치
    public float KillExp = 10.0f;

    //EXP Image, Text
    public Image expBar;
    public Text ExpText;

    private readonly Color initColor = new Vector4(0.0f,1.0f,0.0f,1.0f);
    private Color currColor;

    

    void Start()
    {
        //GameData에서 레벨 , 경험치 불러오기
        SaveEXP = GameManager.instance.gameData.exp;
        MaxExpSetting();
        LvSetting();

        expBar.color = initColor;
        currColor = initColor;
    }

    void MaxExpSetting()
    {
        if(SaveEXP / MaxEXP >= 10 && SaveEXP / MaxEXP < 20)
        {
            MaxEXP = 500.0f;
        }
        else if(SaveEXP / MaxEXP >= 20 && SaveEXP / MaxEXP < 30)
        {
            MaxEXP = 1300.0f;
        }
    }

    void LvSetting()
    {
        if (SaveEXP >= MaxEXP)
        {
            Level = ((int)SaveEXP / (int)MaxEXP);
            currEXP = (SaveEXP % MaxEXP);
            UpdateExpText();
        }
    }

    public void KillEnemy()
    {
        GameManager.instance.EnemyDieCount++;
        currEXP += 10.0f;
        GameManager.instance.IncExp(KillExp);
        UpdateExpText();
    }

    void DisplayExpbar()
    {
        expBar.color = initColor;   
        expBar.fillAmount = (currEXP / MaxEXP);
    }

    //경험치 Text를 표현하는 함수
    void UpdateExpText()
    {
        ExpText.text = string.Format("<color=#ff0000>{0}</color>/{1}", currEXP, MaxEXP);
    }

    void Update()
    {
        UpdateExpText();
        if(currEXP >= MaxEXP)
        {
            Level++;
            currEXP = 0;
            UpdateExpText();
        }
    }
}
