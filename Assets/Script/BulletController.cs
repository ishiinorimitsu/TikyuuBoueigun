using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour       //�e�̏����̃X�N���v�g�B
                                                    //�e�̓�����U���͂Ȃǂ͏e�̂ق��̃X�N���v�g�ɏ���
{
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")       //�G�ɓ��������Ƃ��G���󂵁A���������ł���B
        {
            Destroy(col.gameObject);

            Destroy(gameObject);
        }
    }
}