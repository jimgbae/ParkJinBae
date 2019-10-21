using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public SpawnManager SpManager;

    public void ButtonClick(string type)
    {
        switch(type)
        {
            case "START":
                for (int i = 0; i < SpManager.EnemyNumber; i++)
                {
                    SpManager.SpawnEnemy();
                    SpManager.EnemyCount++;
                }
                SceneManager.LoadScene("InGame");
                break;
            case "OPTION":
                SceneManager.LoadScene("Option");
                break;
        }
    }
}
