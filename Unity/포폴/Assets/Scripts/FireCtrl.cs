using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    //총알 프리팹, 발사 좌표
    public GameObject bullet;
    public Transform firePos;

    //탄피 파티클
    public ParticleSystem cartrige;
    //총구 파티클
    public ParticleSystem muzzleFlash;
   
    void Start()
    {
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
    }
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
        cartrige.Play();
        muzzleFlash.Play();
    }
}
