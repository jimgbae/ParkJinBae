﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    //총알 데미지 및 속도
    public float damage = 20.0f;
    public float speed = 1000.0f;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }
    
    void Update()
    {
        
    }
}
