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

    private float x;  //x軸方向への移動力

    private float z;  //z軸方向への移動力

    private Rigidbody rb;  //Rigidbodyに力を加えるので、それを入れる。

    //-------------------------銃の発射関係---------------------------------------//

    [SerializeField]
    private BulletController bulletPrefab;  //弾のプレファブを入れる。

    public Transform bulletStartPosition;  //弾を生成する地点（銃口の場所）

    [SerializeField]
    private float bulletPower = 1000;   //どのくらいのパワーで弾が飛んでいくか


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");   //水平方向の移動がある場合、１が代入される

        z = Input.GetAxis("Vertical");  //垂直方向の移動がある場合、１が代入される

        if (Input.GetButtonDown("Jump"))  //スペースキーを押したときにメソッドが発動される。
        {
            anim.SetTrigger("Jump");

            Jump();
        }

        if (Input.GetButtonDown("Fire1")){

            BulletController createBullet = Instantiate(bulletPrefab, bulletStartPosition.position, bulletStartPosition.rotation);   //銃弾を生成する

            createBullet.Shot(this);

            anim.SetTrigger("Shot");
        }
    }

    private void FixedUpdate()
    {
        //移動する
        Move();

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * z + Camera.main.transform.right * x;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    //---------------------------------移動に関する処理----------------------------------------------------------//
    //水平方向への移動
    public void Move()
    {
        rb.velocity = new Vector3(x * moveSpeed,rb.velocity.y, z * moveSpeed);

        if(x!=0 || z != 0)
        {
            anim.SetBool("Idle", false);
            anim.SetFloat("Run", 0.5f);
        }
        else
        {
            anim.SetFloat("Run", 0);
            anim.SetBool("Idle", true);
        }
    }

    //ジャンプの移動
    public void Jump()
    {
        rb.AddForce(transform.up*jumpForce);
    }

    //----------------------------------弾を発射する処理----------------------------------------------------------//

    
}


