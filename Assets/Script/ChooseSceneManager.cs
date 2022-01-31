using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseSceneManager : MonoBehaviour
{

    [SerializeField]
    private Transform canvasTran;    //ポップアップを生成する位置（Canvasの子オブジェクトにする。Transform型だと親子関係もできる）

    //-----------------------------------武器を選ぶウィンドウの処理----------------------------------------//

    [SerializeField]
    private SelectWeaponDetail selectWeaponDetail;    //実際に押す武器のボタンを入れる

    [SerializeField]
    private Button weapon1;　　　//ここを押したら武器を選ぶウィンドウが開く（武器１）

    [SerializeField]
    private Text weapon1Text;

    [SerializeField]
    private Button weapon2;　　　//ここを押したら武器を選ぶウィンドウが開く（武器２）

    [SerializeField]
    private Text weapon2Text;

    [SerializeField]
    private WeaponData weaponData1;    //今選ばれている武器１の情報を入れる（選ぶたびに上書きする。出撃するときにGameDataのWeaponDataListに入れる。）

    [SerializeField]
    private WeaponData weaponData2;    //今選ばれている武器２の情報を入れる（選ぶたびに上書きする。出撃するときにGameDataのWeaponDataListに入れる。）

    [SerializeField]
    private ChooseWeaponWindow weaponInfo;    //表示したりする用に作っておく(chooseWeaponWindowPrefabを代入したりする)

    [SerializeField]
    private ChooseWeaponWindow chooseWeaponWindowPrefab;   //生成するポップアップのプレファブ

    [SerializeField]
    private Button gameStartButton;   //出撃するボタン

    public enum WeaponSlotType
    {
        slot1,
        slot2
    }

    public WeaponSlotType weaponSlotType;  //今はどっちのボタンから来たか

    //------------------------------------------ステージを選ぶ時の処理------------------------------------------------//

    [SerializeField]
    private SelectStageDetail selectStageDetail;    //実際に押すステージのボタンを入れる

    [SerializeField]
    private Button stageButton;　　　//ここを押したらステージを選ぶウィンドウが開く

    [SerializeField]
    private Text stageButtonText;     //上のstageButtonのテキスト部分（デフォルトでは「ステージを選択してください」となっている）

    [SerializeField]
    private ChooseStageWindow chooseStageWindowPrefab;     //ステージのポップアップを入れる

    [SerializeField]
    private ChooseStageWindow stageInfo;    //上のchooseStageWindowPrefabを入れる。実際に操作するのはここに入れた複製品の方

    [SerializeField]
    private StageData stageData;   //選ばれたステージの情報を入れる

    [SerializeField]
    private Text totalDinosaurCount;    //出現する恐竜の数(ステージの下の説明欄)

    [SerializeField]
    private Text totalInsectCount;    //出現する昆虫の数(ステージの下の説明欄)



    //------------------------------------------効果音に関する処理-----------------------------------------------//

    public AudioSource audioSource;    //オーディオソースを入れる。

    public AudioClip buttonSelectSE;     //ノーマルのボタン以外のSE

    [SerializeField]
    private AudioClip openMenu;     //武器のメニューを開く音

    [SerializeField]
    private AudioClip syutugekiButtonSE;      //出撃ボタンのSE



    private void Start()
    {
        //---------------------------------武器-----------------------------------------//

        weapon1.onClick.AddListener(() => ChooseWeapon(WeaponSlotType.slot1));

        weapon2.onClick.AddListener(() => ChooseWeapon(WeaponSlotType.slot2));

        weaponData1 = null;
        weaponData2 = null;    //この2行がないと[SerializedField]があると、nullにならない

        //----------------------------------ステージ-----------------------------------//

        stageButton.onClick.AddListener(ChooseStage);

        gameStartButton.interactable = false;

        gameStartButton.onClick.AddListener(OnClickGameStart);

        selectWeaponDetail.sendChooseWeaponManager(this);      //SelectWeaponDetailにこの情報を送る

        selectStageDetail.sendChooseSceneManager(this);     //SelectStageDetailにも送る
    }


    //--------------------------------------武器のウィンドウに対しての処理---------------------------------------------------//

    /// <summary>
    /// メニューが開く
    /// </summary>
    /// <param name="chooseSlotType"></param>
    private void ChooseWeapon(WeaponSlotType chooseSlotType)
    {
        Debug.Log("ChooseWeapon始まりました。");

        //メニューを開くときの音を鳴らす
        audioSource.PlayOneShot(openMenu);

        //武器を選ぶWindowを開く（スイッチを入れて、見えなくしておいたWindowを見せる。）
        if(weaponInfo == null)
        {
            //初回だけ。２回目からはスイッチを入れたり消したりで処理する。
            //第三引数は何かの子オブジェクトの場合、特に今回はCanvasの子オブジェクトなので、falseにしないとworldspaceの座標で生成されてしまう。
            weaponInfo = Instantiate(chooseWeaponWindowPrefab,canvasTran,false);

            Debug.Log("SetUpChooseWeaponWindow試みます");

            weaponInfo.SetUpChooseWeaponWindow(this);
        }
        else
        {
            //SetActiveはゲームオブジェクトが持っているメソッドなので、一回「.gameObject」を挟む必要がある。
            weaponInfo.gameObject.SetActive(true);
        }

        //weapon1から入った場合はWeaponSlotTypeがslot1になる。
        weaponSlotType = chooseSlotType;   //enumは型から書く。
    }

    /// <summary>
    /// 「決定ボタンを」押したときの処理
    /// </summary>
    public void SubmitWeapon(WeaponData chooseWeaponData)　　　
    {
        //ボタンを選択したときの音を鳴らす
        audioSource.PlayOneShot(buttonSelectSE);

        //「決定ボタン」を押したとき、それがslot1だったその情報が「武器１」に入る
        switch (weaponSlotType)
        {
            case WeaponSlotType.slot1:
                weaponData1 = chooseWeaponData;   //今持ってきたものを入れる

                weapon1Text.text = weaponData1.weaponName;   //武器１ボタンの「武器１」という名前を選んだ武器の名称に変える。

                break;

            case WeaponSlotType.slot2:
                weaponData2 = chooseWeaponData;   //今持ってきたものを入れる

                weapon2Text.text = weaponData2.weaponName;   //武器１ボタンの「武器１」という名前を選んだ武器の名称に変える。

                break;
        }

        if (weaponData1 != null && weaponData2 != null)     //どちらも何か入っていないと出撃できないようにする
        {
            gameStartButton.interactable = true;
        }
        else
        {
            gameStartButton.interactable = false;
        }

        weaponInfo.gameObject.SetActive(false);
    }


    //--------------------------------------ステージのウィンドウに対しての処理---------------------------------------------------//

    /// <summary>
    /// メニューが開く
    /// </summary>
    private void ChooseStage()
    {
        Debug.Log("ChooseStage始まりました。");

        //メニューを開くときの音を鳴らす
        audioSource.PlayOneShot(openMenu);

        //武器を選ぶWindowを開く（スイッチを入れて、見えなくしておいたWindowを見せる。）
        if (stageInfo == null)
        {
            //初回だけ。２回目からはスイッチを入れたり消したりで処理する。
            stageInfo = Instantiate(chooseStageWindowPrefab, canvasTran, false);   //第三引数は何かの子オブジェクトの場合、特に今回はCanvasの子オブジェクトなので、falseにしないとworldspaceの座標で生成されてしまう。

            Debug.Log("SetUpChooseWeaponWindow試みます");

            stageInfo.SetUpChooseStageWindow(this);
        }
        else
        {
            stageInfo.gameObject.SetActive(true);  //SetActiveはゲームオブジェクトが持っているメソッドなので、一回「.gameObject」を挟む必要がある。
        }
    }

    /// <summary>
    /// 決定ボタンを押したときの処理
    /// </summary>
    public void SubmitStage(StageData chooseStageData)
    {
        //ボタンを選択したときの音を鳴らす
        audioSource.PlayOneShot(buttonSelectSE);

        //選ばれたステージの情報を入れる
        stageData = chooseStageData;

        //「ステージを選択してください」を、今選んだステージのサブタイトルに変更する
        stageButtonText.text = stageData.subTitle.ToString();

        //ステージの下の説明欄の出てくる数を更新する
        totalDinosaurCount.text = stageData.totalDinosaurCount.ToString();

        //ステージの下の説明欄の出てくる数を更新する
        totalInsectCount.text = stageData.totalInsectCount.ToString();

        //今の選択画面を見えなくする
        stageInfo.gameObject.SetActive(false);
    }


    /// <summary>
    /// 出撃ボタンを押したときの処理
    /// </summary>
    private void OnClickGameStart()
    {
        //出撃ボタンを選択したときの音を鳴らす
        audioSource.PlayOneShot(syutugekiButtonSE);

        GameData.instance.AddWeaponData(weaponData1);

        GameData.instance.AddWeaponData(weaponData2);

        GameData.instance.AddStageData(stageData);

        SceneManager.LoadScene("GameScene");
    }
}