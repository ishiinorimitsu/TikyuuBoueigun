using UnityEngine;

[System.Serializable]
public class EnemyData 
{
    public string enemyName;   //敵の名前
    public int enemyNo;   //敵の番号
    public GameObject enemyPrefab;   //敵のプレファブ
    public GameObject bloodPrefab;    //流す血のプレファブ
    public EnemyBulletController enemyBulletPrefab;   //撃つ弾

    [Multiline]
    public string discription;   //敵の説明
}
