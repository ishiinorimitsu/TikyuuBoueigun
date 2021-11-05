using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour       //弾の処理のスクリプト。
                                                    //弾の動きや攻撃力などは銃のほうのスクリプトに書く
{
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")       //敵に当たったとき敵を壊し、自分も消滅する。
        {
            Destroy(col.gameObject);

            Destroy(gameObject);
        }
    }
}
