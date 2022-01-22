using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaderCameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform chara;

    private Vector3 criterionCharaPosition;�@�@�@//��ƂȂ�L�����̈ʒu������

    private void Start()
    {
        //��ƂȂ�L�����̈ʒu
        criterionCharaPosition = chara.transform.position;
    }
    private void Update()
    {
        //----------------------------------------��]����------------------------------------------------//

        Quaternion rot = Quaternion.Slerp(transform.rotation,chara.rotation,Time.deltaTime);

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x,rot.eulerAngles.y,transform.eulerAngles.z);


        //-----------------------------------------�ړ�����----------------------------------------------//

        transform.position += chara.transform.position - criterionCharaPosition;

        //��ƂȂ�L�����̈ʒu���C��
        criterionCharaPosition = chara.transform.position;   
    }
}
