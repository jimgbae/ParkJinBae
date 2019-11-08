using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    void Start()
    {
        
    }

    public void OnBackBtn()
    {
        SceneManager.LoadScene("Main");
    }
    
    void Update()
    {
        
    }
}
