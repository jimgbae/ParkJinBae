using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CameraMove Camera;
    public SpawnManager SpManager;
    public GameObject Player;


    //https://bluemeta.tistory.com/19?category=733470
    void Start()
    {
        Player = (GameObject)Instantiate(Resources.Load("Prefab/Body")) as GameObject;
        Player.transform.position = new Vector3(0, 2.6f, 0);
        Camera.SetTarget(Player.transform);
    }
    
    void Update()
    {
        
    }
}
