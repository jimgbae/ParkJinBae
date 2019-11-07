using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    //Enemy의 AudioSource, Animator, Transform 컴포넌트를 저장할 변수와 Player Transform을 저장할 변수
    private AudioSource _audio;
    private Animator _animator;
    private Transform enemyTr;
    private Transform playerTr;
    //애니메이션 컨트롤러에 정의한 파라미터의 해시값 추출
    private readonly int hashFire = Animator.StringToHash("Fire");
    private readonly int hashRelaod = Animator.StringToHash("Reload");
    //발사 시간 계산용 변수
    private float nextFire = 0.0f;
    //총알 발사 간격 , Player를 향해 회전할 속도 계수
    private readonly float fireRate = 0.1f;
    private readonly float damping = 10.0f;
    //총알 발사 판단 변수와 사운드 저장 변수
    public bool isFire = false;
    public AudioClip fireSfx;
    public AudioClip reloadSfx;

    public GameObject Bullet;
    public Transform firePos;
    public MeshRenderer muzzleFlash;

    //재장전 시간, 탄창 최대 총알 수, 초기 총알 수, 재장전 여부, 재장전 동안 기다리는 변수
    private readonly float reloadTime = 2.0f;
    private readonly int maxBullet = 10;
    private int currBullet = 10;
    private bool isReload = false;
    private WaitForSeconds wsReload;

    [Header("Object Pool")]
    //총알 Prefab
    public GameObject bulletPrefab;
    //오브젝트 풀에 생성할 개수
    public int maxPool = 10;
    public List<GameObject> BulletPool = new List<GameObject>();

    public GameObject GetBullets()
    {
        for (int i = 0; i < BulletPool.Count; i++)
        {
            //총알 비활성화 여부 판단
            if (BulletPool[i].activeSelf == false)
            {
                return BulletPool[i];
            }
        }
        return null;
    }

    //오브젝트 풀에 총알 생성 함수
    public void CreatePooling()
    {
        GameObject objectPools = new GameObject("ObjectPools");

        //풀링 개수만큼 미리 총알 생성
        for (int i = 0; i < maxPool; i++)
        {
            var obj = Instantiate<GameObject>(bulletPrefab, objectPools.transform);
            obj.name = "Bullet" + i.ToString("00");
            obj.SetActive(false);
            //리스트에 총알 추가
            BulletPool.Add(obj);
        }
    }

    void Start()
    {
        //컴포넌트 추출 및 저장
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();

        Bullet = (Resources.Load("Prefabs/E_Bullet")) as GameObject;

        wsReload = new WaitForSeconds(reloadTime);
        muzzleFlash.enabled = false;
    }
    
    void Update()
    {
        if (!isReload && isFire)
        {
            if(Time.time >= nextFire)
            {
                Fire();
                //다음 발사 시간 계산
                nextFire = Time.time + fireRate + Random.Range(0.0f, 0.3f);
            }
            //Player가 있는 위치까지 회전 각도 계산
            Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
            //보간 함수로 점진적으로 회전
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }
    }

    void Fire()
    {
        _animator.SetTrigger(hashFire);
        _audio.PlayOneShot(fireSfx, 1.0f);
        StartCoroutine(ShowMuzzleFlash());

        GameObject _bullet = Instantiate(Bullet, firePos.position, firePos.rotation);
        Destroy(_bullet, 3.0f);
        isReload = (--currBullet % maxBullet == 0);
        if(isReload)
        {
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        muzzleFlash.enabled = false;
        _animator.SetTrigger(hashRelaod);
        _audio.PlayOneShot(reloadSfx, 1.0f);

        yield return wsReload;

        currBullet = maxBullet;
        isReload = false;
    }

    IEnumerator ShowMuzzleFlash()
    {
        //MuzzleFlash 활성화
        muzzleFlash.enabled = true;

        //회전 각도 계산
        Quaternion rot = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
        //MuzzleFlash를 Z축 방향 회전
        muzzleFlash.transform.localRotation = rot;
        //MuzzleFlash의 스케일 불규칙 조정
        muzzleFlash.transform.localScale = Vector3.one * Random.Range(1.0f, 2.0f);

        //텍스터 Offset속성에 불규칙값 생성
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        //MuzzleFlash의 Material에 Offset값 적용
        muzzleFlash.material.SetTextureOffset("_MainTex", offset);

        //MuzzleFlash가 보일 동안 대기
        yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        //MuzzleFlash 비활성화
        muzzleFlash.enabled = false;
    }
}
