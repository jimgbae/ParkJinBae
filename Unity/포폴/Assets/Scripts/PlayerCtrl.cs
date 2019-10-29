using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private Transform tr;
    public float moveSpeed = 10.0f;

    void Start()
    {
        tr = GetComponent<Transform>();
    }
    
    void Update()
    {
        h = Input.GetAxis("Horiziontal");
        v = Input.GetAxis("Vertical");

        Vector3 Move = (Vector3.forward * v) + (Vector3.right * h);

        tr.Translate(Move.normalized * moveSpeed * Time.deltaTime, Space.Self);
    }
}
