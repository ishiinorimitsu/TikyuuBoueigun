using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;  //�W�����v��

    [SerializeField]
    private float moveSpeed;  //���������ւ̈ړ���

    private float moveX;  //x�������ւ̈ړ���

    private float moveZ;  //z�������ւ̈ړ���

    private Rigidbody rb;  //Rigidbody�ɗ͂�������̂ŁA���������B

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;�@�@//x�������ւ̈ړ���

        moveZ = Input.GetAxis("Vertical") * moveSpeed;     //z�������ւ̈ړ���

        if (Input.GetButtonDown("Jump"))  //�X�y�[�X�L�[���������Ƃ��Ƀ��\�b�h�����������B
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    //---------------------------------�ړ��Ɋւ��鏈��----------------------------------------------------------//
    //���������ւ̈ړ�
    public void Move()
    {
        rb.velocity = new Vector3(moveX, 0, moveZ);
    }

    //�W�����v�̈ړ�
    public void Jump()
    {
        rb.AddForce(transform.up*jumpForce);
    }
}


