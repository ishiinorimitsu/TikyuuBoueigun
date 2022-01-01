using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour       //弾の処理のスクリプト。
                                                    //弾の動きや攻撃力などは銃のほうのスクリプトに書く
{
    private float bulletPowerSpeed;  //弾の速度
    
    public void Shot(CharaController charaController)
    {
        bulletPowerSpeed = GameData.instance.equipWeaponData.bulletSpeed;    //装備している武器からの情報を得たいときはこのようにやる。

        Rigidbody rb = this.GetComponent<Rigidbody>();

        rb.AddForce(charaController.bulletStartPosition.transform.forward * bulletPowerSpeed);      //前方に発射する(キャラの向きの正面)

        Destroy(gameObject, 2.0f);
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")       //敵に当たったとき敵を壊し、自分も消滅する。
        {
            Destroy(gameObject);
        }
        if(col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
