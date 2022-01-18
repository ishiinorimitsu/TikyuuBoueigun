using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderOrbScript : MonoBehaviour    //このスクリプトはサンダーオーブ専用のスクリプト
{
    [SerializeField]
    private EnemyController enemy;     //敵の位置を入れる

    [SerializeField]
    private Rigidbody rb;

    private float thunderOrbSpeed;
        

    /// <summary>
    /// サンダーオーブが敵に向かって発射する
    /// </summary>
    public void ThunderOrbShot(EnemyController enemy)
    {
        //サンダーオーブの位置を取得
        this.enemy = enemy;

        thunderOrbSpeed = DataBaseManager.instance.weaponDataSO.weaponDataList[2].bulletSpeed;    //データベースからサンダーオーブの弾のスピードを取得する

        Debug.Log("ここまでは来ている");

        Debug.Log(enemy);

        if (enemy.gameObject.tag=="Enemy")
        {
            Debug.Log("壊さないよ");

            Vector3 thunderOrbDirection = (enemy.transform.position - transform.position).normalized;

            //敵の方向に弾を発射する（今求めた向きにスピードを足す）
            rb.AddForce(thunderOrbDirection * thunderOrbSpeed);
        }     
        else
        {
            Debug.Log("こわすよ");

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
