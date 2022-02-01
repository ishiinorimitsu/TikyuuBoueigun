using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour   //実際の開発現場では、GamaManagrにだけStartクラスを作るイメージ
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private CharaController charaController;   //スタートメソッドすべてをここに書くために持ってくる

    [SerializeField]
    private UIManager uiManager;

    public AudioSource audioSource;      //オーディオソース（BGM）用を入れる。

    public enum Wave   //ゲームは大体のステージでwave3までにする
    {
        wave1,     
        wave2,
        wave3
    }

    public Wave currentWave;     //現在のWaveを入れる。

    public enum GameState
    {
        prepare,    //ゲームを開始する前の状態
        play,　　　//ゲームプレイできる状態
        end     //ゲームクリア時やゲームオーバー時の操作できない状態
    }

    public GameState currentGameState;    //現在のGameState



    void Start()
    {
        SetGameState(GameState.prepare);     //はじめはGameStateをprepareに変える。

        //weaponGenerator.AddWeaponData();
        charaController.GameStart();      //charaController内のstartメソッドを実行する。

        enemyGenerator.PrepareEnemyGenerator();      //この処理で、まず現在のWaveを1にする。

        enemyGenerator.MatchWave();    //最初はここで現在のWaveとEnemyGenerator内のcurrentWaveIndexを一致させる。

        enemyGenerator.EnemyGenerate();     //Wave1はStartメソッドに書いておく。

        uiManager.returnHome.gameObject.SetActive(false);    //これを切らないと、「ホームに戻る」ボタンは押せてしまう。
        uiManager.gameClearSet.alpha = 0;    //ゲームクリア時のUIを隠しておく。

        uiManager.clearWindow.alpha = 0;    //ゲームクリア時のUIを隠しておく。

        uiManager.taikyaku.gameObject.SetActive(false);    //これを切らないと、退却や再挑戦のボタンは押せてしまう。

        uiManager.saityousen.gameObject.SetActive(false);    //これを切らないと、退却や再挑戦のボタンは押せてしまう。

        uiManager.gameOverSet.alpha = 0;    //ゲームオーバー時のUIを隠しておく。（徐々に出すという演出のため、これもやっておく）

        uiManager.gameEndBackGround.alpha = 0;   //ゲームが終わったときに背景をぼかすもの。

        uiManager.gameOverWindow.alpha = 0;

        uiManager.returnHome.onClick.AddListener(ClickReturnHomeButton);    //ホームに戻るボタンを押したときの処理を設定しておく

        uiManager.saityousen.onClick.AddListener(ClickSaityousenButton);    //再挑戦ボタンを押したときの処理を設定しておく

        uiManager.taikyaku.onClick.AddListener(ClickTaikyakuButton);    //退却ボタンを押したときの処理を設定しておく

        //トータルの恐竜の数をカウントする
        DataBaseManager.instance.stageDataSO.StageDataList[0].totalDinosaurCount = DataBaseManager.instance.stageDataSO.StageDataList[0].wave1DinosaurCount

                                                                                 + DataBaseManager.instance.stageDataSO.StageDataList[0].wave2DinosaurCount

                                                                                 + DataBaseManager.instance.stageDataSO.StageDataList[0].wave3DinosaurCount;

        //トータルの昆虫の数をカウントする
        DataBaseManager.instance.stageDataSO.StageDataList[0].totalInsectCount = DataBaseManager.instance.stageDataSO.StageDataList[0].wave1InsectCount

                                                                                + DataBaseManager.instance.stageDataSO.StageDataList[0].wave2InsectCount

                                                                                + DataBaseManager.instance.stageDataSO.StageDataList[0].wave3InsectCount;

        SetGameState(GameState.play);     //プレイできる状態にする。

        if(currentGameState == GameState.play)
        {
            audioSource.Play();
        }
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
                uiManager.GameClear();    //ゲームクリアのメソッドを実行する
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

    private void ClickReturnHomeButton()
    {
        SceneManager.LoadScene("ChooseWeapon");
    }

    /// <summary>
    /// GameStateを引数の中のものに変更する
    /// </summary>
    /// <param name="nextGameState"></param>
    public void SetGameState(GameState nextGameState)
    {
        currentGameState = nextGameState;    
    }

    private void ClickSaityousenButton()
    {
        //GameSceneを読み込む
        SceneManager.LoadScene("GameScene");
    }

    private void ClickTaikyakuButton()
    {
        //ChooseSceneを読み込む
        SceneManager.LoadScene("ChooseWeapon");
    }
}
