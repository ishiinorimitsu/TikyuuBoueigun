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

    private float cameraRotateSpeed = 200f;  //�ǂꂭ�炢�̃X�s�[�h�ŃJ��������]���邩

    private float limit = 300.0f;

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

        if (Input.GetMouseButton(1))
        {
            x = Input.GetAxis("Mouse X");  //a�����͂��ꂽ�Ƃ�-1,d�����͂��ꂽ�Ƃ�1��Ԃ��B

            //Debug.Log("OK!");

            //�Ώۂ̃I�u�W�F�N�g�̎�����񂷁B�����́A�i���̎�����܂�邩�A�ǂ̕����ɉ�邩�A�ǂ̂��炢�̃X�s�[�h�ŉ�邩�j
            //transform.RotateAround(targetPos, Vector3.up, x * Time.deltaTime * cameraRotateSpeed);

           // Debug.Log("OK2");
        
            z = Input.GetAxis("Mouse Y");

            //Debug.Log("OK3");

            //transform.RotateAround(targetPos, transform.right, z * Time.deltaTime * cameraRotateSpeed);

            //Debug.Log("OK4");

            //x���̈ړ��͈͂̐ݒ�
            float maxLimit = limit;
            float minLimit = 360 - maxLimit;

            //�J�����̉�]���̏����l���Z�b�g
            var localAngle = transform.localEulerAngles;

            //x���̉�]�����Z�b�g
            localAngle.x += z;

            // X �����ғ��͈͓��Ɏ��܂�悤�ɐ���
            if (localAngle.x > maxLimit && localAngle.x < 180)
            {
                localAngle.x = maxLimit;
            }
            if (localAngle.x < minLimit && localAngle.x > 180)
            {
                localAngle.x = minLimit;
            }

            //Y���̉�]����ݒ�
            localAngle.y += x;

            // �J�����̉�]
            transform.localEulerAngles = localAngle;
        }
    }
}
