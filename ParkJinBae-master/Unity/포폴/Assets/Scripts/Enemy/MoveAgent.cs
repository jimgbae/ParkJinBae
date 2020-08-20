using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveAgent : MonoBehaviour
{
    //순찰 지점 저장 배열
    public List<Transform> wayPoints;
    public int nextidx;
    public float speed
    {
        get { return agent.velocity.magnitude; }
    }

    private float damping = 1.0f;
    private NavMeshAgent agent;
    private Transform enemyTr;

    private readonly float patrolSpeed = 1.5f;
    private readonly float traceSpeed = 4.0f;

    //순찰 여부 판단 변수
    private bool _patrolling;
    public bool patrolling
    {
        get { return _patrolling; }
        set
        {
            _patrolling = value;
            if(_patrolling)
            {
                agent.speed = patrolSpeed;
                damping = 1.0f;
                MoveWayPoint();
            }
        }
    }

    //추적 위치 저장 변수
    private Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            damping = 6.0f;
            TraceTarget(_traceTarget);
        }
    }

    void Start()
    {

        enemyTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.updateRotation = false;

        agent.speed = patrolSpeed;


        var group = GameObject.Find("PointGroup");
        if(group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);

            nextidx = Random.Range(0, wayPoints.Count);
        }

        MoveWayPoint();
    }

    void MoveWayPoint()
    {
        if (agent.isPathStale) return;

        agent.destination = wayPoints[nextidx].position;
        agent.isStopped = false;

    }

    //추적할 때 이동 함수
    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale) return;

        agent.destination = pos;
        agent.isStopped = false;
    }

    //순찰 및 추적 정지 함수
    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        _patrolling = false;
    }
    
    void Update()
    {
        //Enemy가 이동 중일 때만 회전
        if(agent.isStopped == false)
        {
            //NavMeshAgent가 가야할 방향을 쿼터니언 각도로 변환
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            //보간 함수로 점진적 회전
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }



        if (!_patrolling) return;

        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {
            //다음 목적지의 배열 첨자를 계산
            nextidx = Random.Range(0, wayPoints.Count);

            //다음 목적지로 이동 명령 수행
            MoveWayPoint();
        }
    }
}
