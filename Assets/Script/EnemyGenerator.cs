using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    public Transform[] enemyTran;    //�G�𐶐�����ꏊ

    [SerializeField]
    private int radius;    //���a[radius]�̉~�̒����烉��������

    public void EnemyGeneratorMethod()
    {
        int generateTranIndex = Random.Range(0,enemyTran.Length);     //��������n�_�����enemyTran���烉���_���Ō��߂�

        for(int i = 0; i < 6; i++)
        {
            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //���a[radius]�̉~�̒����烉���_���Ŏ擾�ł����B(x,y)��x���W��Vector3�ł�x���W�ɁAy���W��Vector3��z���W�ɉ�����

            Vector3 trueEnemyGenerateTran = new Vector3(enemyGenerateTran.x + enemyTran[generateTranIndex].position.x, enemyTran[generateTranIndex].position.y, enemyGenerateTran.y + enemyTran[generateTranIndex].position.z);

            GameObject enemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[0].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);
        }
    }
}
