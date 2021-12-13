using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;   //õ“G”ÍˆÍ“à‚É“ü‚Á‚Ä‚«‚½ƒLƒƒƒ‰‚ğ“ü‚ê‚é

    private NavMeshAgent agent;

    public float attackRange;

    private Vector3 attackDirection;  //UŒ‚‚·‚éŒü‚«

    [SerializeField]
    private EnemyBulletController enemyBulletControllerPrefab;   //“G‚Ì’e

    [SerializeField]
    private Transform enemyBulletTran;  //’e‚ğ¶¬‚·‚éêŠ

    [SerializeField]
    private float shotSpeed;

    [SerializeField]
    private float attackInterval;

    private Animator anim;

    private float timer;

    private int attackPower = 20;    //UŒ‚—Í

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  //navMeshAgent‚ğ“ü‚ê‚é

        anim = GetComponent<Animator>();  //Animator‚ğ“ü‚ê‚é
    }

    // Update is called once per frame
    void Update()
    {
        if(agent == null)
        {
            return;
        }

        if(player != null)
        {
            agent.destination = player.transform.position;   //“G‚Ì–Ú“I’n‚Ìİ’èiplayer‚Í“®‚­‚©‚çj

            //ˆÚ“®‚·‚é‚Æ‚«‚ÌƒAƒjƒ[ƒVƒ‡ƒ“‚Ìİ’èA‚Ç‚±‚©‚µ‚ç‚Ì‘¬“x‚ª1‚æ‚è‘å‚«‚¢iˆÚ“®‚µ‚Ä‚¢‚é‚Æ‚«j
            if(agent.velocity.x > 0.2 || agent.velocity.y > 0.2 || agent.velocity.z > 0.2)
            {
                anim.SetBool("Idle",false);
                anim.SetFloat("Run",1.0f);
            }
            else
            {
                anim.SetFloat("Run", 0);
                anim.SetBool("Idle",true);
                Debug.Log("‚ ‚É‚ßOK  ");
            }

            //UŒ‚”ÍˆÍ“à‚É“ü‚Á‚½‚çUŒ‚‚µ‚Ä‚­‚é
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

    private void EnemyAttack()
    {
        anim.SetTrigger("Attack");

        attackDirection = (player.transform.position - enemyBulletTran.transform.position).normalized;

        EnemyBulletController bullet = Instantiate(enemyBulletControllerPrefab,enemyBulletTran.position,Quaternion.identity);

        bullet.EnemyShot(attackDirection,shotSpeed,attackPower);
    }
}


