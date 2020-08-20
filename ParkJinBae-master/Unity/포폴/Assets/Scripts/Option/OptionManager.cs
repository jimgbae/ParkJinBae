using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    public CanvasGroup KeyGuideCG;

    void Start()
    {
        OnKeyGuideCG(false);
    }

    public void OnBackBtn()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnOffKeyGuide()
    {
        if (KeyGuideCG.alpha == 1.0f)
            OnKeyGuideCG(false);
        else
            OnKeyGuideCG(true);
    }
        

    void OnKeyGuideCG(bool isOpened)
    {
        KeyGuideCG.alpha = (isOpened) ? 1.0f : 0.0f;
        KeyGuideCG.interactable = isOpened;
        KeyGuideCG.blocksRaycasts = isOpened;
    }
}
