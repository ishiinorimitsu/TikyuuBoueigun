using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private float bulletPower;   //どのくらいのパワーで弾が飛んでいくか

    [SerializeField]
    private List<WeaponData> weaponDataList = new List<WeaponData>();

    [SerializeField]
    private float currentBullet;    //今の弾数を入れる。

    [SerializeField]
    private float maxBullet;    //最大弾数を入れる。

    [SerializeField]

    private float minBullet = 0;   //最小球数

    private bool isReload;  //リロード中か

    //---------------------------エネルギー関係------------------------------------//

    public float maxEnergy;     //最大エネルギー量

    public float minEnergy;     //最小エネルギー量（のちの範囲指定で使う）

    public float currentEnergy;       //現在のエネルギー量

    public float jumpEnergy; 　　//一回ジャンプするごとに消費するエネルギー、1を基準として％で計算するよりも、持ち点のように100あって、10ずつ減るみたいなほうが作りやすい

    public float attackEnergy;　　//一回攻撃するごとに使うエネルギー

    public float cureEnergy;    //地面にいる間に回復するエネルギー

    public UIManager UIManager;   //UIManagerにデータを送れるようにする。

    public void GameStart()
    {
        rb = GetComponent<Rigidbody>();　　　//Rigidbodyを代入しておく

        currentEnergy = maxEnergy;    //ゲームが開始したときに最大エネルギー量にしておく。

        UIManager.SetEnergySliderValue(maxEnergy);   //エネルギーに関するもののセット

        UIManager.SetWeaponSliderValue(GameData.instance.equipWeaponData.maxAttackCount);   //最大弾数をセット

        UIManager.SetSelectedWeapon();

        maxBullet = GameData.instance.equipWeaponData.maxAttackCount;  //最大球数を装備している武器から得る

        currentBullet = maxBullet;   //ゲームが開始したときに最大弾数にしておく。

        bulletPower = GameData.instance.equipWeaponData.bulletSpeed;　　//弾の速度は装備している武器の速度
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");   //水平方向の移動がある場合、１が代入される

        z = Input.GetAxis("Vertical");  //垂直方向の移動がある場合、１が代入される

        if (Input.GetButtonDown("Jump")　& currentEnergy >= jumpEnergy)  //スペースキーを押したときにメソッドが発動される。
        {
            anim.SetTrigger("Jump");

            Jump();
        }


        //-----------------------------------------銃を発射する----------------------------------------------------//

        if (currentBullet > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                BulletController createBullet = Instantiate(bulletPrefab, bulletStartPosition.position, bulletStartPosition.rotation);   //銃弾を生成する

                createBullet.Shot(this);

                anim.SetTrigger("Shot");

                currentBullet--;    //今の球数を撃つたびに1ずつ減らしていく

                currentBullet = Mathf.Clamp(currentBullet, minBullet, maxBullet);   //今の球数の範囲を指定する

                UIManager.UpdateDisplayBullet(currentBullet);   //弾数の処理を反映させる
            }
        }

        else
        {
            if (isReload == false)
            {
                isReload = true;   //弾がないのに連打されたとき用

                //装備している武器の「リロード時間」分だけ撃てないようにする
                StartCoroutine(ReloadWeapon());
            }
        }

        //---------------------------------------武器を変える-------------------------------------------------------------//
        if (Input.GetButtonDown("ChangeWeapon"))
        {
            GameData.instance.ChangeWeapon();

            Debug.Log(GameData.instance.equipWeaponData.weaponName);

            UIManager.SetWeaponSliderValue(GameData.instance.equipWeaponData.maxAttackCount);

            UIManager.SetSelectedWeapon();
        }
    }

    private void FixedUpdate()
    {
        //移動する
        Move();

        //カメラの向きからキャラの向きを変える。
        LookRotation();
    }

    //---------------------------------移動に関する処理----------------------------------------------------------//
    //水平方向への移動
    public void Move()
    {
        //rb.velocity = new Vector3(x * moveSpeed,rb.velocity.y, z * moveSpeed);

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
        rb.AddForce(transform.up*jumpForce);   //ジャンプする処理

        JumpEnergyDecrease();   //ジャンプするごとにエネルギーを減らす。

        UIManager.UpdateDisplayEnergy(currentEnergy);   //エネルギー値を更新する
    }

    private void LookRotation()
    {
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

    //----------------------------------エネルギーに関する処理----------------------------------------------------------//

    private void JumpEnergyDecrease()
    {
        currentEnergy -= jumpEnergy;　　//現在のエネルギー量からジャンプエナジーを減らす。
    }
    
    private void OnCollisionStay(Collision col)
    {
        if(col.gameObject.tag == "ground")
        {
            currentEnergy += cureEnergy;

            currentEnergy = Mathf.Clamp(currentEnergy, minEnergy, maxEnergy);

            UIManager.UpdateDisplayEnergy(currentEnergy);   //エネルギー値を更新する
        }
    }

    //-----------------------------------銃を発射する処理に関するメソッド----------------------------------------------------//
    /// <summary>
    /// 装備している武器の「リロード時間」分だけ撃てないようにする
    /// </summary>
    private IEnumerator ReloadWeapon()
    {
        anim.SetTrigger("Reload");

        //装備している武器の「リロードエネルギー」分だけエネルギーを減らす
        currentEnergy -= GameData.instance.equipWeaponData.reloadEnergy;

        UIManager.UpdateDisplayEnergy(currentEnergy);

        yield return new WaitForSeconds(GameData.instance.equipWeaponData.reloadTime);

        //装備している武器の「currentBullet」を最大にする
        currentBullet = maxBullet;

        UIManager.UpdateDisplayBullet(currentBullet);

        isReload = false;
    }
}


