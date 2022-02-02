using UnityEngine;

[System.Serializable]
public class EnemyData 
{
    public string enemyName;   //�G�̖��O
    public int enemyNo;   //�G�̔ԍ�
    public GameObject enemyPrefab;   //�G�̃v���t�@�u
    public EnemyBulletController enemyBulletPrefab;   //���e

    [Multiline]
    public string discription;   //�G�̐���
}
