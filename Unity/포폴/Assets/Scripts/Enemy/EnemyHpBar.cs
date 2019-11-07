using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    //Canvas 렌더링하는 카메라
    private Camera uiCamera;
    //UI용 최상위 캔버스
    private Canvas _canvas;
    //부모,자신 RectTransform 컴포넌트
    private RectTransform rectParent;
    private RectTransform rectHp;

    //HPBar Image위치 조절 offset
    [HideInInspector] public Vector3 offset = Vector3.zero;
    //추적 대상 Transform 컴포넌트
    [HideInInspector] public Transform targetTr;

    void Start()
    {
        //컴포넌트 추출 및 할당
        _canvas = GetComponentInParent<Canvas>();
        uiCamera = _canvas.worldCamera;
        rectParent = _canvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        //월드 좌표 스크린 좌표로 변환
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);
        //카메라의 뒷쪽 영역(180도 회전)일 때 좌푯값 보정
        if(screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }
        //RectTransform 값을 전달 받는 변수
        var localPos = Vector2.zero;
        //스크린 좌표를 RectTransform기준 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
        //생명 게이지 이미지 위치 변경
        rectHp.localPosition = localPos;
    }
    
    void Update()
    {
        
    }
}
