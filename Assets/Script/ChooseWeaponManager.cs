using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseWeaponManager : MonoBehaviour
{
    [SerializeField]
    private SelectWeaponDetail selectWeaponDetail;

    [SerializeField]
    private Button weapon1;

    [SerializeField]
    private Text weapon1Text;

    [SerializeField]
    private Button weapon2;

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
    private Transform canvasTran;    //ポップアップを生成する位置（Canvasの子オブジェクトにする。Transform型だと親子関係もできる）

    [SerializeField]
    private Button gameStartButton;   //出撃するボタン

    //------------------------------------------効果音に関する処理-----------------------------------------------//

    public AudioSource audioSource;    //オーディオソースを入れる。

    public AudioClip buttonSelectSE;     //ノーマルのボタン以外のSE

    [SerializeField]
    private AudioClip openWeaponMenu;     //武器のメニューを開く音

    [SerializeField]
    private AudioClip syutugekiButtonSE;      //出撃ボタンのSE



    public enum WeaponSlotType{
        slot1,
        slot2
    }

    public WeaponSlotType weaponSlotType;  //今はどっちのボタンから来たか


    private void Start()
    {
        weapon1.onClick.AddListener(() => ChooseWeapon(WeaponSlotType.slot1));

        weapon2.onClick.AddListener(() => ChooseWeapon(WeaponSlotType.slot2));

        weaponData1 = null;
        weaponData2 = null;    //この2行がないと[SerializedField]があると、nullにならない

        gameStartButton.interactable = false;

        gameStartButton.onClick.AddListener(OnClickGameStart);

        selectWeaponDetail.sendChooseWeaponManager(this);      //SelectWeaponDetailにこの情報を送る
    }

    private void ChooseWeapon(WeaponSlotType chooseSlotType)
    {
        //メニューを開くときの音を鳴らす
        audioSource.PlayOneShot(openWeaponMenu);

        //武器を選ぶWindowを開く（スイッチを入れて、見えなくしておいたWindowを見せる。）
        if(weaponInfo == null)
        {
            //初回だけ。２回目からはスイッチを入れたり消したりで処理する。
            weaponInfo = Instantiate(chooseWeaponWindowPrefab,canvasTran,false);   //第三引数は何かの子オブジェクトの場合、特に今回はCanvasの子オブジェクトなので、falseにしないとworldspaceの座標で生成されてしまう。

            weaponInfo.SetUpChooseWeaponWindow(this);
        }
        else
        {
            weaponInfo.gameObject.SetActive(true);  //SetActiveはゲームオブジェクトが持っているメソッドなので、一回「.gameObject」を挟む必要がある。
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

    /// <summary>
    /// 出撃ボタンを押したときの処理
    /// </summary>
    private void OnClickGameStart()
    {
        //出撃ボタンを選択したときの音を鳴らす
        audioSource.PlayOneShot(syutugekiButtonSE);

        GameData.instance.AddWeaponData(weaponData1);

        GameData.instance.AddWeaponData(weaponData2);

        SceneManager.LoadScene("GameScene");
    }
}