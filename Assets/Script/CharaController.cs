using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;  //ジャンプ力

    [SerializeField]
    private float moveSpeed;  //水平方向への移動力

    private float moveX;  //x軸方向への移動力

    private float moveZ;  //z軸方向への移動力

    private Rigidbody rb;  //Rigidbodyに力を加えるので、それを入れる。

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;　　//x軸方向への移動量

        moveZ = Input.GetAxis("Vertical") * moveSpeed;     //z軸方向への移動量

        if (Input.GetButtonDown("Jump"))  //スペースキーを押したときにメソッドが発動される。
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    //---------------------------------移動に関する処理----------------------------------------------------------//
    //水平方向への移動
    public void Move()
    {
        rb.velocity = new Vector3(moveX, 0, moveZ);
    }

    //ジャンプの移動
    public void Jump()
    {
        rb.AddForce(transform.up*jumpForce);
    }
}


