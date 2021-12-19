using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;   //索敵範囲内に入ってきたキャラを入れる

    private NavMeshAgent agent;

    public float attackRange;

    private Vector3 attackDirection;  //攻撃する向き

    [SerializeField]
    private EnemyBulletController enemyBulletControllerPrefab;   //敵の弾

    [SerializeField]
    private Transform enemyBulletTran;  //弾を生成する場所

    [SerializeField]
    private float shotSpeed;

    [SerializeField]
    private float attackInterval;

    [SerializeField]
    private int maxEnemyHP;    //敵の最大体力を入れる。

    [SerializeField]
    private int currentEnemyHP;    //今の敵の体力を入れる。

    private Animator anim;

    private float timer;

    private int attackPower = 20;    //攻撃力

    [SerializeField]
    private GameObject blood;

    // Start is called before the first frame update
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
                if (agent.velocity.x > 0.2 || agent.velocity.y > 0.2 || agent.velocity.z > 0.2)
                {
                    anim.SetBool("Idle", false);
                    anim.SetFloat("Run", 1.0f);
                }
                else
                {
                    anim.SetFloat("Run", 0);
                    anim.SetBool("Idle", true);
                    Debug.Log("あにめOK  ");
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

    private void EnemyAttack()
    {
        anim.SetTrigger("Attack");

        attackDirection = (player.transform.position - enemyBulletTran.transform.position).normalized;

        EnemyBulletController bullet = Instantiate(enemyBulletControllerPrefab,enemyBulletTran.position,Quaternion.identity);

        bullet.EnemyShot(attackDirection,shotSpeed,attackPower);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit");

            anim.SetTrigger("Hit");

            foreach(var point in col.contacts)
            {
                var enemyBloodEffect = Instantiate(blood,point.point,Quaternion.identity);

                Destroy(enemyBloodEffect,1.0f);
            }

            currentEnemyHP -= GameData.instance.equipWeaponData.weaponAttackPower;    //弾が当たったときその武器の攻撃力分敵のHPを減らす。

            if(currentEnemyHP <= 0)　　//敵のHPがなくなったら
            {
                //anim.SetTrigger("Die");    //死んだアニメーションを流す

                StartCoroutine(DieAnimation());

                //anim.speed = 0;

                Destroy(gameObject,1.4f);    //1.4秒後に消滅
            }
        }
    }

    private IEnumerator DieAnimation()
    {
        anim.SetTrigger("Die");

        yield return null;
    }
}


