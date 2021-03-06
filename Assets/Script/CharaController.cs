using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaController : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    //------------------------アニメーション-------------------------------------//

    [SerializeField]
    private Animator anim;

    //-------------------------移動関係---------------------------------------//

    [SerializeField]
    private float jumpForce;  //ジャンプ力

    [SerializeField]
    private bool isGround;     //今地面と接しているか

    [SerializeField]
    private float moveSpeed;  //水平方向への移動力

    private float x;  //x軸方向への移動力

    private float z;  //z軸方向への移動力

    private Rigidbody rb;  //Rigidbodyに力を加えるので、それを入れる。

    //[SerializeField]
    //private GameObject flyFire;    //飛ぶときにロケットのお尻から出る炎

    //[SerializeField]
    //private Transform fireGenerateTran;     //炎を出す場所

    //-------------------------銃の発射関係---------------------------------------//

    [SerializeField]
    private BulletController bulletPrefab;  //弾のプレファブを入れる。

    public Transform bulletStartPosition;  //弾を生成する地点（銃口の場所）

    [SerializeField]
    private float bulletPower;   //どのくらいのパワーで弾が飛んでいくか

    //[SerializeField]
    //private List<WeaponData> weaponDataList = new List<WeaponData>();

    [SerializeField]
    private List<int> currentBulletList = new List<int>();　　//武器の今の弾数のリスト

    //[SerializeField]
    //private int currentBullet;    //今の弾数を入れる。

    private int minBullet = 0;   //最小球数

    private bool isReload;  //リロード中か


    private int bulletTimer;     //連射できる武器のタイマー

    //---------------------------エネルギー関係------------------------------------//

    public float maxEnergy;     //最大エネルギー量

    public float minEnergy;     //最小エネルギー量（のちの範囲指定で使う）

    public float currentEnergy;       //現在のエネルギー量

    public float jumpEnergy; 　　//一回ジャンプするごとに消費するエネルギー、1を基準として％で計算するよりも、持ち点のように100あって、10ずつ減るみたいなほうが作りやすい

    public float attackEnergy;　　//一回攻撃するごとに使うエネルギー

    public float cureEnergy;    //地面にいる間に回復するエネルギー

    public UIManager UiManager;   //UIManagerにデータを送れるようにする。


    //-------------------------------HP関係-----------------------------------------//

    [SerializeField]
    private int maxHp;    //最大HP

    private int minHp = 0;    //最小HP

    [SerializeField]
    public int currentHp;    //現在のHP

    //---------------------------------ゲーム情報--------------------------------------//
    [SerializeField]
    private EnemyGenerator enemyGenerator;


    //---------------------------------サウンドエフェクトの内容-----------------------------------//

    [SerializeField]
    private AudioSource audioSource;    //オーディオソースを入れる。

    [SerializeField]
    private AudioSource walkAudio;    //歩く音を入れるオーディオソースを入れる

    [SerializeField]
    private AudioClip reloadGunSE;    //銃をリロードする音

    [SerializeField]
    private AudioClip shotGunSE;     //銃を撃つ音

    [SerializeField]
    private AudioClip healSE;    //回復アイテムをとったときの音

    [SerializeField]
    private AudioClip walkSE;    //キャラの歩く音

    [SerializeField]
    private AudioClip jumpSE;    //ジャンプの音




    public void GameStart()
    {
        rb = GetComponent<Rigidbody>();   //Rigidbodyを代入しておく

        currentEnergy = maxEnergy;    //ゲームが開始したときに最大エネルギー量にしておく。

        currentHp = maxHp;   //ゲームが開始したときに最大体力にしておく。

        //UiManager.SetEnergySliderValue(maxEnergy);   //エネルギーに関するもののセット

        for (int i = 0; i < GameData.instance.chooseWeaponData.Count; i++)
        {
            //currentBulletのリストに持てる武器分の「currentBullet」を作る
            currentBulletList.Add(GameData.instance.chooseWeaponData[i].maxBullet);
        }

        UiManager.SetWeaponSliderValue(GameData.instance.equipWeaponData.maxBullet, currentBulletList[GameData.instance.currentEquipWeaponNo]);   //最大弾数をセット（引数は、今選ばれている武器の最大弾数）

        UiManager.SetSelectedWeapon();　　//今選ばれている武器の名前と画像をセット

        UiManager.SetHpSliderValue(maxHp);    //最大HPをセットする

        bulletPower = GameData.instance.equipWeaponData.bulletSpeed;　　//弾の速度は装備している武器の速度
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHp > 0)
        {
            if (gameManager.currentGameState == GameManager.GameState.play)   //GameStateがplayの時だけ
            {
                x = Input.GetAxis("Horizontal");   //水平方向の移動がある場合、１が代入される

                z = Input.GetAxis("Vertical");  //垂直方向の移動がある場合、１が代入される

                if (isGround)
                {
                    if (Input.GetButtonDown("Jump") & currentEnergy >= jumpEnergy)  //スペースキーを押したときにメソッドが発動される。
                    {
                        anim.SetTrigger("Jump");

                        Jump();
                    }
                }


                //-----------------------------------------銃を発射する----------------------------------------------------/

                //今選んでいる武器の現在の弾数が０より大きかったら（まだ弾が入っていたら）
                if (currentBulletList[GameData.instance.currentEquipWeaponNo] > 0)
                {
                    //連射できる武器の場合
                    if (GameData.instance.equipWeaponData.rapidFire == true)
                    {
                        if (Input.GetButton("Fire1"))
                        {
                            bulletTimer++;

                            if (bulletTimer > GameData.instance.equipWeaponData.rapidFireTimer)
                            {
                                bulletTimer = 0;

                                BulletController createBullet = Instantiate(GameData.instance.equipWeaponData.bulletPrefab, bulletStartPosition.position, bulletStartPosition.rotation);   //銃弾を生成する

                                createBullet.Shot(this);

                                anim.SetTrigger("Attack");

                                audioSource.PlayOneShot(shotGunSE);   //銃を撃つ音を鳴らす。

                                currentBulletList[GameData.instance.currentEquipWeaponNo]--;    //今の球数を撃つたびに1ずつ減らしていく

                                currentBulletList[GameData.instance.currentEquipWeaponNo] = Mathf.Clamp(currentBulletList[GameData.instance.currentEquipWeaponNo], minBullet, GameData.instance.equipWeaponData.maxBullet);   //今の球数の範囲を指定する

                                UiManager.UpdateDisplayBullet(currentBulletList[GameData.instance.currentEquipWeaponNo]);   //弾数の処理を反映させる
                            }
                        }
                    }

                    //連射できない武器の場合
                    else if(GameData.instance.equipWeaponData.rapidFire == false)
                    {
                        if (Input.GetButtonDown("Fire1"))
                        {
                            //銃弾を生成する
                            BulletController createBullet = Instantiate(GameData.instance.equipWeaponData.bulletPrefab, bulletStartPosition.position, bulletStartPosition.rotation); 

                            //実際弾がどう移動するかはここにある。
                            createBullet.Shot(this);

                            anim.SetTrigger("Attack");

                            audioSource.PlayOneShot(shotGunSE);   //銃を撃つ音を鳴らす。

                            currentBulletList[GameData.instance.currentEquipWeaponNo]--;    //今の球数を撃つたびに1ずつ減らしていく

                            currentBulletList[GameData.instance.currentEquipWeaponNo] = Mathf.Clamp(currentBulletList[GameData.instance.currentEquipWeaponNo], minBullet, GameData.instance.equipWeaponData.maxBullet);   //今の球数の範囲を指定する

                            UiManager.UpdateDisplayBullet(currentBulletList[GameData.instance.currentEquipWeaponNo]);   //弾数の処理を反映させる
                        }
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

                //「リロード中に武器を変えたとき、裏でもリロードを継続して行う」という処理がうまく実装できなかったため、リロード中の時は武器を変えられないようにする
                if(isReload == false)
                {
                    if (Input.GetButtonDown("ChangeWeapon"))
                    {
                        GameData.instance.ChangeWeapon();  //持っている武器を変える処理

                        audioSource.PlayOneShot(reloadGunSE);

                        Debug.Log(GameData.instance.equipWeaponData.weaponName);

                        UiManager.SetWeaponSliderValue(GameData.instance.equipWeaponData.maxBullet, currentBulletList[GameData.instance.currentEquipWeaponNo]);  //

                        UiManager.SetSelectedWeapon();   //現在選ばれている武器の名前、イラストを変える
                    }
                }
            }
        }
    }   

    private void FixedUpdate()
    {
        if (gameManager.currentGameState == GameManager.GameState.play)
        {
             //移動する
             Move();
            
            //カメラの向きからキャラの向きを変える。
            LookRotation();
        }
    }

    //---------------------------------移動に関する処理----------------------------------------------------------//
    //水平方向への移動

    //歩く効果音を鳴らすためのタイマー
    float walkTimer;
    public void Move()
    {
        //rb.velocity = new Vector3(x * moveSpeed,rb.velocity.y, z * moveSpeed);
        if (x != 0 || z != 0)
        {
            anim.SetBool("Idle", false);
            anim.SetFloat("Run", 0.5f);
            
            walkTimer+=Time.deltaTime;
            if (isGround)
            {
                if (walkTimer > 0.3f)
                {
                    walkTimer = 0;
                    walkAudio.PlayOneShot(walkSE, 0.3f);
                    StartCoroutine(walkSoundWait());
                }
            } 
        }
        else
        {
            anim.SetFloat("Run", 0);
            anim.SetBool("Idle", true);
        }
    }

    private IEnumerator walkSoundWait()
    {
        yield return new WaitForSeconds(0.5f);

        walkAudio.Stop();
    }

    //ジャンプの移動
    public void Jump()
    {
        rb.AddForce(transform.up*jumpForce);   //ジャンプする処理

        JumpEnergyDecrease();   //ジャンプするごとにエネルギーを減らす。

        audioSource.PlayOneShot(jumpSE);

        walkAudio.Stop();   //歩く音を止める

        //UiManager.UpdateDisplayEnergy(currentEnergy);   //エネルギー値を更新する
    }


    //private void OnCollisionExit(Collision col)
    //{
    //    if (col.gameObject.tag == "Ground")
    //    {
    //        isGround = false;
    //    }
    //}

    private void LookRotation()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * z + Camera.main.transform.right * x;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
       

        // キャラクターの向きを進行方向に
        //if (moveForward != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.LookRotation(moveForward);
        //}

        transform.rotation = Quaternion.Euler(0, Camera.main.transform.localEulerAngles.y, 0);
    }

    //----------------------------------エネルギーに関する処理----------------------------------------------------------//

    private void JumpEnergyDecrease()
    {
        currentEnergy -= jumpEnergy;　　//現在のエネルギー量からジャンプエナジーを減らす。
    }
    
    private void OnCollisionStay(Collision col)
    {
        if(col.gameObject.tag == "Ground")
        {
            currentEnergy += cureEnergy;

            currentEnergy = Mathf.Clamp(currentEnergy, minEnergy, maxEnergy);

            //UiManager.UpdateDisplayEnergy(currentEnergy);   //エネルギー値を更新する

            isGround = true;
        }
    }

    //-----------------------------------銃を発射する処理に関するメソッド----------------------------------------------------//
    /// <summary>
    /// 弾をセットする。装備している武器が持つ「最大弾数」をcurrentBulletListに入れる
    /// </summary>
    private void SetBullet()
    {
        for(int i = 0; i < GameData.instance.chooseWeaponData.Count; i++)  //現在登録されている武器（２つ）
        {
            currentBulletList.Add(GameData.instance.chooseWeaponData[i].maxBullet);　　//それぞれの武器の最大弾数をcurrentBulletListに代入する
        }
    }
    
    /// <summary>
    /// 装備している武器の「リロード時間」分だけ撃てないようにする
    /// </summary>
    private IEnumerator ReloadWeapon()
    {
        anim.SetTrigger("Reload");

        //装備している武器の「リロードエネルギー」分だけエネルギーを減らす
        currentEnergy -= GameData.instance.equipWeaponData.reloadEnergy;

        //UiManager.UpdateDisplayEnergy(currentEnergy);

        UiManager.ReloadWeaponSlider();

        yield return new WaitForSeconds(GameData.instance.equipWeaponData.reloadTime);

        //装備している武器の「currentBullet」を最大にする
        currentBulletList[GameData.instance.currentEquipWeaponNo] = GameData.instance.equipWeaponData.maxBullet;

        UiManager.UpdateDisplayBullet(currentBulletList[GameData.instance.currentEquipWeaponNo]);

        audioSource.PlayOneShot(reloadGunSE);

        isReload = false;
    }


    //-----------------------------------------攻撃されたときの処理----------------------------------------------------------------//

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "EnemyBullet")
        {
            col.gameObject.TryGetComponent<EnemyBulletController>(out EnemyBulletController enemyBulletController);

            currentHp -= enemyBulletController.attackPower;　　//攻撃されるたびにHpを減らす

            currentHp = Mathf.Clamp(currentHp,minHp,maxHp);    //HPがマイナスになったりを防ぐ

            UiManager.UpdateDisplayHp(currentHp);    //HPのゲージを更新する

            if (currentHp <= 0)    //その攻撃でHPが0になったら死ぬアニメーションを流す。
            {
                anim.SetTrigger("Die");    //死ぬアニメーションを流す

                UiManager.GameOver();     //GameOverの処理を実装する。

                StartCoroutine(KillPlayer());　　　//キャラのスイッチを切らないと死んだ後も恐竜の攻撃で何回も死んでしまうから
            }
        }

        if(col.gameObject.tag == "Heal")
        {
            audioSource.PlayOneShot(healSE);   //回復アイテムをとったときの音を流す

            col.gameObject.TryGetComponent(out HealItemScript healItem);     //接触した回復アイテムを取得する。

            currentHp += healItem.healCount;

            currentHp = Mathf.Clamp(currentHp,minHp,maxHp);

            UiManager.UpdateDisplayHp(currentHp);     //HPゲージの更新をする
        }
    }

    /// <summary>
    /// コルーチンにしないと最初の一回の死ぬアニメーションも再生されないから。
    /// </summary>
    /// <returns></returns>
    private IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(1.0f);    //１秒待つ

        gameObject.SetActive(false);　　//キャラのスイッチを切る。
    }
}


