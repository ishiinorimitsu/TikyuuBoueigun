using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaderCameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform chara;

    private Vector3 criterionCharaPosition;　　　//基準となるキャラの位置を入れる

    private void Start()
    {
        //基準となるキャラの位置
        criterionCharaPosition = chara.transform.position;
    }
    private void Update()
    {
        //----------------------------------------回転方向------------------------------------------------//

        Quaternion rot = Quaternion.Slerp(transform.rotation,chara.rotation,Time.deltaTime);

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x,rot.eulerAngles.y,transform.eulerAngles.z);


        //-----------------------------------------移動方向----------------------------------------------//

        transform.position += chara.transform.position - criterionCharaPosition;

        //基準となるキャラの位置を修正
        criterionCharaPosition = chara.transform.position;   
    }
}
