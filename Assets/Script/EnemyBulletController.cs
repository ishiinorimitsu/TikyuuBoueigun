using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    //�L�����N�^�[�ɓ��������Ƃ��ɂ�����attackPower���Q�l�Ƀ_���[�W�v�Z������̂ŁA���������Ă����B
    public int attackPower;

    public void EnemyShot(Vector3 direction,float shotSpeed,int attackPower)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(direction*shotSpeed);

        //�L�����ɓ��������Ƃ��̂��߂ɂ����Ɏ����Ă���B
        this.attackPower = attackPower;

        Destroy(gameObject,4.0f);
    }

    private void OnCollisionEnter(Collision col)�@�@�@//�����ɓ��������Ƃ��̏���
    {
        if(col.gameObject.tag == "Ground")�@�@//�n�ʂɓ��������Ƃ��͏��ł���
        {
            Destroy(gameObject);�@�@
        }

        if(col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
