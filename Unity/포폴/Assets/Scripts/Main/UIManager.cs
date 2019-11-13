using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public CanvasGroup CreditCG;
    public CanvasGroup StageCG;
    public int StageNumber;

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
