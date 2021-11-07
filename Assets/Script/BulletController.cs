using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour       //�e�̏����̃X�N���v�g�B
                                                    //�e�̓�����U���͂Ȃǂ͏e�̂ق��̃X�N���v�g�ɏ���
{

    [SerializeField]
    private float bulletPower;
    public void Shot(CharaController charaController)
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();

        rb.AddForce(charaController.bulletStartPosition.transform.forward * bulletPower);      //�O���ɔ��˂���(�L�����̌����̐���)

        Destroy(gameObject, 2.0f);
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")       //�G�ɓ��������Ƃ��G���󂵁A���������ł���B
        {
            Destroy(col.gameObject);

            Destroy(gameObject);
        }
    }
}
