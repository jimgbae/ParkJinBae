using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public CanvasGroup CreditCG;

    void Start()
    {
        OnCreditOpen(false);
    }

    public void OnClickStartBtn()
    {
        SceneManager.LoadScene("SceneLoader");
    }

    public void OnClickOptionBtn()
    {
        SceneManager.LoadScene("Option");
    }

    public void OnCreditOpen(bool isOpened)
    {
        CreditCG.alpha = (isOpened) ? 1.0f : 0.0f;
        CreditCG.interactable = isOpened;
        CreditCG.blocksRaycasts = isOpened;
    }
    
}
