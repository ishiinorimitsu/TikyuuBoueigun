using UnityEngine;

[System.Serializable]
public class EnemyData 
{
    public string enemyName;   //敵の名前
    public int enemyNo;   //敵の番号
    public GameObject enemyPrefab;   //敵のプレファブ

    public int enemyAttackPower;   //敵の一発撃った時のダメージ（ダメージ）
    public GameObject bloodPrefab;    //流す血のプレファブ
    public int attackRange;   //武器の射程（ｍ）
    public EnemyBulletController enemyBulletPrefab;   //撃つ弾
    public float bulletSpeed;  //弾の速度

    [Multiline]
    public string discription;   //敵の説明
}
