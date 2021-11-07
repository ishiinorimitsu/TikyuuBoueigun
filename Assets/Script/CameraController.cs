using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("�ǐՂ���Q�[���I�u�W�F�N�g")]
    public CharaController targetObj;

    private Vector3 targetPos;  //targetObj�̈ʒu�����[���锠

    private Camera cam;

    private float x;       //�������ւ̃J�����̉�]����

    private float z;       //�������ւ̃J�����̉�]����

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();      //�J�������擾

        targetPos = targetObj.transform.position;�@�@�@//targetObj�̈ʒu�����擾
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += targetObj.transform.position - targetPos;  //�^�[�Q�b�g���ړ������������J�������ړ�����(-targetPos�����闝�R�́A��������Ȃ��Ƃ��Ƃ��Ǝ����Ă���targetpos�̍��W���v���X����A�u�ړ����v�ɂȂ�Ȃ�����B)

        targetPos = targetObj.transform.position;

        if (Input.GetButton("CameraHorizontal"))
        {
            x = Input.GetAxis("CameraHorizontal");  //a�����͂��ꂽ�Ƃ�-1,d�����͂��ꂽ�Ƃ���Ԃ��B

            Debug.Log("OK!");

            //�Ώۂ̃I�u�W�F�N�g�̎�����񂷁B�����́A�i���̎�����܂�邩�A�ǂ̕����ɉ�邩�A�ǂ̂��炢�̃X�s�[�h�ŉ�邩�j
            transform.RotateAround(targetPos, Vector3.up, x * Time.deltaTime * 200f);      

            Debug.Log("OK2");
        }
        if (Input.GetButton("CameraVertical"))
        {
            z = Input.GetAxis("CameraVertical");

            Debug.Log("OK3");

            transform.RotateAround(targetPos, transform.right, z * Time.deltaTime * 200f);

            Debug.Log("OK4");
        }
    }
}
