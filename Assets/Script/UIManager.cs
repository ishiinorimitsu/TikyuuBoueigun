using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    //----------------------------------------エネルギー関係----------------------------------------------------//

    //[SerializeField]
    //private Image energySlider;     //エネルギーゲージを入れる

    //private float maxEnergyCount;    //最大エネルギー量を入れる

    //-----------------------------------------弾数関係---------------------------------------------------//

    [SerializeField]
    private Image bulletSlider;     //残り弾数ゲージを入れる

    [SerializeField]
    private Text lastBullet;    //残っている弾数を入れる。

    [SerializeField]
    private Text maxBullet;    //最大弾数を入れる。(Text)

    private float maxBulletCount;   //最大弾数を入れる(float)

    [SerializeField]
    public int reloadLastBullet;   //弾数をカウントアップするとき用の弾

    public int ReloadLastBullet
    {
        set
        {
            reloadLastBullet = value;

            lastBullet.text = reloadLastBullet.ToString();
        }
        get
        {
            return reloadLastBullet;
        }
    }

    //------------------------------------------武器関係--------------------------------------------------//

    [SerializeField]
    private Image selectedWeapon;  //選ばれている武器の画像を入れる

    [SerializeField]
    private Text selectedWeaponText;  //選ばれている武器のテキストを入れる

    //------------------------------------------HP関係--------------------------------------------------//

    [SerializeField]
    private Image HPSlider;    //HPゲージを入れる

    [SerializeField]
    private Image reloadSlider;     //リロード中のゲージを入れる

    [SerializeField]
    private Text lastHP;    //残っているHPを入れる。

    [SerializeField]
    private Text maxHP;    //最大HPを入れる。(Text)

    private float maxHPCount;    //最大HPを入れる(float)

    //------------------------------------------クリア時のUI関係--------------------------------------------------//

    public CanvasGroup gameClearSet;     //ゲームクリアした時に出すリボンを入れる

    public CanvasGroup clearWindow;     //ゲームのリザルト画面を入れる

    public CanvasGroup gameOverSet;     //GameOver時の文字を入れる。

    public CanvasGroup gameEndBackGround;    //ゲームが終わったときの背景をぼかすもの

    public CanvasGroup gameOverWindow;   //ゲームオーバー時の退却か再挑戦かを選ぶウィンドウを入れる。

    public Button taikyaku;    //退却のボタン（のちにここを押したときにChooseSceneを読み込む）

    public Button saityousen;    //再挑戦のボタン（のちにここを押したときにGameSceneを読み込む）

    public Button returnHome;    //クリアした後の「ホームに戻る」ボタン

    public Text defeatDinosaurCount;    //倒した恐竜の数   

    public Text defeatInsectCount;   //倒した虫の数


    //-------------------------------------効果音の設定-------------------------------------//

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip fanfare;     //ゲームをクリアしたときのファンファーレ

    [SerializeField]
    private AudioClip clearBGM;    //ファンファーレの後に流すBGM

    [SerializeField]
    private AudioClip failBGM;     //ゲームオーバーの時のBGM


    //----------------------------------------エネルギーゲージの処理------------------------------------------------------//
    /// <summary>
    /// エネルギーゲージの処理
    /// </summary>
    /// <param name="maxEnergy"></param>
    //public void SetEnergySliderValue(float maxEnergy)
    //{
    //    this.maxEnergyCount = maxEnergy;

    //    energySlider.fillAmount = maxEnergy;     //スライダーの中のmaxValueをmaxEnergyと一緒にする。

    //    UpdateDisplayEnergy(maxEnergy);    //まず最初はvalueの値はmaxEnergyと同じでいい。
    //}

    //public void UpdateDisplayEnergy(float currentEnergy)
    //{
    //    //Debug.Log(currentEnergy);

    //    energySlider.fillAmount = currentEnergy/maxEnergyCount;  //currentEnergyまで1.0秒かけて動かす（最初の引数の値はmaxEnergyでいい。）

    //    energySlider.DOFillAmount(currentEnergy / maxEnergyCount, 1.0f);
    //}


    //------------------------------------------武器の弾数等の処理---------------------------------------------------------//

    /// <summary>
    /// スライダーのゲージに関する処理
    /// </summary>
    /// <param name="maxBullet"></param>
    public void SetWeaponSliderValue(int maxBullet,int currentBullet)     //まず準備
    {
        this.maxBulletCount = maxBullet;

        bulletSlider.fillAmount = maxBullet;     //スライダーの中のmaxValueをmaxBulletと一緒にする。

        UpdateDisplayBullet(currentBullet);    //まず最初はvalueの値はmaxEnergyと同じでいい。

        this.maxBullet.text = maxBullet.ToString();    //UIの最大弾数を装備している武器の最大弾数にする。

       reloadSlider.fillAmount = 0;    //リロードするとき（0から100を担当するので、リロードの最大は０にする）
    }

    /// <summary>
    /// スライダーに変更があった場合
    /// </summary>
    /// <param name="currentBulletCount"></param>
    public void UpdateDisplayBullet(int currentBulletCount)　　　//変化があったときの処理
    {
        bulletSlider.fillAmount　=　currentBulletCount/maxBulletCount;  //currentBulletCountまでvalueを動かす（最初の引数の値はmaxBulletCountでいい。）

        lastBullet.text = currentBulletCount.ToString();   //今の球数を反映させる。数字の更新
    }

    /// <summary>
    /// 武器をリロードするときのスライダーの処理
    /// </summary>
    public void ReloadWeaponSlider ()
    {
        //今までの武器ゲージを見えなくする。
        bulletSlider.gameObject.SetActive(false);

        //ゲージを徐々に回復する
        reloadSlider.DOFillAmount(maxBulletCount/maxBulletCount,GameData.instance.equipWeaponData.reloadTime);

        //武器の残り球数を徐々に変化させる。（まだテキストに反映させていない）
        DOTween.To(()=>ReloadLastBullet,num=>ReloadLastBullet=num,GameData.instance.equipWeaponData.maxBullet,GameData.instance.equipWeaponData.reloadTime);

        //武器ゲージを再び見えるようにする
        bulletSlider.gameObject.SetActive(true);

        //リロードゲージのFillAmountを、リロード残弾数を０に戻す
        StartCoroutine(waitReloadTime());
    }

    private IEnumerator waitReloadTime()
    {
        yield return new WaitForSeconds(GameData.instance.equipWeaponData.reloadTime);

        reloadSlider.fillAmount = 0;

        reloadLastBullet = 0;
    }
    //------------------------------------------今の武器の名前と画像に関する処理-------------------------------------------------//
    public void SetSelectedWeapon()
    {
        selectedWeapon.sprite = GameData.instance.equipWeaponData.weaponSprite;    //現在選ばれている武器の画像が入る

        selectedWeaponText.text = GameData.instance.equipWeaponData.weaponName;    //現在選ばれている武器の名前が入る
    }

    //------------------------------------------------------HPゲージの処理--------------------------------------------------------//
    public void SetHpSliderValue(int maxHp)
    {
        this.maxHPCount = maxHp;

        HPSlider.fillAmount = maxHp;     //スライダーの中のmaxValueをmaxHpと一緒にする。

        UpdateDisplayHp(maxHp);    //まず最初はvalueの値はmaxHpと同じでいい。

        maxHP.text = maxHp.ToString();    //UIの最大HPを表示する。
    }

    public void UpdateDisplayHp(int currentHp)
    {
        HPSlider.fillAmount = currentHp/maxHPCount; //currentHpまで1.0秒かけて動かす（最初の引数の値はmaxHpでいい。）

        lastHP.text = currentHp.ToString();   //今のHPを反映させる。数字の更新
    }

    //-----------------------------------------------------ゲームが終わったときの処理---------------------------------------------//

    
    /// <summary>
    /// ゲームクリアの条件を満たしたときにGameClearの画像をファンファーレとともに出す。
    /// </summary>
    public void GameClear()
    {
        //背景を曇らせる
        gameEndBackGround.DOFade(1.0f, 1.0f);

        gameManager.SetGameState(GameManager.GameState.end);   //操作できないようにする

        //今流れている曲を止める
        gameManager.audioSource.Stop();

        //ファンファーレを流す
        audioSource.PlayOneShot(fanfare);

        //GameClearのリボンを出す
        gameClearSet.DOFade(endValue: 1.0f, duration: 2.0f);     //2秒間かけてリボンを見えるようにする

        //５秒くらい待つ
        StartCoroutine(WaitClearTime());
    }

    private IEnumerator WaitClearTime()
    {
        yield return new WaitForSeconds(5.0f);

        //リザルト画面を出す
        ShowClearResult();
    }

    private void ShowClearResult()
    {
        //倒した敵の数を数える
        CountDefeatEnemy();

        //returnHomeのスイッチを入れる。
        returnHome.gameObject.SetActive(true);
        
        //ClearResultを出す
        clearWindow.DOFade(1.0f,1.0f);

        audioSource.PlayOneShot(clearBGM);
    }

    public void GameOver()
    {
        //背景を曇らせる
        gameEndBackGround.DOFade(1.0f,1.0f);

        gameManager.SetGameState(GameManager.GameState.end);   //操作できないようにする

        //今流れている曲を止める
        gameManager.audioSource.Stop();

        //失敗のBGMを流す
        audioSource.PlayOneShot(failBGM);

        //切っていた退却、再挑戦のスイッチを入れる
        taikyaku.gameObject.SetActive(true);
        saityousen.gameObject.SetActive(true);

        gameOverSet.DOFade(endValue: 1.0f, duration: 2.0f);     //2秒間かけてGameOverを見えるようにする

        StartCoroutine(WaitGameOverTime());
    }

    private IEnumerator WaitGameOverTime()
    {
        yield return new WaitForSeconds(5.0f);

        //「GameOver」の画面のスイッチを切る（表示させない）
        gameOverSet.gameObject.SetActive(false);

        //リザルト画面を出す
        ShowGameOverWindow();
    }

    public void ShowGameOverWindow()
    {
        gameOverWindow.DOFade(1.0f, 1.0f);
    }

    private void CountDefeatEnemy()
    {
        //今計算したものをテキストに入れる。
        defeatDinosaurCount.text = GameData.instance.currentStageData.totalDinosaurCount.ToString();

        defeatInsectCount.text = GameData.instance.currentStageData.totalInsectCount.ToString();
    }
}
