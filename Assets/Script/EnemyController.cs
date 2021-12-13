using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;   //���G�͈͓��ɓ����Ă����L����������

    private NavMeshAgent agent;

    public float attackRange;

    private Vector3 attackDirection;  //�U���������

    [SerializeField]
    private EnemyBulletController enemyBulletControllerPrefab;   //�G�̒e

    [SerializeField]
    private Transform enemyBulletTran;  //�e�𐶐�����ꏊ

    [SerializeField]
    private float shotSpeed;

    [SerializeField]
    private float attackInterval;

    private Animator anim;

    private float timer;

    private int attackPower = 20;    //�U����

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  //navMeshAgent������

        anim = GetComponent<Animator>();  //Animator������
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
            agent.destination = player.transform.position;   //�G�̖ړI�n�̐ݒ�iplayer�͓�������j

            //�ړ�����Ƃ��̃A�j���[�V�����̐ݒ�A�ǂ�������̑��x��1���傫���i�ړ����Ă���Ƃ��j
            if(agent.velocity.x > 0.2 || agent.velocity.y > 0.2 || agent.velocity.z > 0.2)
            {
                anim.SetBool("Idle",false);
                anim.SetFloat("Run",1.0f);
            }
            else
            {
                anim.SetFloat("Run", 0);
                anim.SetBool("Idle",true);
                Debug.Log("���ɂ�OK  ");
            }

            //�U���͈͓��ɓ�������U�����Ă���
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


