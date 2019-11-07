using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance = null;

    [Header("Enemy Create Info")]
    //Enemy 프리팹 저장 변수
    public GameObject enemyPrefab;
    //Enemy 생성 주기 변수
    public float createTime = 2.0f;
    //최대 Enemy 개수
    public int maxEnemy = 10;
    public List<GameObject> EnemyPool = new List<GameObject>();


    //Enemy 스폰지역 Prefab
    public GameObject SpawnPrefab;
    //Max Object Pools
    public int maxSpawnPool = 10;
    public List<GameObject> SpawnPool = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        enemyPrefab = (Resources.Load("Prefabs/Enemy")) as GameObject;
        CreateEnemyPooling();
    }

    public void CreateEnemyPooling()
    {
        GameObject EnemyObjectPool = new GameObject("EnemyObjectPools");
        for(int i = 0; i < maxEnemy; i++)
        {
            var obj = Instantiate<GameObject>(enemyPrefab, EnemyObjectPool.transform);
            obj.name = "Enemy" + i.ToString("00");
            obj.SetActive(false);
            EnemyPool.Add(obj);
        }
    }

    void Start()
    {
        /*if (points.Length > 0)
        {
            StartCoroutine(this.CreateEnemy());
        }*/
    }
    
    void Update()
    {
        
    }

    IEnumerator CreateEnemy()
    {
        while (!isGameOver)
        {
            int enemyCount = (int)GameObject.FindGameObjectsWithTag("ENEMY").Length;


            if (enemyCount < maxEnemy)
            {
                yield return new WaitForSeconds(createTime);

                //int idx = Random.Range(1, points.Length);
                //Instantiate(enemy, points[idx].position, points[idx].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }



    //오브젝트 풀에 스폰위치 생성 함수
    /*public void CreateSpawnPooling()
    {
        GameObject objectSpawnPools = new GameObject("ObejctSpawnPools");

        for(int i = 0; i < maxSpawnPool; i++)
        {
            var obj = Instantiate<GameObject>(SpawnPrefab, objectSpawnPools.transform);
            obj.name = "SpawnPoint" + i.ToString("00");
            obj.SetActive(false);
            //리스트에 추가
            SpawnPool.Add(obj);
        }
    }*/
}
