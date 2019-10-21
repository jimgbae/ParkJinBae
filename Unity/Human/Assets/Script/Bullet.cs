using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BtSpeed = 300.0f;
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Enemy")
        {
            Destroy(coll.gameObject);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * 1.0f);
        if (transform.position.y < 0)
            Destroy(gameObject);
    }
}
