using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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

    //탄창 Image UI와 남은 총알 수 Text UI
    public Image magazineImg;
    public Text magazineText;

    //최대 총알 수와 남은 총알 수
    public int maxBullet = 20;
    public int remainingBullet = 20;
    //재장전 시간
    public float reloadTime = 2.0f;
    //재장전 여부 판단 변수
    public bool isReloading = false;
    //총알 사거리 시간
    public float lifeTime = 3.0f;

    //변경할 무기 Image와 교체할 무기 Image UI
    public Sprite[] weaponIcons;
    public Image weaponImage;
    //Enemy의 레이어를 저장할 변수
    private int enemyLayer;
    //장애물 레이어를 저장할 변수
    private int obstacleLayer;
    //레이어 마스크의 비트 연산을 위한 변수
    private int layerMask;
    //자동 발사 여부 판단 변수
    public bool isFire = false;
    //다음 발사 시간 저장 변수
    private float nextFire;
    //총알 발사 간격
    public float fireRate = 0.2f;
   
    void Start()
    {
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
        bullet = (Resources.Load("Prefabs/Bullet")) as GameObject;
        enemyLayer = LayerMask.NameToLayer("ENEMY");
        obstacleLayer = LayerMask.NameToLayer("OBSTACLE");
        layerMask = 1 << obstacleLayer | 1 << enemyLayer;
        Settings();
    }

    void Settings()
    {
        magazineImg = CanvasManager.instance.magazineImg;
        magazineText = CanvasManager.instance.magazineText;
        weaponImage = CanvasManager.instance.weaponImage;
    }
    
    void Update()
    {
        Debug.DrawRay(firePos.position, firePos.forward * 20.0f, Color.green);

        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        //레이캐스트에 검출된 객체 정보 저장 변수
        RaycastHit hit;
        //레이캐스트로 적 검출 ( 광선의 발사 원점 좌표, 광선 발사 방향 백터, 검출된 객체의 정보 반환 변수, 광선 도달 거리, 검출할 레이어 )
        if (Physics.Raycast(firePos.position, transform.forward, out hit, 20.0f, layerMask))
        {

            isFire = (hit.collider.CompareTag("ENEMY"));
        }
        else
        {
            isFire = false;
        }

        //레이캐스트가 Enemy에 닿았을 시 자동 발사
        if(!isReloading && isFire && remainingBullet > 0)
        {
            if(Time.time > nextFire)
            {
                --remainingBullet;
                Fire();
                nextFire = Time.time + fireRate;
            }
        }

        if(!isReloading && Input.GetMouseButtonDown(0) && remainingBullet > 0)
        {
            --remainingBullet;
            Fire();
        }

        if (remainingBullet == 0 && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reloading());
        }

        if (!isReloading && Input.GetKeyDown(KeyCode.R) && !isFire && remainingBullet != maxBullet)
        {
            StartCoroutine(Reloading());
        }

        if (isReloading == true)
        {
            StartCoroutine(Reloading());
        }
    }

    void Fire()
    {
        var _bullet = GameManager.instance.GetBullet();
        if(_bullet != null)
        {
            _bullet.transform.position = firePos.position;
            _bullet.transform.rotation = transform.rotation;
            _bullet.SetActive(true);
        }
        //탄피 추출 파티클 실행
        cartrige.Play();
        //총구 화염 파티클 실행
        muzzleFlash.Play();
        //사운드 발생
        FireSfx();

        //재장전 이미지 속성값 지정, 남은 총알 수 갱신
        magazineImg.fillAmount = (float)remainingBullet / (float)maxBullet;
        UpdateBulletText();
    }

    void FireSfx()
    {
        //무기 오디오 클립 가져옴
        var _sfx = playersfx.fire[(int)currWeapon];
        //사운드 발생
        _audio.PlayOneShot(_sfx, 1.0f);
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        _audio.PlayOneShot(playersfx.reload[(int)currWeapon], 1.0f);

        yield return new WaitForSeconds(playersfx.reload[(int)currWeapon].length + 0.3f);

        isReloading = false;
        magazineImg.fillAmount = 1.0f;
        remainingBullet = maxBullet;
        UpdateBulletText();
    }

    void UpdateBulletText()
    {
        magazineText.text = string.Format("<color=#ff0000>{0}</color>/{1}", remainingBullet, maxBullet);
    }

    public void OnChangeWeapon()
    {
        currWeapon = (WEAPON)((int)++currWeapon % 2);
        weaponImage.sprite = weaponIcons[(int)currWeapon];
    }

    void LateUpdate()
    {
        if(GameManager.instance.isGameOver)
        {
            isReloading = false;
            remainingBullet = maxBullet;
        }
    }
}
