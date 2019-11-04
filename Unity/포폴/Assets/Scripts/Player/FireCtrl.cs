using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct PlayerSfx
{
    public AudioClip[] fire;
    public AudioClip[] reload;
}

public class FireCtrl : MonoBehaviour
{
    public enum WEAPON
    {
        WEAPON_RIFLE = 0,
        WEAPON_SHOTGUN
    }

    public WEAPON currWeapon = WEAPON.WEAPON_RIFLE;

    //총알 프리팹, 발사 좌표
    public GameObject bullet;
    public Transform firePos;

    //탄피 파티클
    public ParticleSystem cartrige;
    //총구 파티클
    private ParticleSystem muzzleFlash;
    //AudioSource , 오디오 클립 저장 변수
    private AudioSource _audio;
    public PlayerSfx playersfx;

    //Shake클래스 저장 변수
    private Shake shake;

   
    void Start()
    {
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
        bullet = (Resources.Load("Prefabs/Bullet")) as GameObject;
        shake = GameObject.Find("CameraManager").GetComponent<Shake>();
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
        StartCoroutine(shake.ShakeCamera(0.05f,0.1f,0.25f));
        Instantiate(bullet, firePos.position, firePos.rotation);
        cartrige.Play();
        muzzleFlash.Play();
        FireSfx();
    }

    void FireSfx()
    {
        //무기 오디오 클립 가져옴
        var _sfx = playersfx.fire[(int)currWeapon];
        //사운드 발생
        _audio.PlayOneShot(_sfx, 1.0f);
    }
}
