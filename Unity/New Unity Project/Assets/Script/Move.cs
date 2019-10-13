using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    private Rigidbody Rigid; 

    void Start()
    {
        Rigid = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float _MoveX = Input.GetAxisRaw("Horizontal");
        float _MoveZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _MoveX;
        Vector3 _moveVectical = transform.forward * _MoveZ;

        Vector3 _velocity = (_moveHorizontal + _moveVectical).normalized * walkSpeed;

        Rigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }
}
