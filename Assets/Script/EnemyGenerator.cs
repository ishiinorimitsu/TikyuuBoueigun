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

    public int knockDownEnemyCount;    //倒した敵の数（各waveごとにリセットする）

    private int currentWaveDinosaurCount;　　//今のウェーブで作る恐竜の数

    private int currentWaveInsectCount;     //今のウェーブで生成する昆虫の数

    private int currentWaveBossDinosaurCount;　　//今のウェーブで作る恐竜の数

    private int currentWaveBossInsectCount;     //今のウェーブで生成する昆虫の数

    private int currentWaveGenerateTran;     //今のウェーブで生成する場所

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

                //現在のウェーブのそれぞれの敵の数、生成する場所をcurrentWaveDinosaurCountなどに対応させる。
                currentWaveDinosaurCount = GameData.instance.currentStageData.wave1DinosaurCount;
                currentWaveInsectCount = GameData.instance.currentStageData.wave1InsectCount;
                currentWaveBossDinosaurCount = GameData.instance.currentStageData.wave1BossDinosaur;
                currentWaveBossInsectCount = GameData.instance.currentStageData.wave1BossInsect;
                currentWaveGenerateTran = GameData.instance.currentStageData.wave1GenerateEnemyTranIndex;

                Debug.Log("Wave1です");
                break;

            case (GameManager.Wave.wave2):　　//Wave2なら

                //現在のウェーブのそれぞれの敵の数、生成する場所をcurrentWaveDinosaurCountなどに対応させる。
                currentWaveDinosaurCount = GameData.instance.currentStageData.wave2DinosaurCount;
                currentWaveInsectCount = GameData.instance.currentStageData.wave2InsectCount;
                currentWaveBossDinosaurCount = GameData.instance.currentStageData.wave2BossDinosaur;
                currentWaveBossInsectCount = GameData.instance.currentStageData.wave2BossInsect;
                currentWaveGenerateTran = GameData.instance.currentStageData.wave2GenerateEnemyTranIndex;

                Debug.Log("Wave2です");
                break;

            case (GameManager.Wave.wave3):　　//Wave3なら

                //現在のウェーブのそれぞれの敵の数、生成する場所をcurrentWaveDinosaurCountなどに対応させる。
                currentWaveDinosaurCount = GameData.instance.currentStageData.wave3DinosaurCount;
                currentWaveInsectCount = GameData.instance.currentStageData.wave3InsectCount;
                currentWaveBossDinosaurCount = GameData.instance.currentStageData.wave3BossDinosaur;
                currentWaveBossInsectCount = GameData.instance.currentStageData.wave3BossInsect;
                currentWaveGenerateTran = GameData.instance.currentStageData.wave3GenerateEnemyTranIndex;

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
        for (int i = 0; i < currentWaveDinosaurCount; i++)     
        {
            //------------------------------------位置の指定------------------------------------------------------//

            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //半径[radius]の円の中からランダムで取得できた。(x,y)のx座標はVector3でもx座標に、y座標はVector3のz座標に加える

            Vector3 trueEnemyGenerateTran = new Vector3(

                    enemyGenerateTran.x + enemyTran[currentWaveGenerateTran].position.x,      //DataBaseManagerの中のStageDataSOで指定した場所のそれぞれの座標にenemyGenerateTranのものを加える。

                    enemyTran[currentWaveGenerateTran].position.y,

                    enemyGenerateTran.y + enemyTran[currentWaveGenerateTran].position.z

                    );

            //-----------------------------------生成する-------------------------------------------------------------//

            GameObject enemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[0].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);

            enemy.GetComponent<EnemyController>().GetEnemyGenerator(this);    //この処理で作ったEnemyController内にEnemyGeneratorを入れる。

            generatedEnemyCount++;     //生成した敵の数を数える
        }


        //虫の生成
        for (int j = 0;j < currentWaveInsectCount; j++)
        {
            //------------------------------------位置の指定------------------------------------------------------//

            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //半径[radius]の円の中からランダムで取得できた。(x,y)のx座標はVector3でもx座標に、y座標はVector3のz座標に加える

            Vector3 trueEnemyGenerateTran = new Vector3(

                    enemyGenerateTran.x + enemyTran[currentWaveGenerateTran].position.x,      //DataBaseManagerの中のStageDataSOで指定した場所のそれぞれの座標にenemyGenerateTranのものを加える。

                    enemyTran[currentWaveGenerateTran].position.y,

                    enemyGenerateTran.y + enemyTran[currentWaveGenerateTran].position.z

                    );

            //------------------------------------生成する------------------------------------------------------//

            GameObject insectEnemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[1].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);

            insectEnemy.GetComponent<EnemyController>().GetEnemyGenerator(this);    //この処理で作ったEnemyController内にEnemyGeneratorを入れる。

            generatedEnemyCount++;     //生成した敵の数を数える
        }


        //ボス恐竜の生成
        for (int j = 0; j < currentWaveBossDinosaurCount; j++)
        {
            //------------------------------------位置の指定------------------------------------------------------//

            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //半径[radius]の円の中からランダムで取得できた。(x,y)のx座標はVector3でもx座標に、y座標はVector3のz座標に加える

            Vector3 trueEnemyGenerateTran = new Vector3(

                    enemyGenerateTran.x + enemyTran[currentWaveGenerateTran].position.x,      //DataBaseManagerの中のStageDataSOで指定した場所のそれぞれの座標にenemyGenerateTranのものを加える。

                    enemyTran[currentWaveGenerateTran].position.y,

                    enemyGenerateTran.y + enemyTran[currentWaveGenerateTran].position.z

                    );

            //------------------------------------生成する------------------------------------------------------//

            GameObject bossDinosaurEnemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[2].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);

            bossDinosaurEnemy.GetComponent<EnemyController>().GetEnemyGenerator(this);    //この処理で作ったEnemyController内にEnemyGeneratorを入れる。

            generatedEnemyCount++;     //生成した敵の数を数える
        }


        //ボス虫の生成
        for (int j = 0; j < currentWaveBossInsectCount; j++)
        {
            //------------------------------------位置の指定------------------------------------------------------//

            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //半径[radius]の円の中からランダムで取得できた。(x,y)のx座標はVector3でもx座標に、y座標はVector3のz座標に加える

            Vector3 trueEnemyGenerateTran = new Vector3(

                    enemyGenerateTran.x + enemyTran[currentWaveGenerateTran].position.x,      //DataBaseManagerの中のStageDataSOで指定した場所のそれぞれの座標にenemyGenerateTranのものを加える。

                    enemyTran[currentWaveGenerateTran].position.y,

                    enemyGenerateTran.y + enemyTran[currentWaveGenerateTran].position.z

                    );

            //------------------------------------生成する------------------------------------------------------//

            GameObject bossInsectEnemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[3].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);

            bossInsectEnemy.GetComponent<EnemyController>().GetEnemyGenerator(this);    //この処理で作ったEnemyController内にEnemyGeneratorを入れる。

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
