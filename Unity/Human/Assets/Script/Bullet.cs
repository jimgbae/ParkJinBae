using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string poolItemName = "Bullet";
    public float BtSpeed = 300.0f;
    public float lifeTime = 3.0f;
    public float _elqpsedTime = 0f;
    public ObjectPool obPool;

    public void obSetting()
    {
        obPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
    }

    void Update()
    {
        transform.position += transform.forward * BtSpeed * Time.deltaTime;

        if(GetTimer() > lifeTime)
        {
            SetTimer();
            obPool.PushToPool(poolItemName, gameObject);
        }
    }

    float GetTimer()
    {
        return (_elqpsedTime += Time.deltaTime);
    }

    void SetTimer()
    {
        _elqpsedTime = 0f;
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Enemy")
        {
            Destroy(coll.gameObject);
        }
    }

}
