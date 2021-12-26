using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    public Transform[] enemyTran;    //敵を生成する場所

    [SerializeField]
    private int radius;    //半径[radius]の円の中からランラムで

    [SerializeField]
    private GameManager gameManager;     //gameManagerと連携する(こっちのスクリプトでは生成の準備をするだけ。実際に作るのはgameManagerスクリプト)

    private int currentWaveIndex;    //現在のWaveが第何Waveかを入れる

    public int generatedEnemyCount;    //生成した敵の数

    public int knockDownEnemyCount;    //倒した敵の数

    /// <summary>
    /// 敵を生成する処理の準備をする
    /// </summary>
    public void PrepareEnemyGenerator()
    {
        //まず始まったときはgameManagerのWaveをwave1にする。
        gameManager.currentWave = GameManager.Wave.wave1;
    }

    /// <summary>
    /// 敵を倒し終わったらこのメソッドを入れて更新する。
    /// </summary>
    public void UpdateNextWave()
    {
        //現在のwaveを1増やす（wave1からwave2へ、wave2からwave3へ）
        if(gameManager.currentWave == GameManager.Wave.wave1)
        {
            gameManager.currentWave = GameManager.Wave.wave2;
        }else if(gameManager.currentWave == GameManager.Wave.wave2)
        {
            gameManager.currentWave = GameManager.Wave.wave3;
        }

        MatchWave();   //currentWaveを反映させる。
    }

    /// <summary>
    /// 現在のWaveからDataBasemanagerのインデックス番号と一致させるためのメソッド。
    /// </summary>
    public void MatchWave()
    {
        switch (gameManager.currentWave)    //もし現在のWaveが
        {
            case (GameManager.Wave.wave1):　　//Wave1なら

                currentWaveIndex = 0;    //この処理によってStageData内のインデックス番号と第何Waveかを一致させている

                Debug.Log("Wave1です");
                break;

            case (GameManager.Wave.wave2):　　//Wave2なら

                currentWaveIndex = 1;    //この処理によってStageData内のインデックス番号と第何Waveかを一致させている

                Debug.Log("Wave2です");
                break;

            case (GameManager.Wave.wave3):　　//Wave3なら

                currentWaveIndex = 2;    //この処理によってStageData内のインデックス番号と第何Waveかを一致させている

                Debug.Log("Wave3です");
                break;
        }
    }

    /// <summary>
    /// 敵を生成する処理。
    /// </summary>
    public void EnemyGenerate()
    {     
        //恐竜の生成
        for (int i = 0; i < DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].DinosaurCount; i++)     
        {
            //------------------------------------位置の指定------------------------------------------------------//

            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //半径[radius]の円の中からランダムで取得できた。(x,y)のx座標はVector3でもx座標に、y座標はVector3のz座標に加える

            Vector3 trueEnemyGenerateTran = new Vector3(

                    enemyGenerateTran.x + enemyTran[DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].GenerateEnemyTranIndex].position.x,      //DataBaseManagerの中のStageDataSOで指定した場所のそれぞれの座標にenemyGenerateTranのものを加える。

                    enemyTran[DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].GenerateEnemyTranIndex].position.y,

                    enemyGenerateTran.y + enemyTran[DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].GenerateEnemyTranIndex].position.z

                    );

            //-----------------------------------生成する-------------------------------------------------------------//

            GameObject enemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[0].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);

            enemy.GetComponent<EnemyController>().GetEnemyGenerator(this);    //この処理で作ったEnemyController内にEnemyGeneratorを入れる。

            generatedEnemyCount++;     //生成した敵の数を数える
        }


        //ロボットの生成
        for (int j = 0;j < DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].RobotCount; j++)
        {
            //------------------------------------位置の指定------------------------------------------------------//

            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //半径[radius]の円の中からランダムで取得できた。(x,y)のx座標はVector3でもx座標に、y座標はVector3のz座標に加える

            Vector3 trueEnemyGenerateTran = new Vector3(

                    enemyGenerateTran.x + enemyTran[DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].GenerateEnemyTranIndex].position.x,      //DataBaseManagerの中のStageDataSOで指定した場所のそれぞれの座標にenemyGenerateTranのものを加える。

                    enemyTran[DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].GenerateEnemyTranIndex].position.y,

                    enemyGenerateTran.y + enemyTran[DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].GenerateEnemyTranIndex].position.z

                    );

            //------------------------------------生成する------------------------------------------------------//

            GameObject robotEnemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[1].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);

            robotEnemy.GetComponent<EnemyController>().GetEnemyGenerator(this);    //この処理で作ったEnemyController内にEnemyGeneratorを入れる。

            generatedEnemyCount++;     //生成した敵の数を数える
        }
    }

    /// <summary>
    /// GameManagerのCountUpKnockOutEnemyCountメソッドをEnemyControllerから取り出せるようにする
    /// </summary>
    public void SendCountUpKnockOutEnemyCount()
    {
        gameManager.CountUpKnockOutEnemyCount();
    }
}
