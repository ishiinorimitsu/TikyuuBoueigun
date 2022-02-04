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
    private Collider dieCollider;    //���񂾂Ƃ��R���C�_��Trigger�ɂ��āA���񂾂�e��������Ȃ��悤�ɂ���

    [SerializeField]
    private GameObject blood;  //�G��������

    [SerializeField]
    private Transform enemyBloodPosition;    //�����o��ꏊ������B

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

    [SerializeField]
    private int attackPower;    //�U����

    //-----------------------------------------�̗͊֌W----------------------------------------------------//

    [SerializeField]
    private int maxEnemyHP;    //�G�̍ő�̗͂�����B

    [SerializeField]
    private int currentEnemyHP;    //���̓G�̗̑͂�����B


    //-------------------------------------------����------------------------------------------------------//

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip attackVoice;

    [SerializeField]
    private AudioClip dieVoice;

    //------------------------------------------�A�C�e��-----------------------------------------//
    [SerializeField]
    private GameObject heal;     //�񕜃A�C�e��������B�i���ʂ̓G�����Ƃ��̂́u�񕜁i���j�v�A�{�X�����Ƃ��̂́u�񕜁i��j�v�j

    [SerializeField]
    private int dropHealPercent;     //���p�[�Z���g�̊m���ŉ񕜃A�C�e���𗎂Ƃ���



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  //navMeshAgent������

        anim = GetComponent<Animator>();  //Animator������

        currentEnemyHP = maxEnemyHP;   //�G�̗̑͂��}�b�N�X�ɂ���

        player = GameObject.Find("Player");
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

                transform.LookAt(player.transform);

                //�ړ�����Ƃ��̃A�j���[�V�����̐ݒ�A�ǂ�������̑��x��0.2���傫���i�ړ����Ă���Ƃ��j
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

        audioSource.PlayOneShot(attackVoice);

        attackDirection = (player.transform.position - enemyBulletTran.transform.position).normalized;

        EnemyBulletController bullet = Instantiate(enemyBulletControllerPrefab,enemyBulletTran.position,Quaternion.identity);

        bullet.EnemyShot(attackDirection,shotSpeed,attackPower);
    }

    private void OnParticleCollision(GameObject col)      //�e��particleeffect�ō���Ă���Ƃ���OnCollisionEnter�ł͔������Ȃ������̂ŁAOnParticleCollision�ɂ���
    {
        if (currentEnemyHP > 0)
        {
            if (col.gameObject.tag == "Bullet")
            {
                Debug.Log("Particle�R���C�_������");
                currentEnemyHP -= GameData.instance.equipWeaponData.weaponAttackPower;    //�e�����������Ƃ����̕���̍U���͕��G��HP�����炷�B

                //��L�̗��R��OnParticleCollision���g���A������GameObject�ɂȂ����̂ŁAforeach�̒���contact���g���Ȃ��B���������āA�u���������ꏊ�Ɍ��̃G�t�F�N�g�𔭐�������v�Ƃ��������̎����͂�����߁A���炩���ߌ��𔭐�������ꏊ���w�肵�Ă���
                //foreach (var point in col.contacts)     //���𐶐����鏈��
                //{
                //    var enemyBloodEffect = Instantiate(blood, point.point, Quaternion.identity);

                //    Destroy(enemyBloodEffect, 1.0f);
                //}


                //�����������ۂɎg��
                

                if(currentEnemyHP > 0)
                {
                    anim.SetTrigger("Hit");

                    Debug.Log("�܂�HP������̂Ńq�b�g�̃A�j���[�V�����𗬂��܂��B�i���́j");

                    audioSource.PlayOneShot(attackVoice);

                    var enemyBloodEffect = Instantiate(blood, enemyBloodPosition.position, Quaternion.identity);

                    Destroy(enemyBloodEffect, 1.0f);
                }
                if (currentEnemyHP <= 0)  //�G��HP���Ȃ��Ȃ�����
                {
                    anim.SetTrigger("Die");

                    Debug.Log("���̍U����HP���O�ɂȂ����̂Ŏ��񂾃A�j���[�V�����𗬂��܂�");

                    audioSource.PlayOneShot(dieVoice);

                    enemyGenerator.SendCountUpKnockOutEnemyCount();    //�|�����G�̐�����̂����₵�Ă���

                    //---------------------------------�񕜃A�C�e�������̏���--------------------------------------//

                    int random;

                    random = Random.Range(0, 100);

                    Debug.Log(random);

                    if (random <= dropHealPercent)
                    {
                        var healDrop = Instantiate(heal, transform.position, Quaternion.identity);
                    }

                    Destroy(gameObject, 1.0f);    //1.4�b��ɏ���
                }
            }
        }
        //����HP��0�������ꍇ�������Ȃ��B(��������Ȃ��Ǝ��񂾌������������������Ă��܂�)
        if (currentEnemyHP <= 0)
        {
            return;
        }
    }

    /// <summary>
    /// �e�����̂̎��̂���
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter(Collision col)
    {
        //���̎��_�ł͒e��������O�܂ł�HP
        if (currentEnemyHP > 0)
        {
            if (col.gameObject.tag == "Bullet")
            {
                Debug.Log("���̂̂����������");

                currentEnemyHP -= GameData.instance.equipWeaponData.weaponAttackPower;    //�e�����������Ƃ����̕���̍U���͕��G��HP�����炷�B

                //��L�̗��R��OnParticleCollision���g���A������GameObject�ɂȂ����̂ŁAforeach�̒���contact���g���Ȃ��B���������āA�u���������ꏊ�Ɍ��̃G�t�F�N�g�𔭐�������v�Ƃ��������̎����͂�����߁A���炩���ߌ��𔭐�������ꏊ���w�肵�Ă���
                //foreach (var point in col.contacts)     //���𐶐����鏈��
                //{
                //    var enemyBloodEffect = Instantiate(blood, point.point, Quaternion.identity);

                //    Destroy(enemyBloodEffect, 1.0f);
                //}

                if (currentEnemyHP > 0)
                {
                    anim.SetTrigger("Hit");

                    Debug.Log("�܂�HP������̂Ńq�b�g�̃A�j���[�V�����𗬂��܂��B�i���́j");

                    audioSource.PlayOneShot(attackVoice);

                    //�����������ۂɎg��
                    var enemyBloodEffect = Instantiate(blood, enemyBloodPosition.position, Quaternion.identity);

                    Destroy(enemyBloodEffect, 1.0f);
                }

                //���̍U����HP���O�ɂȂ�����
                if (currentEnemyHP <= 0)  
                {
                    anim.SetTrigger("Die");

                    Debug.Log("���̍U����HP���O�ɂȂ����̂Ŏ��񂾃A�j���[�V�����𗬂��܂�");

                    audioSource.PlayOneShot(dieVoice);

                    enemyGenerator.SendCountUpKnockOutEnemyCount();    //�|�����G�̐�����̂����₵�Ă���

                    //---------------------------------�񕜃A�C�e�������̏���--------------------------------------//

                    int random;

                    random = Random.Range(0,100);

                    Debug.Log(random);

                    if(random <= dropHealPercent)
                    {
                        var healDrop = Instantiate(heal,transform.position,Quaternion.identity);
                    }

                    Destroy(gameObject, 1.0f);    //1.4�b��ɏ���
                }

            }
        }
        //����HP��0�������ꍇ�������Ȃ��B(��������Ȃ��Ǝ��񂾌������������������Ă��܂�)
        if(currentEnemyHP <= 0)
        {
            return;
        }       
    }
}


