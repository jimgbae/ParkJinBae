using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //CanvasGroup 저장 변수
    public CanvasGroup CreditCG;
    public CanvasGroup StageCG;
    //씬 이동 번호 저장 변수
    public int StageNumber;

    //UIManager를 Singleton으로 접근하기 위해 만든 static변수
    public static UIManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        OnCreditOpen(false);
        OnStageListOpen(false);
    }

    public void OnClickStartBtn(int Number)
    {
        StageNumber = Number;
        SceneManager.LoadScene("SceneLoader");
    }

    public void OnClickOptionBtn()
    {
        SceneManager.LoadScene("Option");
    }

    public void OnStageListOpen(bool isOpened)
    {
        StageCG.alpha = (isOpened) ? 1.0f : 0.0f;
        StageCG.interactable = isOpened;
        StageCG.blocksRaycasts = isOpened;
    }

    public void OnCreditOpen(bool isOpened)
    {
        CreditCG.alpha = (isOpened) ? 1.0f : 0.0f;
        CreditCG.interactable = isOpened;
        CreditCG.blocksRaycasts = isOpened;
    }
    
}
