using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Enemy;
    public bool EnemySpanwCheck = false;
    public int EnemyNumber;

    void Start()
    {
        for (int i = 0; i < EnemyNumber; i++)
            SpawnEnemy();
    }
    
    void SpawnEnemy()
    {
        while(true)
        {
            float randomX = Random.Range(-74f, 74f);
            float randomZ = Random.Range(-74f, 74f);
            if (!EnemySpanwCheck && randomX != 0 && randomZ != 0)
            {
                GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefab/Enemy")) as GameObject;
                enemy.transform.position = new Vector3(randomX, 2.6f, randomZ);
                break;
            }
        }
    }
}
