using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponWindow : MonoBehaviour　　//このスクリプトではボタンの作成、押されたボタンの特徴を表示するメソッドなどを書く。
{
    [SerializeField]
    public Button btnSubmit;　　//決定ボタンを入れる。

    [SerializeField]
    private CanvasGroup canvasGroup; 　 //このCanvasGroupのalphaを調節することで見えるように/見えないようにする。

    [SerializeField]
    private Image imgPickupWeapon;　　//現在選ばれている武器の画像を入れる

    [SerializeField]
    private Text txtPickupWeaponName;　　//現在選ばれている武器の名前を入れる　

    [SerializeField]
    private Text txtPickupWeaponAttackPower;　　//現在選ばれている武器の攻撃力を入れる

    [SerializeField]
    private Text txtPickupWeaponAttackRangeType;　　//現在選ばれている武器の射程を入れる

    [SerializeField]
    private Text txtPickupWeaponMaxShot;　　//現在選ばれている武器の最大弾数を入れる

    [SerializeField]
    private Text txtWeaponDescription;   //現在選択されている武器の説明を入れる。

    [SerializeField]
    private SelectWeaponDetail selectWeaponDetailPrefab;    //武器を選択するボタン用のPrefabをアサインする。

    [SerializeField]
    private Transform selectCharaDetailTran;   //キャラのボタンを生成する位置をアサインする。

    //[SerializeField]
    //private List<SelectCharaDetail> selectCharaDetailslList = new List<SelectCharaDetail>();  //生成するキャラのボタンを管理する

    [SerializeField]
    private WeaponData chooseWeaponData;   //現在選択しているキャラの情報を管理する

    //private CharaGenerator charaGenerator;

    [SerializeField]
    private ChooseWeaponManager chooseWeaponManager;    //ChooseWeaponManagerを入れる

    public void SetUpChooseWeaponWindow(ChooseWeaponManager chooseWeaponManager)   //武器のボタンの生成
    {
        this.chooseWeaponManager = chooseWeaponManager;

        for(int i = 0; i < 5; i++)   //まず5個ボタンを作ってみる。
        {
            //ボタンのゲームオブジェクトを生成する
            SelectWeaponDetail selectWeaponDetail = Instantiate(selectWeaponDetailPrefab, selectCharaDetailTran, false);

            //ボタンに処理を追加する
            selectWeaponDetail.SetUpSelectWeaponDetail(this,DataBaseManager.instance.weaponDataSO.weaponDataList[i]);
        }

        btnSubmit.onClick.AddListener(() => chooseWeaponManager.SubmitWeapon(chooseWeaponData));   //AddListenerは引数があるときはこの形にする。
    }

    public void SetSelectWeaponDetail(WeaponData weaponData)
    {
        chooseWeaponData = weaponData;     //今選択している武器に、クリックされたボタンが入力される。

        //imgPickupWeapon.sprite = weaponData.weaponSprite;     //今選択している武器の画像に、クリックされたボタンの画像が入力される。

        txtPickupWeaponName.text = weaponData.weaponName;     //今選択している武器のに、名前クリックされたボタンの名前が入力される。

        txtPickupWeaponAttackPower.text = weaponData.weaponAttackPower.ToString();     //今選択している武器の画像に、クリックされたボタンの画像が入力される。

        txtPickupWeaponAttackRangeType.text = weaponData.attackRange.ToString();     //今選択している武器の画像に、クリックされたボタンの画像が入力される。

        txtPickupWeaponMaxShot.text = weaponData.maxAttackCount.ToString();     //今選択している武器の最大弾数に、クリックされたボタンの最大弾数が入力される。

        txtWeaponDescription.text = weaponData.discription;     //今選択している武器の説明に、クリックされたボタンの説明が入力される。
    }
}
