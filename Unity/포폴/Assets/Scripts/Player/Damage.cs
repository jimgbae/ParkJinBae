using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private const string bulletTag = "BULLET";
    private const string enemyTag = "ENEMY";
    private float initHp = 100.0f;
    private int CONstitution;
    public float currHp;
    //BloodScreen 텍스처 저장 변수
    public Image bloodScreen;

    public Image hpBar;

    private readonly Color initColor = new Vector4(0, 1.0f, 0.0f, 1.0f);
    private Color currColor;

    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;

    public Text HPText;



    void OnEnable()
    {
        GameManager.OnItemChange += UpdateSetup;
    }

    void UpdateSetup()
    {
        initHp = GameManager.instance.gameData.hp;
        currHp = GameManager.instance.gameData.hp - currHp;
    }

    void Start()
    {
        HPText = CanvasManager.instance.HP;
        hpBar = CanvasManager.instance.hpBar;
        bloodScreen = CanvasManager.instance.bloodScreen;

        initHp = GameManager.instance.gameData.hp;
        currHp = initHp;

        hpBar.color = initColor;
        currColor = initColor;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == bulletTag)
        {
            //총알 삭제
            coll.gameObject.SetActive(false);

            StartCoroutine(ShowBloodScreen());

            currHp -= 5.0f;
            Debug.Log("Player HP = " + currHp.ToString());

            DisplayHpbar();

            if(currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    IEnumerator ShowBloodScreen()
    {
        bloodScreen.color = new Color(1, 0, 0, Random.Range(0.2f, 0.3f));
        yield return new WaitForSeconds(0.1f);
        bloodScreen.color = Color.clear;
    }

    void PlayerDie()
    {
        OnPlayerDie();
        currHp = initHp;
        GameManager.instance.PlayerDie();
        GameManager.instance.isGameOver = true;
    }

    void DisplayHpbar()
    {
        if ((currHp / initHp) > 0.5f)
            currColor.r = (1 - (currHp / initHp)) * 2.0f;
        else
            currColor.g = (currHp / initHp) * 2.0f;

        hpBar.color = currColor;
        hpBar.fillAmount = (currHp / initHp);
    }

    void LateUpdate()
    {
        if (GameManager.instance.isResetPlayer)
        {
            currHp = initHp;
        }
    }

    void Update()
    {
        UpdateHPText();
    }

    void UpdateHPText()
    {
        HPText.text = string.Format("{0}", initHp);
    }
}
