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
    private GameObject blood;

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

    private int attackPower = 20;    //攻撃力

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

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            currentEnemyHP -= GameData.instance.equipWeaponData.weaponAttackPower;    //弾が当たったときその武器の攻撃力分敵のHPを減らす。

            foreach (var point in col.contacts)     //血を生成する処理
            {
                var enemyBloodEffect = Instantiate(blood, point.point, Quaternion.identity);

                Destroy(enemyBloodEffect, 1.0f);
            }

            if (currentEnemyHP > 0)
            {
                Debug.Log("Hit");

                anim.SetTrigger("Hit");

                Debug.Log("ヒット");

                audioSource.PlayOneShot(attackVoice);   
            }

            if (currentEnemyHP <= 0)  //敵のHPがなくなったら
            {
                //StartCoroutine(DieAnimation());

                anim.SetTrigger("Die");

                audioSource.PlayOneShot(dieVoice);

                enemyGenerator.SendCountUpKnockOutEnemyCount();　　　　//倒した敵の数を一体ずつ増やしていく

                Destroy(gameObject, 1.0f);    //1.4秒後に消滅
            }
        }
    }

    /// <summary>
    /// 死ぬときのアニメーションや声を再生する
    /// </summary>
    /// <returns></returns>
    private IEnumerator DieAnimation()
    {
        anim.SetTrigger("Die");

        audioSource.PlayOneShot(dieVoice);

        Debug.Log("sadousiteiru");

        yield return null;
    }
}


