using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private const string bulletTag = "BULLET";
    //HP 게이지
    private float hp = 100.0f;
    //피격시 사용 효과
    private GameObject bloodEffect;

    void Start()
    {
        //혈흔 효과 프리팹 로드
        bloodEffect = (Resources.Load("Prefabs/BulletImpactFleshBigEffect")) as GameObject;
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == bulletTag)
        {
            //혈흔 효과 생성 호출 함수
            ShowBloodEffect(coll);
            //총알 삭제
            Destroy(coll.gameObject);
            
            //HP가 damage만큼 감소
            hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;
            if(hp<=0.0f)
            {
                //Enemy상태를 DIE로 변경
                GetComponent<EnemyAI>().state = EnemyAI.STATE.STATE_DIE;
            }
        }
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
