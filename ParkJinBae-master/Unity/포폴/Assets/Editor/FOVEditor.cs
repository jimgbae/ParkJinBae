using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyFOV))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyFOV fov = (EnemyFOV)target;

        //원주 위의 시작점의 좌표 계산 (주어진 각도의 절반)
        Vector3 fromAnglePos = fov.CirclePoint(-fov.viewAngle * 0.5f);

        //원의 색상을 흰색으로 지정
        Handles.color = Color.white;

        //외각선만 표현하는 원반을 그림(원점 좌표, 노멀 벡터, 원의 반지름)
        Handles.DrawWireDisc(fov.transform.position, Vector3.up, fov.viewRange);

        //부채꼴 색상 지정
        Handles.DrawSolidArc(fov.transform.position, Vector3.up, fromAnglePos, fov.viewAngle, fov.viewRange);

        Handles.Label(fov.transform.position + (fov.transform.forward * 2.0f), fov.viewAngle.ToString());
    }
}
