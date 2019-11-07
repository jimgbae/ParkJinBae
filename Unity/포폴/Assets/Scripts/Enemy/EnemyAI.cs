using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Enemy 상태 열거형 변수 정의
    public enum STATE
    {
        STATE_PATROL = 0,
        STATE_TRACE,
        STATE_ATTACK,
        STATE_DIE
    }
    //상태 저장 변수
    public STATE state = STATE.STATE_PATROL;
    //공격, 추적 사정거리
    public float attackDist = 5.0f;
    public float traceDist = 10.0f;
    //죽음 판단 변수
    public bool isDie = false;

    //Player와 Enemy위치를 저장하는 변수
    private Transform playerTr;
    private Transform enemyTr;
    //Animator 컴포넌트 저장 변수
    private Animator animator;
    //코루틴에서 사용할 지연시간 변수
    private WaitForSeconds ws;
    //MoveAgent클래스 저장 변수
    private MoveAgent moveagent;
    //EnemyFire 클래스 저장 변수
    private EnemyFire enemyFire;
    //EnemyFOV 클래스 저장 변수
    private EnemyFOV enemyFOV;

    //파라미터 해시값 추출
    private readonly int hashMove = Animator.StringToHash("isMove");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashDieIdx = Animator.StringToHash("DieIdx");
    private readonly int hashOffset = Animator.StringToHash("Offset");
    private readonly int hashwalkSpeed = Animator.StringToHash("WalkSpeed");
    private readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");

    void Awake()
    {
        //Player GameObject 추출
        var player = GameObject.FindGameObjectWithTag("PLAYER");

        //Player와 Enemy의 Transform 추출
        if (player != null)
            playerTr = player.GetComponent<Transform>(); 
        enemyTr = GetComponent<Transform>();
        //Anumator 추출
        animator = GetComponent<Animator>();
        //MoveAgent 추출
        moveagent = GetComponent<MoveAgent>();
        //EnemyFire 추출
        enemyFire = GetComponent<EnemyFire>();
        //EnemyFOV 추출
        enemyFOV = GetComponent<EnemyFOV>();

        //코루틴 지연 시간
        ws = new WaitForSeconds(0.3f);

        //Offset값과 Speed 값을 불규칙하게 변경
        animator.SetFloat(hashOffset, Random.Range(0.0f, 1.0f));
        animator.SetFloat(hashwalkSpeed, Random.Range(1.0f, 1.2f));
    }

    void OnEnable()
    {
        StartCoroutine(CheckState());
        StartCoroutine(Action());

        Damage.OnPlayerDie += this.OnPlayerDie;
    }

    void OnDisable()
    {
        Damage.OnPlayerDie -= this.OnPlayerDie;
    }

    IEnumerator CheckState()
    {
        yield return new WaitForSeconds(1.0f);

        while (!isDie)
        {

            //상태 사망이면 코루틴 종료
            if (state == STATE.STATE_DIE) yield break;

            //Player와 Enemy의 거리 계산
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);

            //공격 및 추적 사거리 이내인 경우
            if(dist <= attackDist)
            {
                if (enemyFOV.isViewPlayer())
                    state = STATE.STATE_ATTACK; //장애물 없으면 공격
                else
                    state = STATE.STATE_TRACE;  //장애물 있으면 추적
            }//추적 반경 및 시야각에 들어왔는지 판단
            else if(enemyFOV.isTracePlayer())
            {
                state = STATE.STATE_TRACE;
            }
            else
            {
                state = STATE.STATE_PATROL;
            }


            yield return ws;
        }
    }

    IEnumerator Action()
    {
        while(!isDie)
        {
            yield return ws;
            switch (state)
            {
                case STATE.STATE_PATROL:
                    enemyFire.isFire = false;
                    moveagent.patrolling = true;
                    animator.SetBool(hashMove, true);
                    break;
                case STATE.STATE_TRACE:
                    enemyFire.isFire = false;
                    moveagent.traceTarget = playerTr.position;
                    animator.SetBool(hashMove, true);
                    break;
                case STATE.STATE_ATTACK:
                    moveagent.Stop();
                    animator.SetBool(hashMove, false);
                    if (enemyFire.isFire == false)
                        enemyFire.isFire = true;
                    break;
                case STATE.STATE_DIE:
                    this.gameObject.tag = "Untagged";
                    isDie = true;
                    enemyFire.isFire = false;
                    moveagent.Stop();
                    animator.SetInteger(hashDieIdx, Random.Range(0, 3));
                    animator.SetTrigger(hashDie);
                    GetComponent<CapsuleCollider>().enabled = false;
                    break;
            }

        }
    }

    public void OnPlayerDie()
    {
        moveagent.Stop();
        enemyFire.isFire = false;
        StopAllCoroutines();
        animator.SetTrigger(hashPlayerDie);
    }

    void Update()
    {
        animator.SetFloat(hashSpeed, moveagent.speed);
    }
}
