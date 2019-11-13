using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int StageEnemy = 11;
    public Transform[] points;

    void Awake()
    {
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
    }

    void Start()
    {
    }

    public void GameSetting()
    {
        GameManager.instance.points = points;
        GameManager.instance.maxEnemy = StageEnemy;
        GameManager.instance.Setting();
    }
}
