using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    //-------------------------�ړ��֌W---------------------------------------//

    [SerializeField]
    private float jumpForce;  //�W�����v��

    [SerializeField]
    private float moveSpeed;  //���������ւ̈ړ���

    private float moveX;  //x�������ւ̈ړ���

    private float moveZ;  //z�������ւ̈ړ���

    private Rigidbody rb;  //Rigidbody�ɗ͂�������̂ŁA���������B

    //-------------------------�e�̔��ˊ֌W---------------------------------------//

    [SerializeField]
    private BulletController bulletPrefab;  //�e�̃v���t�@�u������B

    [SerializeField]
    private Transform bulletStartPosition;  //�e�𐶐�����n�_�i�e���̏ꏊ�j

    private float bulletPower = 10;   //�ǂ̂��炢�̃p���[�Œe�����ł�����




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

        if (Input.GetButtonDown("Fire1")){
            Shot();

            Debug.Log("ok");
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
        rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ);
    }

    //�W�����v�̈ړ�
    public void Jump()
    {
        rb.AddForce(transform.up*jumpForce);
    }

    //----------------------------------�e�𔭎˂��鏈��----------------------------------------------------------//

    private void Shot()
    {
        BulletController createBullet = Instantiate(bulletPrefab,bulletStartPosition.position,Quaternion.identity);   //�e�e�𐶐�����

        createBullet.GetComponent<Rigidbody>().AddForce(createBullet.transform.forward * bulletPower*100);      //�O���ɔ��˂���
    }
}


