using UnityEngine;

[System.Serializable]
public class EnemyData 
{
    public string enemyName;   //�G�̖��O
    public int enemyNo;   //�G�̔ԍ�
    public GameObject enemyPrefab;   //�G�̃v���t�@�u

    public int enemyAttackPower;   //�G�̈ꔭ���������̃_���[�W�i�_���[�W�j
    public GameObject bloodPrefab;    //�������̃v���t�@�u
    public int attackRange;   //����̎˒��i���j
    public EnemyBulletController enemyBulletPrefab;   //���e
    public float bulletSpeed;  //�e�̑��x

    [Multiline]
    public string discription;   //�G�̐���
}
