using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;   //���G�͈͓��ɓ����Ă����L����������

    private NavMeshAgent agent;

    private Animator anim;

    [SerializeField]
    private GameObject blood;

    [SerializeField]
    private EnemyGenerator enemyGenerator;

    //----------------------------------------�U���֌W------------------------------------------------//

    public float attackRange;

    private Vector3 attackDirection;  //�U���������

    [SerializeField]
    private EnemyBulletController enemyBulletControllerPrefab;   //�G�̒e

    [SerializeField]
    private Transform enemyBulletTran;  //�e�𐶐�����ꏊ

    [SerializeField]
    private float shotSpeed;

    [SerializeField]
    private float attackInterval;�@�@�@�@//�G�̍U������C���^�[�o��

    private float timer;    //���̃^�C�}�[���C���^�[�o���̎��Ԃ𒴂�����U������

    private int attackPower = 20;    //�U����

    //-----------------------------------------�̗͊֌W----------------------------------------------------//

    [SerializeField]
    private int maxEnemyHP;    //�G�̍ő�̗͂�����B

    [SerializeField]
    private int currentEnemyHP;    //���̓G�̗̑͂�����B



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  //navMeshAgent������

        anim = GetComponent<Animator>();  //Animator������

        currentEnemyHP = maxEnemyHP;   //�G�̗̑͂��}�b�N�X�ɂ���
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnemyHP > 0)    //�����HP������Ƃ�
        {
            if (agent == null)
            {
                return;
            }

            if (player != null)
            {
                agent.destination = player.transform.position;   //�G�̖ړI�n�̐ݒ�iplayer�͓�������j

                //�ړ�����Ƃ��̃A�j���[�V�����̐ݒ�A�ǂ�������̑��x��1���傫���i�ړ����Ă���Ƃ��j
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
    }


    /// <summary>
    /// EnemyGenerator���擾����B
    /// </summary>
    /// <param name="enemyGenerator"></param>
    public void GetEnemyGenerator(EnemyGenerator enemyGenerator)
    {
        this.enemyGenerator = enemyGenerator;
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

            currentEnemyHP -= GameData.instance.equipWeaponData.weaponAttackPower;    //�e�����������Ƃ����̕���̍U���͕��G��HP�����炷�B

            if (currentEnemyHP <= 0)  //�G��HP���Ȃ��Ȃ�����
            {
               StartCoroutine(DieAnimation());

                enemyGenerator.SendCountUpKnockOutEnemyCount();�@�@�@�@//�|�����G�̐�����̂����₵�Ă���

                Destroy(gameObject, 1.4f);    //1.4�b��ɏ���
            }
        }
    }

    private IEnumerator DieAnimation()
    {
        anim.SetTrigger("Die");

        yield return null;
    }
}


