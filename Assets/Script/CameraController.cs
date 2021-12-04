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

    private float cameraRotateSpeed = 200f;  //どれくらいのスピードでカメラが回転するか

    private float limit = 300.0f;

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

        if (Input.GetMouseButton(1))
        {
            x = Input.GetAxis("Mouse X");  //aが入力されたとき-1,dが入力されたとき1を返す。

            //Debug.Log("OK!");

            //対象のオブジェクトの周りを回す。引数は、（何の周りをまわるか、どの方向に回るか、どのくらいのスピードで回るか）
            //transform.RotateAround(targetPos, Vector3.up, x * Time.deltaTime * cameraRotateSpeed);

           // Debug.Log("OK2");
        
            z = Input.GetAxis("Mouse Y");

            //Debug.Log("OK3");

            //transform.RotateAround(targetPos, transform.right, z * Time.deltaTime * cameraRotateSpeed);

            //Debug.Log("OK4");

            //x軸の移動範囲の設定
            float maxLimit = limit;
            float minLimit = 360 - maxLimit;

            //カメラの回転情報の初期値をセット
            var localAngle = transform.localEulerAngles;

            //x軸の回転情報をセット
            localAngle.x += z;

            // X 軸を稼働範囲内に収まるように制御
            if (localAngle.x > maxLimit && localAngle.x < 180)
            {
                localAngle.x = maxLimit;
            }
            if (localAngle.x < minLimit && localAngle.x > 180)
            {
                localAngle.x = minLimit;
            }

            //Y軸の回転情報を設定
            localAngle.y += x;

            // カメラの回転
            transform.localEulerAngles = localAngle;
        }
    }
}
