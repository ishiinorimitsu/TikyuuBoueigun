using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;   //索敵範囲内に入ってきたキャラを入れる

    private NavMeshAgent agent;

    private Animator anim;

    [SerializeField]
    private Collider dieCollider;    //死んだときコライダをTriggerにして、死んだら弾が当たらないようにする

    [SerializeField]
    private GameObject blood;  //敵が流す血

    [SerializeField]
    private Transform enemyBloodPosition;    //血が出る場所を入れる。

    [SerializeField]
    private EnemyGenerator enemyGenerator;

    //----------------------------------------攻撃関係------------------------------------------------//

    public float attackRange;

    private Vector3 attackDirection;  //攻撃する向き

    [SerializeField]
    private EnemyBulletController enemyBulletControllerPrefab;   //敵の弾

    [SerializeField]
    private Transform enemyBulletTran;  //弾を生成する場所

    [SerializeField]
    private float shotSpeed;

    [SerializeField]
    private float attackInterval;　　　　//敵の攻撃するインターバル

    private float timer;    //このタイマーがインターバルの時間を超えたら攻撃する

    [SerializeField]
    private int attackPower;    //攻撃力

    //-----------------------------------------体力関係----------------------------------------------------//

    [SerializeField]
    private int maxEnemyHP;    //敵の最大体力を入れる。

    [SerializeField]
    private int currentEnemyHP;    //今の敵の体力を入れる。


    //-------------------------------------------鳴き声------------------------------------------------------//

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip attackVoice;

    [SerializeField]
    private AudioClip dieVoice;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  //navMeshAgentを入れる

        anim = GetComponent<Animator>();  //Animatorを入れる

        currentEnemyHP = maxEnemyHP;   //敵の体力をマックスにする

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnemyHP > 0)    //相手のHPがあるとき
        {
            if (agent == null)
            {
                return;
            }

            if (player != null)
            {
                agent.destination = player.transform.position;   //敵の目的地の設定（playerは動くから）

                //移動するときのアニメーションの設定、どこかしらの速度が1より大きい（移動しているとき）
                if (Mathf.Abs(agent.velocity.x) > 0.2 || Mathf.Abs(agent.velocity.y) > 0.2 || Mathf.Abs(agent.velocity.z) > 0.2)
                {
                    anim.SetBool("Idle", false);
                    anim.SetFloat("Run", 1.0f);
                }
                else
                {
                    anim.SetFloat("Run", 0);
                    anim.SetBool("Idle", true);
                }

                //攻撃範囲内に入ったら攻撃してくる
                if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
                {

                    timer += Time.deltaTime;

                    if (timer > attackInterval)
                    {
                        EnemyAttack();

                        timer = 0;
                    }
                }
            }
        }
    }


    /// <summary>
    /// EnemyGeneratorを取得する。
    /// </summary>
    /// <param name="enemyGenerator"></param>
    public void GetEnemyGenerator(EnemyGenerator enemyGenerator)
    {
        this.enemyGenerator = enemyGenerator;
    }


    private void EnemyAttack()
    {
        anim.SetTrigger("Attack");

        audioSource.PlayOneShot(attackVoice);

        attackDirection = (player.transform.position - enemyBulletTran.transform.position).normalized;

        EnemyBulletController bullet = Instantiate(enemyBulletControllerPrefab,enemyBulletTran.position,Quaternion.identity);

        bullet.EnemyShot(attackDirection,shotSpeed,attackPower);
    }

    private void OnParticleCollision(GameObject col)      //弾をparticleeffectで作っているときはOnCollisionEnterでは反応しなかったので、OnParticleCollisionにする
    {
        if(col.gameObject.tag == "Bullet")
        {
            if (player == null)
            {
                player = GameObject.Find("Player");
            }

            Debug.Log("弾と認識");

            currentEnemyHP -= GameData.instance.equipWeaponData.weaponAttackPower;    //弾が当たったときその武器の攻撃力分敵のHPを減らす。



            //上記の理由でOnParticleCollisionを使い、引数がGameObjectになったので、foreachの中のcontactが使えない。したがって、「当たった場所に血のエフェクトを発生させる」という処理の実装はあきらめ、あらかじめ血を発生させる場所を指定しておく
            //foreach (var point in col.contacts)     //血を生成する処理
            //{
            //    var enemyBloodEffect = Instantiate(blood, point.point, Quaternion.identity);

            //    Destroy(enemyBloodEffect, 1.0f);
            //}


            //こっちを実際に使う
            var enemyBloodEffect = Instantiate(blood, enemyBloodPosition.position, Quaternion.identity);

            Destroy(enemyBloodEffect, 1.0f);


            if (currentEnemyHP > 0)
            {
                anim.SetTrigger("Hit");

                audioSource.PlayOneShot(attackVoice);   
            }

            if (currentEnemyHP <= 0)  //敵のHPがなくなったら
            {
                //HPが0になったときコライダのトリガーをオンにすることで弾を当たらなくする。
                dieCollider.isTrigger = true;

                //StartCoroutine(DieAnimation());

                anim.SetTrigger("Die");

                audioSource.PlayOneShot(dieVoice);

                enemyGenerator.SendCountUpKnockOutEnemyCount();　　　　//倒した敵の数を一体ずつ増やしていく

                Destroy(gameObject, 1.0f);    //1.4秒後に消滅
            }
        }
    }

    /// <summary>
    /// 弾が実体の時のため
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter(Collision col)
    {
        //この時点では弾が当たる前までのHP
        if (currentEnemyHP > 0)
        {
            anim.SetTrigger("Hit");

            audioSource.PlayOneShot(attackVoice);

            if (col.gameObject.tag == "Bullet")
            {
                //if(player == null)
                //{
                //    player = GameObject.Find("Player");
                //}

                currentEnemyHP -= GameData.instance.equipWeaponData.weaponAttackPower;    //弾が当たったときその武器の攻撃力分敵のHPを減らす。



                //上記の理由でOnParticleCollisionを使い、引数がGameObjectになったので、foreachの中のcontactが使えない。したがって、「当たった場所に血のエフェクトを発生させる」という処理の実装はあきらめ、あらかじめ血を発生させる場所を指定しておく
                //foreach (var point in col.contacts)     //血を生成する処理
                //{
                //    var enemyBloodEffect = Instantiate(blood, point.point, Quaternion.identity);

                //    Destroy(enemyBloodEffect, 1.0f);
                //}

                //今の攻撃でHPが０になったら
                if (currentEnemyHP <= 0)  
                {
                    ////HPが0になったときコライダのトリガーをオンにすることで弾を当たらなくする。
                    //dieCollider.isTrigger = true;

                    //StartCoroutine(DieAnimation());

                    anim.SetTrigger("Die");

                    audioSource.PlayOneShot(dieVoice);

                    enemyGenerator.SendCountUpKnockOutEnemyCount();    //倒した敵の数を一体ずつ増やしていく

                    Destroy(gameObject, 1.0f);    //1.4秒後に消滅
                }


                //こっちを実際に使う
                var enemyBloodEffect = Instantiate(blood, enemyBloodPosition.position, Quaternion.identity);

                Destroy(enemyBloodEffect, 1.0f);
            }
        }

        //既にHPが0だった場合何もしない。
        if(currentEnemyHP <= 0)
        {
            return;
        }       
    }
}


