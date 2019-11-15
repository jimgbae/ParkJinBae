using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;                        //추적 대상
    public float moveDamping = 15.0f;               //이동 속도
    public float rotateDamping = 5.0f;              //회전 속도
    public float dist = 5.0f;                      //추적 대상과의 거리
    public float height = 5.0f;                     //추적 대상과의 높이
    public float targetOffset = 2.0f;               //추적 좌표 오프셋

    [Header("Wall Obstacle Setting")]
    public float heightAboveWall = 7.0f;
    public float colliderRadius = 1.8f;
    public float overDamping = 5.0f;
    private float originHeight;

    [Header("Etc Obstacle Setting")]
    public float heightAboveObstacle = 12.0f;
    public float castOffset = 1.0f;


    private Transform tr;

    void Start()
    {
        target = GameManager.instance.Player.transform;
        //CameraManager의 Transform 컴포넌트 추출
        tr = GetComponent<Transform>();
        //최초 카메라 높이 저장
        originHeight = height;
    }

    void LateUpdate()
    {
        //카메라 높이와 거리 계산
        var camPos = target.position - (target.forward * dist) + (target.up * height);
        //이동할때 속도 적용
        tr.position = Vector3.Slerp(tr.position, camPos, Time.deltaTime * moveDamping);
        //회전할때 속도 적용
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);
        //카메라 추적 대상으로 Z축 회전
        tr.LookAt(target.position + (target.up * targetOffset));
    }
    
    void Update()
    {
        if(Physics.CheckSphere(tr.position, colliderRadius))
        {
            height = Mathf.Lerp(height, heightAboveWall, Time.deltaTime * overDamping);
        }
        else
        {
            height = Mathf.Lerp(height, originHeight, Time.deltaTime * overDamping);
        }

        Vector3 castTarget = target.position + (target.up * castOffset);
        Vector3 castDir = (castTarget - tr.position).normalized;
        RaycastHit hit;

        if(Physics.Raycast(tr.position, castDir, out hit, Mathf.Infinity))
        {
            if(!hit.collider.CompareTag("PLAYER"))
            {
                height = Mathf.Lerp(height, heightAboveObstacle, Time.deltaTime * overDamping);
            }
            else
            {
                height = Mathf.Lerp(height, originHeight, Time.deltaTime * overDamping);
            }
        }
    }
}
