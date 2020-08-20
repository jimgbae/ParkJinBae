using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform Body;
    public float Dist = 10f;
    public float Height = 5f;
    public float dampRotate = 5f;

    private Transform tr;

    void Start()
    {
       tr = GetComponent<Transform>();
    }
    
    public void SetTarget(Transform trans)
    {
        Body = trans;
    }

    void Update()
    {
        float currYAngle = Mathf.LerpAngle(tr.eulerAngles.y, Body.eulerAngles.y, dampRotate * Time.deltaTime);

        Quaternion rot = Quaternion.Euler(0, currYAngle, 0);

        tr.position = Body.position - (rot * Vector3.forward * Dist) + (Vector3.up * Height);
        tr.LookAt(Body);
    }
}
