using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public EnemyAI enemyAI;
    private const string bulletTag = "BULLET";
    //HP 게이지
    private float hp = 100.0f;
    //초기 생명 수치
    private float initHp = 100.0f;

    //피격시 사용 효과
    private GameObject bloodEffect;

    //생명 게이지 프리팹 저장 변수
    public GameObject hpBarPrefab;
    //생명 게이지 위치 보정 오프셋
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    //부모 Canvas
    private Canvas uiCanvas;
    //HPbar 수치에 따라 속성 변경 Image
    private Image hpBarImage;

    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();

        //혈흔 효과 프리팹 로드
        bloodEffect = (Resources.Load("Prefabs/BulletImpactFleshBigEffect")) as GameObject;
        SetHpBar();
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == bulletTag)
        {
            //혈흔 효과 생성 호출 함수
            ShowBloodEffect(coll);
            //총알 삭제
            coll.gameObject.SetActive(false);
            
            //HP가 damage만큼 감소
            hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;
            //HpBar 감소
            hpBarImage.fillAmount = hp / initHp;
            if(hp<=0.0f)
            {
                //Enemy상태를 DIE로 변경
                GetComponent<EnemyAI>().state = EnemyAI.STATE.STATE_DIE;
                //Enemy 사망 이후 HpBar 투명 처리
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
                //Enemy 사망 횟수 누적 함수 호출
                GameManager.instance.IncKillCount();
                //Capsul Collider 컴포넌트 비활성화
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }

    void SetHpBar()
    {
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        //UI Canvas 하위로 생명 게이지 생성
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        //속성 변경 Image 추출
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        //생명게이지 따라가야할 대상과 오프셋 값 설정
        var _hpBar = hpBar.GetComponent<EnemyHpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;
    }

    void ShowBloodEffect(Collision coll)
    {
        //총알 충돌 지점 산출
        Vector3 pos = coll.contacts[0].point;
        //총알 충돌 법선 벡터
        Vector3 _normal = coll.contacts[0].normal;
        //총알 충돌시 방향 벡터 회전값 계산
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);

        //혈흔 효과 생성
        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot);
        Destroy(blood, 1.0f);
    }
}
