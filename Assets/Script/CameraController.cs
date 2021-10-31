using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("�ǐՂ���Q�[���I�u�W�F�N�g")]
    public CharaController targetObj;

    private Vector3 targetPos;  //targetObj�̈ʒu�����[���锠

    private Camera cam;

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
    }
}
