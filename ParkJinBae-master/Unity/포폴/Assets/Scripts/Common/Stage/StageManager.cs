using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int StageEnemy = 11;
    public Transform[] points;
    public Transform Playerpoints;

    void Awake()
    {
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
    }

    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.Setting();
            GameManager.instance.GameStart();
        }
        if (CanvasManager.instance != null)
            CanvasManager.instance.OnCanvas();
    }

    public void GameSetting()
    {
        GameManager.instance.PlayerSpawn = Playerpoints;
        GameManager.instance.points = points;
        GameManager.instance.maxEnemy = StageEnemy;
    }
}
