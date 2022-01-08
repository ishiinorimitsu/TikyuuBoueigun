using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour       //�e�̏����̃X�N���v�g�B
                                                    //�e�̓�����U���͂Ȃǂ͏e�̂ق��̃X�N���v�g�ɏ���
{
    private float bulletPowerSpeed;  //�e�̑��x
    
    public void Shot(CharaController charaController)
    {
        bulletPowerSpeed = GameData.instance.equipWeaponData.bulletSpeed;    //�������Ă��镐�킩��̏��𓾂����Ƃ��͂��̂悤�ɂ��B

        Rigidbody rb = this.GetComponent<Rigidbody>();

        rb.AddForce(charaController.bulletStartPosition.transform.forward * bulletPowerSpeed);      //�O���ɔ��˂���(�L�����̌����̐���)

        Destroy(gameObject, 2.0f);
    }

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
