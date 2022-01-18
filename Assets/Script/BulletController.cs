using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour       //�e�̏����̃X�N���v�g�B
                                                    //�e�̓�����U���͂Ȃǂ͏e�̂ق��̃X�N���v�g�ɏ���
{
    [SerializeField]
    private Transform[] ThunderOrbTran;        //�T���_�[�I�[�u�̎q���𐶐�����ꏊ

    [SerializeField]
    private GameObject thunderOrbChild;     //�T���_�[�I�[�u�̎q��

    [SerializeField]
    private bool isThunder;       //�T���_�[�I�[�u�����݂��Ă��邩

    [SerializeField]
    private SearchEnemy searchEnemy;

    [SerializeField]
    private EnemyController enemy;       //�T���_�[�I�[�u�ɂ���ăQ�b�g�����G�̏�������

    private float bulletPowerSpeed;  //�e�̑��x
    
    public void Shot(CharaController charaController)
    {
        bulletPowerSpeed = GameData.instance.equipWeaponData.bulletSpeed;    //�������Ă��镐�킩��̏��𓾂����Ƃ��͂��̂悤�ɂ��B



        //if (GameData.instance.equipWeaponData.weaponNo == 3)�@�@�@//�����T���_�[�I�[�u��������
        //{
        //    Rigidbody rb = GetComponent<Rigidbody>();

        //    //isThunder = true;    //�T���_�[�I�[�u�����݂���

        //    rb.AddForce(charaController.bulletStartPosition.transform.forward * bulletPowerSpeed);      //�O���ɔ��˂���(�L�����̌����̐���)

        //    //������0.6�b�����炻���Ŏ~�܂�B
        //    StartCoroutine(ThunderOrbAttack());


        //    //�e�ɂ͂܂��ʂ̃X�N���v�g�����A�G�̈ʒu�ɔ��ł����悤�ɂ���B
        //    //�q�I�u�W�F�N�g��SearchEnemy����G�̏��𓾂āA����𐶐������e�ɑ���A�G�̈ʒu�ɔ��ł�����悤�ɂ���B
        //    //�S�����˂����������B
        //}
        //else�@�@//�T���_�[�I�[�u�ȊO
        //{
            Rigidbody rb = GetComponent<Rigidbody>();

            rb.AddForce(charaController.bulletStartPosition.transform.forward * bulletPowerSpeed);      //�O���ɔ��˂���(�L�����̌����̐���)

            Destroy(gameObject, 5.0f);
        //}
    }


    ////-------------------------------------�T���_�[�I�[�u�̍U���̃X�N���v�g------------------------------------------//
    ///// <summary>
    ///// �T���_�[�I�[�u�̍U���̃X�N���v�g
    ///// </summary>
    ///// <returns></returns>
    //private IEnumerator ThunderOrbAttack()
    //{
    //    if(enemy != null)
    //    {
    //        yield return new WaitForSeconds(0.6f);   //0.6�b�҂�

    //        ThunderOrbStop();
    //    }
    //    else
    //    {
    //        yield return new WaitForSeconds(0.6f);

    //        Destroy(gameObject);
    //    }
    //}

    //private void ThunderOrbStop()
    //{
    //    Rigidbody rb1 = GetComponent<Rigidbody>();    //�Ȃ���قǑ������rb�������ł͎g���Ȃ��̂���������Ȃ�

    //    rb1.isKinematic = true;�@�@�@//����Őe�T���_�[�I�[�u�͎~�߂Ă���

    //    ThunderOrbGenerate();
    //}

    //private void ThunderOrbGenerate()
    //{
    //    //�~�܂�����q�I�u�W�F�N�g��GenerateTran�ɒe�𐶐�����(4������)
    //    for(int i = 0; i < 4; i++)
    //    {
    //        GameObject thunderOrb = Instantiate(thunderOrbChild, ThunderOrbTran[i].position, Quaternion.identity);

    //        thunderOrb.GetComponent<ThunderOrbScript>().ThunderOrbShot(this.enemy);
    //    }

    //    Destroy(gameObject);
    //}

    ///// <summary>
    ///// �T���_�[�I�[�u�̍��G�œ����G�̏���bullet�ɑ���B����ɂ���Ă����̃X�N���v�g���ɓG�̈ʒu�������Ă����B
    ///// </summary>
    //public void SendSearchEnemy(EnemyController enemy)    
    //{
    //    this.enemy = enemy;
    //}


    //-------------------------------------------------------------------------------------------------------------------//



    /// <summary>
    /// �e���p�[�e�B�N���V�X�e�����g���Ă����ꍇ
    /// </summary>
    /// <param name="col"></param>
    private void OnParticleCollision(GameObject col)
    {
        if (col.tag == "Enemy")       //�G�ɓ��������Ƃ��G���󂵁A���������ł���B
        {
            Destroy(gameObject);

            Debug.Log("�G�ƏՓ�");
        }
        if (col.tag == "Ground")
        {
            Destroy(gameObject);

            Debug.Log("�n�ʂƏՓ�");
        }
    }


    /// <summary>
    /// ���̂̂��鋅�𔭎˂���Ƃ��̂��߂ɂ��������p�ӂ��Ă���
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")       //�G�ɓ��������Ƃ��G���󂵁A���������ł���B
        {
            Destroy(gameObject);

            Debug.Log("�G�ƏՓ�");
        }
        if (col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);

            Debug.Log("�n�ʂƏՓ�");
        }
    }
}
