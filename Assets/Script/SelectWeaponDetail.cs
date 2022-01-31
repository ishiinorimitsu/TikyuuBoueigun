using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeaponDetail : MonoBehaviour    //作成する武器のボタン。このスクリプトには、一つ一つのボタンの設定（押したときの操作など）を書く。実際に作るのはChooseWeaponWindowスクリプトの中。
{
    [SerializeField]    //このserializeFieldがないとエラーになる
    private ChooseSceneManager chooseSceneManager;

    private WeaponData weaponData;   //作成するボタンのデータが入る。

    private ChooseWeaponWindow chooseWeaponWindow;  //このボタンはChooseWeaponWindowの中にある。

    [SerializeField]
    private Image weaponImage;   //得た画像データを「ここに表示させますよ」と、自身のImageコンポーネントを代入する。

    [SerializeField]
    private Button btnSelectWeaponDetail;  //のちに「このボタンを押したときのメソッド」を追加するため、自身のButtonコンポーネントを代入する。

    /// <summary>
    /// 各武器ボタンに武器の情報を連携させる（for文の中で一つずつ呼び出している）
    /// </summary>
    /// <param name="chooseWeaponWindow"></param>
    /// <param name="weaponData"></param>
    public void SetUpSelectWeaponDetail(ChooseWeaponWindow chooseWeaponWindow,WeaponData weaponData)
    {
        this.chooseWeaponWindow = chooseWeaponWindow;   //引数で持ってきたものを代入する

        this.weaponData = weaponData;   //引数で持ってきたものを代入する
        
        ChangeActiveButton(false);      //ボタンにメソッドを追加するメソッドの準備ができるまでボタンを押せなくする
        
        weaponImage.sprite = this.weaponData.weaponSprite;   //Imageにある画像をボタンに表示する。
        
        btnSelectWeaponDetail.onClick.AddListener(OnClickSelectCharaDetail);   //ボタンを押したときの処理
        
        ChangeActiveButton(true);       //ボタンにメソッドを追加するメソッドの準備ができたらボタンを押せるようにする
    }

    private void OnClickSelectCharaDetail()
    {
        //ボタンを押したときの効果音を鳴らす
        chooseSceneManager.audioSource.PlayOneShot(chooseSceneManager.buttonSelectSE);

        //chooseWeaponWindow内の処理を発動。押したボタンの情報をセットする。
        chooseWeaponWindow.SetSelectWeaponDetail(weaponData);
    }

    public void sendChooseWeaponManager(ChooseSceneManager chooseSceneManager)
    {
        this.chooseSceneManager = chooseSceneManager;
    }

    private void ChangeActiveButton(bool isSwitch)
    {
        btnSelectWeaponDetail.interactable = isSwitch;   //押せるか押せないかをやるときはここに作用させる。
    }
}
