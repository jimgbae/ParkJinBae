using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//인스펙터 뷰 노출
[System.Serializable]
public class PlayerAnim
{
    public AnimationClip idle;
    public AnimationClip runF;
    public AnimationClip runB;
    public AnimationClip runL;
    public AnimationClip runR;
    public AnimationClip Reload;
}

public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private float r = 0.0f;
    private float y = 0.0f;

    private int DEXDexterity;

    private Transform tr;
    public float moveSpeed;
    public float rotSpeed = 80.0f;

    //애니메이션 클래스 변수
    public PlayerAnim playerAnim;
    //Animation컴포넌트 저장 변수
    public Animation anim;

    public FireCtrl fireC;



    void OnEnable()
    {
        GameManager.OnItemChange += UpdateSetup;
    }

    void UpdateSetup()
    {
        moveSpeed = GameManager.instance.gameData.speed;
        DEXDexterity = GameManager.instance.gameData.Dexterity;
    }

    void Start()
    {
        fireC = GetComponent<FireCtrl>();

        tr = GetComponent<Transform>();
        //Animation 컴포넌트 변수 할당
        anim = GetComponent<Animation>();
        //Animation 컴포넌트의 애니메이션 클립 지정 실행
        anim.clip = playerAnim.idle;
        anim.Play();

        moveSpeed = GameManager.instance.gameData.speed;
    }
    
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        //이동 Vector계산
        Vector3 Move = (Vector3.forward * v) + (Vector3.right * h);

        //Translate로 (방향 * 속도 * 변위감 * Time.deltaTime, 기준좌표)
        tr.Translate(Move.normalized * moveSpeed * Time.deltaTime, Space.Self);

        //Rotate로 Vector3.up기준으로 rotSpeep만큼 속도회전
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * y);

        if(v >= 0.1f)
        {
            anim.CrossFade(playerAnim.runF.name, 0.3f);//전진 애니메이션
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade(playerAnim.runB.name, 0.3f);//후진 애니메이션
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade(playerAnim.runR.name, 0.3f);//오른쪽 애니메이션
        }
        else if (h <= -0.1f)    
        {
            anim.CrossFade(playerAnim.runL.name, 0.3f);//왼쪽 애니메이션
        }
        else
        {
            anim.CrossFade(playerAnim.idle.name, 0.3f);//정지 애니메이션
        }

        if(fireC.isReloading)
        {
            anim.CrossFade(playerAnim.Reload.name, 0.3f);
        }
    }
}
