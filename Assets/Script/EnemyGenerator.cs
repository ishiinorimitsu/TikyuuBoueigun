using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    public Transform[] enemyTran;    //敵を生成する場所

    [SerializeField]
    private int radius;    //半径[radius]の円の中からランラムで

    public void EnemyGeneratorMethod()
    {
        int generateTranIndex = Random.Range(0,enemyTran.Length);     //生成する地点を上のenemyTranからランダムで決める

        for(int i = 0; i < 6; i++)
        {
            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //半径[radius]の円の中からランダムで取得できた。(x,y)のx座標はVector3でもx座標に、y座標はVector3のz座標に加える

            Vector3 trueEnemyGenerateTran = new Vector3(enemyGenerateTran.x + enemyTran[generateTranIndex].position.x, enemyTran[generateTranIndex].position.y, enemyGenerateTran.y + enemyTran[generateTranIndex].position.z);

            GameObject enemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[0].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);
        }
    }
}
