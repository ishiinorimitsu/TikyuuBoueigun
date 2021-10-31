using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("追跡するゲームオブジェクト")]
    public CharaController targetObj;

    private Vector3 targetPos;  //targetObjの位置を収納する箱

    private Camera cam;

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
    }
}
