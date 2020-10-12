using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    public CanvasGroup ChatGuideCG;
    public CanvasGroup TextCG;

    void Start()
    {
        OnKeyGuideCG(false);
    }

    public void OnBackBtn()
    {
        SceneManager.LoadScene("Main");
    }

    void CheckTextCG(bool isOpened)
    {
        TextCG.alpha = (isOpened) ? 1.0f : 0.0f;
        TextCG.interactable = isOpened;
        TextCG.blocksRaycasts = isOpened;
    }


    void OnKeyGuideCG(bool isOpened)
    {
        ChatGuideCG.alpha = (isOpened) ? 1.0f : 0.0f;
        ChatGuideCG.interactable = isOpened;
        ChatGuideCG.blocksRaycasts = isOpened;
    }
}
