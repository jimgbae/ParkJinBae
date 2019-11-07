using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    //총알 데미지 및 속도
    public float damage = 20.0f;
    public float speed = 1000.0f;

    //컴포넌트 저장 변수
    private Transform tr;
    private Rigidbody rigid;
    private TrailRenderer trail;


    void Awake()
    {
        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();

        damage = GameManager.instance.gameData.damage;
    }

    void OnEnable()
    {
        rigid.AddForce(transform.forward * speed);
        GameManager.OnItemChange += UpdateSetup;
    }

    void UpdateSetup()
    {
        damage = GameManager.instance.gameData.damage;
    }

    void OnDisable()
    {
        trail.Clear();
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rigid.Sleep();
    }
}
