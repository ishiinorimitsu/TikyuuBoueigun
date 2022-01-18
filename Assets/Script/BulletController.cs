using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour       //弾の処理のスクリプト。
                                                    //弾の動きや攻撃力などは銃のほうのスクリプトに書く
{
    [SerializeField]
    private Transform[] ThunderOrbTran;        //サンダーオーブの子供を生成する場所

    [SerializeField]
    private GameObject thunderOrbChild;     //サンダーオーブの子供

    [SerializeField]
    private bool isThunder;       //サンダーオーブが存在しているか

    [SerializeField]
    private SearchEnemy searchEnemy;

    [SerializeField]
    private EnemyController enemy;       //サンダーオーブによってゲットした敵の情報を入れる

    private float bulletPowerSpeed;  //弾の速度
    
    public void Shot(CharaController charaController)
    {
        bulletPowerSpeed = GameData.instance.equipWeaponData.bulletSpeed;    //装備している武器からの情報を得たいときはこのようにやる。



        //if (GameData.instance.equipWeaponData.weaponNo == 3)　　　//もしサンダーオーブだった時
        //{
        //    Rigidbody rb = GetComponent<Rigidbody>();

        //    //isThunder = true;    //サンダーオーブが存在する

        //    rb.AddForce(charaController.bulletStartPosition.transform.forward * bulletPowerSpeed);      //前方に発射する(キャラの向きの正面)

        //    //投げて0.6秒したらそこで止まる。
        //    StartCoroutine(ThunderOrbAttack());


        //    //弾にはまた別のスクリプトをつけ、敵の位置に飛んでいくようにする。
        //    //子オブジェクトのSearchEnemyから敵の情報を得て、それを生成した弾に送り、敵の位置に飛んでいけるようにする。
        //    //全部発射したら消える。
        //}
        //else　　//サンダーオーブ以外
        //{
            Rigidbody rb = GetComponent<Rigidbody>();

            rb.AddForce(charaController.bulletStartPosition.transform.forward * bulletPowerSpeed);      //前方に発射する(キャラの向きの正面)

            Destroy(gameObject, 5.0f);
        //}
    }


    ////-------------------------------------サンダーオーブの攻撃のスクリプト------------------------------------------//
    ///// <summary>
    ///// サンダーオーブの攻撃のスクリプト
    ///// </summary>
    ///// <returns></returns>
    //private IEnumerator ThunderOrbAttack()
    //{
    //    if(enemy != null)
    //    {
    //        yield return new WaitForSeconds(0.6f);   //0.6秒待つ

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
    //    Rigidbody rb1 = GetComponent<Rigidbody>();    //なぜ先ほど代入したrbがここでは使えないのかが分からない

    //    rb1.isKinematic = true;　　　//これで親サンダーオーブは止めておく

    //    ThunderOrbGenerate();
    //}

    //private void ThunderOrbGenerate()
    //{
    //    //止まったら子オブジェクトのGenerateTranに弾を生成する(4発撃つ)
    //    for(int i = 0; i < 4; i++)
    //    {
    //        GameObject thunderOrb = Instantiate(thunderOrbChild, ThunderOrbTran[i].position, Quaternion.identity);

    //        thunderOrb.GetComponent<ThunderOrbScript>().ThunderOrbShot(this.enemy);
    //    }

    //    Destroy(gameObject);
    //}

    ///// <summary>
    ///// サンダーオーブの索敵で得た敵の情報をbulletに送る。これによってここのスクリプト内に敵の位置が入ってきた。
    ///// </summary>
    //public void SendSearchEnemy(EnemyController enemy)    
    //{
    //    this.enemy = enemy;
    //}


    //-------------------------------------------------------------------------------------------------------------------//



    /// <summary>
    /// 弾がパーティクルシステムを使っていた場合
    /// </summary>
    /// <param name="col"></param>
    private void OnParticleCollision(GameObject col)
    {
        if (col.tag == "Enemy")       //敵に当たったとき敵を壊し、自分も消滅する。
        {
            Destroy(gameObject);

            Debug.Log("敵と衝突");
        }
        if (col.tag == "Ground")
        {
            Destroy(gameObject);

            Debug.Log("地面と衝突");
        }
    }


    /// <summary>
    /// 実体のある球を発射するときのためにこっちも用意しておく
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")       //敵に当たったとき敵を壊し、自分も消滅する。
        {
            Destroy(gameObject);

            Debug.Log("敵と衝突");
        }
        if (col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);

            Debug.Log("地面と衝突");
        }
    }
}
