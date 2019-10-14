using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public GameObject Body;
    public float offsetX;
    public float offsetY;
    public float offsetZ;

    void Start()
    {
    }
    
    void Update()
    {
        Vector3 FixedPos = new Vector3(Body.transform.position.x + offsetX, Body.transform.position.y + offsetY, Body.transform.position.z + offsetZ);
        transform.position = FixedPos;
    }
}
