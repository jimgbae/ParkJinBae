using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject Enemy;
    public bool EnemySpanwCheck = false;
    public int EnemyNumber = 5;
    public int EnemyCount = 0;
    public float SpawnMin = -74.0f;
    public float SpawnMax = 74.0f;
    

    public void SettingDifficult(int Number, float Min, float Max)
    {
        EnemyNumber = Number;
        SpawnMin = Min;
        SpawnMax = Max;
    }

    
    public void SpawnEnemy()
    {
        while(true)
        {
            float randomX = Random.Range(SpawnMin, SpawnMax);
            float randomZ = Random.Range(SpawnMin, SpawnMax);
            if (!EnemySpanwCheck && randomX != 0 && randomZ != 0)
            {
                GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefab/Enemy")) as GameObject;
                enemy.transform.position = new Vector3(randomX, 2.6f, randomZ);
                break;
            }
        }
    }
}
