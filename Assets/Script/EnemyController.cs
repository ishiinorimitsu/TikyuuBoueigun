using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;

    private NavMeshAgent agent;

    public float attackRange;

    private Vector3 attackDirection;  //UŒ‚‚·‚éŒü‚«

    [SerializeField]
    private EnemyBulletController enemyBulletControllerPrefab;   //“G‚Ì’e

    [SerializeField]
    private Transform enemyBulletTran;

    [SerializeField]
    private float shotSpeed;

    [SerializeField]
    private float attackInterval;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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

            transform.LookAt(player.transform);   //player‚Ì‚Ù‚¤‚ğŒü‚­


            if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
            {
                Debug.Log("UŒ‚");

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
        attackDirection = (player.transform.position - transform.position).normalized;

        EnemyBulletController bullet = Instantiate(enemyBulletControllerPrefab,enemyBulletTran.position,Quaternion.identity);

        bullet.EnemyShot(attackDirection,shotSpeed);
    }
}


