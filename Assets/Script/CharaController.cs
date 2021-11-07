using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    //------------------------�A�j���[�V����-------------------------------------//

    [SerializeField]
    private Animator anim;

    //-------------------------�ړ��֌W---------------------------------------//

    [SerializeField]
    private float jumpForce;  //�W�����v��

    [SerializeField]
    private float moveSpeed;  //���������ւ̈ړ���

    private float x;  //x�������ւ̈ړ���

    private float z;  //z�������ւ̈ړ���

    private Rigidbody rb;  //Rigidbody�ɗ͂�������̂ŁA���������B

    //-------------------------�e�̔��ˊ֌W---------------------------------------//

    [SerializeField]
    private BulletController bulletPrefab;  //�e�̃v���t�@�u������B

    public Transform bulletStartPosition;  //�e�𐶐�����n�_�i�e���̏ꏊ�j

    [SerializeField]
    private float bulletPower = 1000;   //�ǂ̂��炢�̃p���[�Œe�����ł�����


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");   //���������̈ړ�������ꍇ�A�P����������

        z = Input.GetAxis("Vertical");  //���������̈ړ�������ꍇ�A�P����������

        if (Input.GetButtonDown("Jump"))  //�X�y�[�X�L�[���������Ƃ��Ƀ��\�b�h�����������B
        {
            anim.SetTrigger("Jump");

            Jump();
        }

        if (Input.GetButtonDown("Fire1")){

            BulletController createBullet = Instantiate(bulletPrefab, bulletStartPosition.position, bulletStartPosition.rotation);   //�e�e�𐶐�����

            createBullet.Shot(this);

            anim.SetTrigger("Shot");
        }
    }

    private void FixedUpdate()
    {
        //�ړ�����
        Move();

        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * z + Camera.main.transform.right * x;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    //---------------------------------�ړ��Ɋւ��鏈��----------------------------------------------------------//
    //���������ւ̈ړ�
    public void Move()
    {
        rb.velocity = new Vector3(x * moveSpeed,rb.velocity.y, z * moveSpeed);

        if(x!=0 || z != 0)
        {
            anim.SetBool("Idle", false);
            anim.SetFloat("Run", 0.5f);
        }
        else
        {
            anim.SetFloat("Run", 0);
            anim.SetBool("Idle", true);
        }
    }

    //�W�����v�̈ړ�
    public void Jump()
    {
        rb.AddForce(transform.up*jumpForce);
    }

    //----------------------------------�e�𔭎˂��鏈��----------------------------------------------------------//

    
}


