using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour   //実際の開発現場では、GamaManagrにだけStartクラスを作るイメージ
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private CharaController charaController;   //スタートメソッドすべてをここに書くために持ってくる

    public enum Wave   //ゲームは大体のステージでwave3までにする
    {
        wave1,     
        wave2,
        wave3
    }

    public Wave currentWave;     //現在のWaveを入れる。

    void Start()
    {
        //weaponGenerator.AddWeaponData();
        charaController.GameStart();      //charaController内のstartメソッドを実行する。

        enemyGenerator.PrepareEnemyGenerator();      //この処理で、まず現在のWaveを1にする。

        enemyGenerator.MatchWave();    //最初はここで現在のWaveとEnemyGenerator内のcurrentWaveIndexを一致させる。

        enemyGenerator.EnemyGenerate();     //Wave1はStartメソッドに書いておく。
    }


    /// <summary>
    /// 倒した敵の数をカウントし、Waveをクリアしたか確認する。
    /// </summary>
    public void CountUpKnockOutEnemyCount()
    {
        enemyGenerator.knockDownEnemyCount++;    //倒した敵の数を＋１

        if (enemyGenerator.knockDownEnemyCount >= enemyGenerator.generatedEnemyCount)    //生成した敵の数を超えたら(全部倒したら)
        {
            Debug.Log("Waveクリア");

            if(currentWave == Wave.wave3)     //もし現在のWaveが３ならば、ゲームクリアとる。
            {
                GameClear();      //ゲームクリアのメソッドを実行する
            }
            else　　//
            {
                StartCoroutine(WaitWaveInterval(5.0f));     //まず５秒のインターバル、それから次のWaveのキャラ生成をやる
            }
        }
    }


    private IEnumerator WaitWaveInterval(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //現在のWaveを1だったら2へ、2だったら3に変える。
        enemyGenerator.UpdateNextWave();

        Debug.Log("Waveを更新しました。");

        //knockDowaEnemyCountとgeneratedEnemyCountを一回０に戻す。
        enemyGenerator.knockDownEnemyCount = 0;

        enemyGenerator.generatedEnemyCount = 0;

        Debug.Log("生成したり倒した敵の数を一回リセット");

        //現在のWaveをもとにデータベースから指定された数の敵を生成する。

        enemyGenerator.EnemyGenerate();
    }


    private void GameClear()
    {
        Debug.Log("ゲームクリア");
    }
}
