using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerLv : MonoBehaviour
{
    //Player 레벨
    public int Level = 1;

    //Player 경험치
    private float MaxEXP;
    public float currEXP;

    //EXP Image, Text
    public Image expBar;
    public Text ExpText;

    private readonly Color initColor = new Vector4(0, 0.0f, 0.0f, 1.0f);
    private Color currColor;


    void Awake()
    {
        MaxEXP += (Level * 20);
    }

    void Start()
    {
        //GameData에서 레벨 , 경험치 불러오기
        Level = GameManager.instance.gameData.level;
        MaxEXP += 100 + (Level * 20);
        currEXP = GameManager.instance.gameData.exp;

        expBar.color = initColor;
        currColor = initColor;
    }

    public void KillEnemy()
    {
        GameManager.instance.EnemyDieCount++;
        currEXP += 10.0f;
        GameManager.instance.IncExp();
        UpdateExpText();
    }

    void DisplayExpbar()
    {
        currColor.g = (currEXP / MaxEXP) * 2.0f;
        expBar.color = currColor;   
        expBar.fillAmount = (currEXP / MaxEXP);
    }

    //경험치 Text를 표현하는 함수
    void UpdateExpText()
    {
        ExpText.text = string.Format("<color=#ff0000>{0}</color>/{1}", currEXP, MaxEXP);
    }

    void Update()
    {
        if (currEXP >= MaxEXP)
        {
            Level += 1;
            currEXP = 0;
            MaxEXP += (Level * 20);
            UpdateExpText();
            GameManager.instance.IncLevel(Level);
        }
    }
}
