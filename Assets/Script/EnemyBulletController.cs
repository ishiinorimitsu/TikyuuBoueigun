using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public int attackPower;

    public void EnemyShot(Vector3 direction,float shotSpeed,int attackPower)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(direction*shotSpeed);

        this.attackPower = attackPower;

        Destroy(gameObject,2.0f);
    }

    private void OnCollisionEnter(Collision col)　　　//何かに当たったときの処理
    {
        if(col.gameObject.tag == "Ground")　　//地面に当たったときは消滅する
        {
            Destroy(gameObject);　　
        }

        if(col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
