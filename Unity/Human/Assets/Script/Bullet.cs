using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BtSpeed = 300.0f;

    void Update()
    {
        float moveZ = BtSpeed * Time.deltaTime;
        transform.Translate(0, 0, moveZ);

        if (transform.position.y < 0)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Enemy")
        {
            Destroy(coll.gameObject);
            gameObject.SetActive(false);
        }
    }

}
