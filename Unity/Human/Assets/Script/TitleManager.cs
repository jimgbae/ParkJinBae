using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public GameManager GMManager;

    void Start()
    {
        GMManager = GameManager.GetInstance;
    }


    public void ButtonClick(string type)
    {
        switch(type)
        {
            case "START":
                GMManager.GameStart = true;
                SceneManager.LoadScene("InGame");
                break;
            case "OPTION":
                SceneManager.LoadScene("Option");
                break;
        }
    }
}
