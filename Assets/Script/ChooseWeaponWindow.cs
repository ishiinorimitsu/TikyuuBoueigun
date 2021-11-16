using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponWindow : MonoBehaviour　　//このスクリプトではボタンの作成、押されたボタンの特徴を表示するメソッドなどを書く。
{
    [SerializeField]
    private Button btnSubmit;　　//決定ボタンを入れる。

    [SerializeField]
    private CanvasGroup canvasGroup; 　 //このCanvasGroupのalphaを調節することで見えるように/見えないようにする。

    [SerializeField]
    private Image imgPickupChara;　　//現在選ばれている武器の画像を入れる

    [SerializeField]
    private Text txtPickupWeaponName;　　//現在選ばれている武器の名前を入れる　

    [SerializeField]
    private Text txtPickupWeaponAttackPower;　　//現在選ばれている武器の攻撃力を入れる

    [SerializeField]
    private Text txtPickupWeaponAttackRangeType;　　//現在選ばれている武器の画像射程を入れる

    [SerializeField]
    private Text txtPickupWeaponMaxShot;　　//現在選ばれている武器の最大弾数を入れる

    [SerializeField]
    private Text txtWeaponDescription;   //現在選択されている武器の説明を入れる。

    //[SerializeField]
    //private SelectCharaDetail selectCharaDetailPrefab;    //キャラを選択するボタン用のPrefabをアサインする。

    //[SerializeField]
    //private Transform selectCharaDetailTran;   //キャラのボタンを生成する位置をアサインする。

    //[SerializeField]
    //private List<SelectCharaDetail> selectCharaDetailslList = new List<SelectCharaDetail>();  //生成するキャラのボタンを管理する

    //[SerializeField]
    //private CharaData chooseCharaData;   //現在選択しているキャラの情報を管理する

    //private CharaGenerator charaGenerator;

    public void SetUpChooseWeaponWindow()   //武器のボタンの生成
    {

    }
}
