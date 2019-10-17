using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static bool HaveBullet = false;
    public static bool PlayerLive = true; 
    public static float walkSpeed;
    public static float MoveDelta;
    public Object bt;

    private Rigidbody Rigid; 

    void Start()
    {
        Rigid = GetComponent<Rigidbody>();
        walkSpeed = 8.0f;
    }
    
    void Update()
    {
        PlayerControll();
    }

    private void PlayerControll()
    {
        MoveDelta = walkSpeed * Time.deltaTime;
        
        float _MoveX = Input.GetAxisRaw("Horizontal");
        float _MoveZ = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.right * MoveDelta * _MoveX);
        transform.Translate(Vector3.forward * MoveDelta * _MoveZ);

        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(0, -1, 0, Space.World);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(0, 1, 0, Space.World);
        if (Input.GetKey(KeyCode.Space))
        {
            if (!HaveBullet)
            {
                Instantiate(bt,)
            }
        }
    }
}
