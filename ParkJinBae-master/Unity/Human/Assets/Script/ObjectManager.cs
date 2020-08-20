using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    List<GameObject> bullet = new List<GameObject>();

    private void Awake()
    {
        if(ObjectManager.instance == null)
        {
            ObjectManager.instance = this;
        }
    }

    void Start()
    {
        CreateBullet(20);
    }

    void CreateBullet(int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullets = Instantiate(Resources.Load("Prefab/Bullet")) as GameObject;
            bullets.transform.parent = transform;
            bullets.SetActive(false);

            bullet.Add(bullets);
        }
    }

    public GameObject GetBullet(Vector3 pos)
    {
        GameObject reqBullet = null;
        for(int i = 0; i < bullet.Count;i++)
        {
            if(bullet[i].activeSelf == false)
            {
                reqBullet = bullet[i];

                break;
            }
        }

        reqBullet.SetActive(true);
        reqBullet.transform.position = pos;

        return reqBullet;
    }


    void Update()
    {
        
    }
}
