using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public SpawnManager spManager;
    public CameraMove Camera = null;
    public GameObject Player;

    const int Default_EnemyNumber = 5;
    const float Default_SpawnMin = -74.0f;
    const float Default_SpawnMax = 74.0f;
    public int EnemyNumber;
    public float SpawnMin;
    public float SpawnMax;
    public bool GameStart = false;
    public bool GameSetting = false;

    public void SettingDifficult(int Number, float Min, float Max)
    {
        EnemyNumber = Number;
        SpawnMin = Min;
        SpawnMax = Max;
    }

    public void SettingPlayer()
    {
        Player = (GameObject) Instantiate(Resources.Load("Prefab/Body")) as GameObject;
        Player.transform.position = new Vector3(0, 2.6f, 0);
        if(Camera == null)
        {
            Camera = GameObject.Find("PlayerCamera").GetComponent<CameraMove>();
            Camera.SetTarget(Player.transform);
        }
    }
    
    void Update()
    {
        if (GameStart)
        {
            if (Player == null)
            {
                SettingPlayer();
                spManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
                spManager.SettingDifficult(EnemyNumber, SpawnMin, SpawnMax);
                spManager.Spawn();
            }
        }
    }
}
