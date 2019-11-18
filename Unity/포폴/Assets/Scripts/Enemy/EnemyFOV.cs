using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    //Enemy의 추적 사정거리 범위와 시야각
    public float viewRange = 20.0f;
    [Range(0, 360)]
    public float viewAngle = 180.0f;

    private Transform enemyTr;
    private Transform playerTr;
    private int playerLayer;
    private int obstacleLayer;
    private int layerMask;

    void Start()
    {
        enemyTr = GetComponent<Transform>();
        playerTr = GameManager.instance.Player.transform;

        playerLayer = LayerMask.NameToLayer("PLAYER");
        obstacleLayer = LayerMask.NameToLayer("OBSTACLE");
        layerMask = 1 << playerLayer | 1 << obstacleLayer;
    }

    public Vector3 CirclePoint(float angle)
    {
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public bool isTracePlayer()
    {
        bool isTrace = false;

        //추적 반경 범위 안에서 Player추출
        Collider[] colls = Physics.OverlapSphere(enemyTr.position, viewRange, 1 << playerLayer);

        //배열 개수가 1일 때 범위안에 있다고 판단
        if(colls.Length == 1)
        {
            //Enemy와 Player 사이 방향 벡터 계산
            Vector3 dir = (playerTr.position - enemyTr.position).normalized;

            //Enemy의 시야각에 들어왔는지 판단
            if (Vector3.Angle(enemyTr.forward, dir) < viewAngle * 0.5f)
            {
                isTrace = true;
            }
        }

        return isTrace;
    }


    public bool isViewPlayer()
    {
        bool isView = false;
        RaycastHit hit;

        //Enemy와 Player사이의 방향 벡터 계산
        Vector3 dir = (playerTr.position - enemyTr.position).normalized;

        //레이캐스트를 투사해 장애물 여부 있는지 판단
        if(Physics.Raycast(enemyTr.position, dir, out hit, viewRange, layerMask))
        {
            isView = (hit.collider.CompareTag("PLAYER"));
        }
        return isView;
    }
}
