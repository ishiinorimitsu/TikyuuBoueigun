using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeaponDetail : MonoBehaviour    //作成する武器のボタン。このスクリプトには、一つ一つのボタンの設定（押したときの操作など）を書く。実際に作るのはChooseWeaponWindowスクリプトの中。
{
    [SerializeField]
    private ChooseWeaponManager chooseWeaponManager;

    private WeaponData weaponData;   //作成するボタンのデータが入る。

    private ChooseWeaponWindow chooseWeaponWindow;  //このボタンはChooseWeaponWindowの中にある。

    [SerializeField]
    private Image weaponImage;   //得た画像データを「ここに表示させますよ」と、自身のImageコンポーネントを代入する。

    [SerializeField]
    private Button btnSelectWeaponDetail;  //のちに「このボタンを押したときのメソッド」を追加するため、自身のButtonコンポーネントを代入する。

    public void SetUpSelectWeaponDetail(ChooseWeaponWindow chooseWeaponWindow,WeaponData weaponData)
    {
        Debug.Log("SetUpSelectWeaponDetail入ります。");

        this.chooseWeaponWindow = chooseWeaponWindow;   //引数で持ってきたものを代入する

        Debug.Log("１");

        this.weaponData = weaponData;   //引数で持ってきたものを代入する
        Debug.Log("１");
        ChangeActiveButton(false);      //ボタンにメソッドを追加するメソッドの準備ができるまでボタンを押せなくする
        Debug.Log("１");
        weaponImage.sprite = this.weaponData.weaponSprite;   //Imageにある画像をボタンに表示する。
        Debug.Log("１");
        btnSelectWeaponDetail.onClick.AddListener(OnClickSelectCharaDetail);   //ボタンを押したときの処理
        Debug.Log("１");
        ChangeActiveButton(true);       //ボタンにメソッドを追加するメソッドの準備ができたらボタンを押せるようにする
    }

    private void OnClickSelectCharaDetail()
    {
        chooseWeaponManager.audioSource.PlayOneShot(chooseWeaponManager.buttonSelectSE);

        chooseWeaponWindow.SetSelectWeaponDetail(weaponData);    //chooseWeaponWindow内の処理を発動。押したボタンの情報をセットする。
    }

    public void sendChooseWeaponManager(ChooseWeaponManager chooseWeaponManager)
    {
        this.chooseWeaponManager = chooseWeaponManager;
    }

    private void ChangeActiveButton(bool isSwitch)
    {
        btnSelectWeaponDetail.interactable = isSwitch;   //押せるか押せないかをやるときはここに作用させる。
    }
}
