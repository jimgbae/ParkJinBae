using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    
    public static T GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                    Debug.LogError("No Instance Object");

               
            }

            return instance;
        }
    }
}


