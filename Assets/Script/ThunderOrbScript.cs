using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderOrbScript : MonoBehaviour    //���̃X�N���v�g�̓T���_�[�I�[�u��p�̃X�N���v�g
{
    [SerializeField]
    private EnemyController enemy;     //�G�̈ʒu������

    [SerializeField]
    private Rigidbody rb;

    private float thunderOrbSpeed;
        

    /// <summary>
    /// �T���_�[�I�[�u���G�Ɍ������Ĕ��˂���
    /// </summary>
    public void ThunderOrbShot(EnemyController enemy)
    {
        //�T���_�[�I�[�u�̈ʒu���擾
        this.enemy = enemy;

        thunderOrbSpeed = DataBaseManager.instance.weaponDataSO.weaponDataList[2].bulletSpeed;    //�f�[�^�x�[�X����T���_�[�I�[�u�̒e�̃X�s�[�h���擾����

        Debug.Log("�����܂ł͗��Ă���");

        Debug.Log(enemy);

        if (enemy.gameObject.tag=="Enemy")
        {
            Debug.Log("�󂳂Ȃ���");

            Vector3 thunderOrbDirection = (enemy.transform.position - transform.position).normalized;

            //�G�̕����ɒe�𔭎˂���i�����߂������ɃX�s�[�h�𑫂��j
            rb.AddForce(thunderOrbDirection * thunderOrbSpeed);
        }     
        else
        {
            Debug.Log("���킷��");

            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
