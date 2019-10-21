using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform Target;
    public static float MoveSpeed;
    public static float MoveDelta;

    private Transform tr;
    private Rigidbody rigid;



    void Start()
    {
        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        MoveSpeed = 3.0f;
    }
    
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Bullet")
        {
            Destroy(coll.gameObject);
        }
    }


    private void MoveEnemy()
    {
        Target = GameObject.FindWithTag("Player").transform;
        MoveDelta = MoveSpeed * Time.deltaTime;
        transform.Rotate(Target.position.x, Target.position.y, Target.position.z);
        transform.Translate(Vector3.forward * MoveDelta);
        tr.LookAt(Target);
    }

    void Update()
    {
        MoveEnemy();
    }
}
