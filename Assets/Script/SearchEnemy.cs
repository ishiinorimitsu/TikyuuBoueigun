using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchEnemy : MonoBehaviour     //ここの中に入ってきた敵を弾に情報として与える
{
    [SerializeField]
    private EnemyController enemy;

    [SerializeField]
    private BulletController bullet;

    private void Start()
    {
        enemy = null;
    }

    /// <summary>
    /// 範囲内に入ってきた敵をenemyの中に入れる。
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            TryGetComponent<EnemyController>(out enemy);
        }

        //bullet.SendSearchEnemy(enemy);
    }
}
