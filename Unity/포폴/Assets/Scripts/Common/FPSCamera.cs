using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public float rotSpeed = 10.0f;

    public Camera fpsCamera;

    void Start()
    {
        
    }
    
    void Update()
    {
        RotCtrl();
    }

    void RotCtrl()
    {
        float rotX = Input.GetAxis("Mouse Y") * rotSpeed;
        float rotY = Input.GetAxis("Mouse X") * rotSpeed;

        this.transform.localRotation *= Quaternion.Euler(0, rotY, 0);
        fpsCamera.transform.localRotation *= Quaternion.Euler(-rotX, 0, 0);
    }
}
