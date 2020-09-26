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
        OnOffTextCG(false);
    }

    public void OnBackBtn()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnOffKeyGuide()
    {
        if (ChatGuideCG.alpha == 1.0f)
            OnKeyGuideCG(false);
        else
            OnKeyGuideCG(true);
    }

    public void OnOffTextCG()
    {
        if (ChatGuideCG.alpha == 1.0f)
            OnKeyGuideCG(false);
        else
            OnKeyGuideCG(true);
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
