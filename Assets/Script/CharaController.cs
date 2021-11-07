using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    //------------------------アニメーション-------------------------------------//

    [SerializeField]
    private Animator anim;

    //-------------------------移動関係---------------------------------------//

    [SerializeField]
    private float jumpForce;  //ジャンプ力

    [SerializeField]
    private float moveSpeed;  //水平方向への移動力

    private float moveX;  //x軸方向への移動力

    private float moveZ;  //z軸方向への移動力

    private Rigidbody rb;  //Rigidbodyに力を加えるので、それを入れる。

    //-------------------------銃の発射関係---------------------------------------//

    [SerializeField]
    private BulletController bulletPrefab;  //弾のプレファブを入れる。

    [SerializeField]
    private Transform bulletStartPosition;  //弾を生成する地点（銃口の場所）

    [SerializeField]
    private float bulletPower = 1000;   //どのくらいのパワーで弾が飛んでいくか


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();
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

        if (Input.GetButtonDown("Fire1")){
            Shot();

            Debug.Log("ok");
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
        rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ);

        //anim.Play();
    }

    //ジャンプの移動
    public void Jump()
    {
        rb.AddForce(transform.up*jumpForce);
    }

    //----------------------------------弾を発射する処理----------------------------------------------------------//

    private void Shot()
    {
        BulletController createBullet = Instantiate(bulletPrefab,bulletStartPosition.position,bulletStartPosition.rotation);   //銃弾を生成する

        createBullet.GetComponent<Rigidbody>().AddForce(bulletStartPosition.transform.forward * bulletPower);      //前方に発射する(キャラの向きの正面)
    }
}


