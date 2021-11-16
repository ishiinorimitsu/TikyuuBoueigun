using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeaponDetail : MonoBehaviour    //作成する武器のボタン。このスクリプトには、一つ一つのボタンの設定（押したときの操作など）を書く。実際に作るのはChooseWeaponWindowスクリプトの中。
{
    private WeaponData weaponData;   //作成するボタンのデータが入る。

    private ChooseWeaponWindow chooseWeaponInfo;  //このボタンはChooseWeaponWindowの中にある。

    [SerializeField]
    private Image charaImage;   //得た画像データを「ここに表示させますよ」と、自身のImageコンポーネントを代入する。

    [SerializeField]
    private Button btnSelectCharaDetail;  //のちに「このボタンを押したときのメソッド」を追加するため、自身のButtonコンポーネントを代入する。

    public void SetUpSelectCharaDetail(ChooseWeaponWindow chooseWeaponWindow,WeaponData weaponData)
    {
        this.chooseWeaponInfo = chooseWeaponWindow;   //引数で持ってきたものを代入する

        this.weaponData = weaponData;   //引数で持ってきたものを代入する

        ChangeActiveButton(false);      //ボタンにメソッドを追加するメソッドの準備ができるまでボタンを押せなくする

        charaImage.sprite = this.weaponData.charaSprite;   //Imageにある画像をボタンに表示する。

        //btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);   //ボタンを押したときの処理

        ChangeActiveButton(true);       //ボタンにメソッドを追加するメソッドの準備ができたらボタンを押せるようにする
    }

    //private void OnClickSelectCharaDetail()
    //{
    //    WeaponInfo.SetSelectCharaDetail(charaData);    //chooseWeaponWindow内の処理を発動。押したボタンの情報をセットする。
    //}

    private void ChangeActiveButton(bool isSwitch)
    {
        btnSelectCharaDetail.interactable = isSwitch;   //押せるか押せないかをやるときはここに作用させる。
    }
}
