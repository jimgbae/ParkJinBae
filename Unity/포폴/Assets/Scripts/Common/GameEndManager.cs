using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    public void OnClickLobbyBtn()
    {
        GameManager.instance.Reset();
        SceneManager.LoadScene("Main");
    }   
}
