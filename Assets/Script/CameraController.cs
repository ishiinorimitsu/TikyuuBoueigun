using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("追跡するゲームオブジェクト")]
    public CharaController targetObj;

    private Vector3 targetPos;  //targetObjの位置を収納する箱

    private Camera cam;

    private float x;       //横方向へのカメラの回転方向

    private float z;       //横方向へのカメラの回転方向

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();      //カメラを取得

        targetPos = targetObj.transform.position;　　　//targetObjの位置情報を取得
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += targetObj.transform.position - targetPos;  //ターゲットが移動した分だけカメラも移動する(-targetPosをする理由は、これをしないともともと持っているtargetposの座標もプラスされ、「移動分」にならないから。)

        targetPos = targetObj.transform.position;

        if (Input.GetButton("CameraHorizontal"))
        {
            x = Input.GetAxis("CameraHorizontal");  //aが入力されたとき-1,dが入力されたときを返す。

            Debug.Log("OK!");

            //対象のオブジェクトの周りを回す。引数は、（何の周りをまわるか、どの方向に回るか、どのくらいのスピードで回るか）
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
