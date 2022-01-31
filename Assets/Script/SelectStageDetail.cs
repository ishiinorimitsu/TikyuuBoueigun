using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStageDetail : MonoBehaviour
{
    [SerializeField]    //このserializeFieldがないとエラーになる
    private ChooseSceneManager chooseSceneManager;

    private ChooseStageWindow chooseStageWindow;

    private StageData stageData;

    [SerializeField]
    private Button btnSelectStageDetail;    //自分自身を入れる（のちに「このボタンを押したとき」というメソッドで使う）

    [SerializeField]
    private Text btnSelectStageDetailText;    //このボタンのテキスト。ステージ名が入る。

    public void SetUpSelectStageDetail(ChooseStageWindow chooseStageWindow,StageData stageData)
    {
        this.chooseStageWindow = chooseStageWindow;

        this.stageData = stageData;

        ChangeActiveButton(false);   //準備が完了するまで押せないようにする

        //ボタンのステージ名を変える
        btnSelectStageDetailText.text = this.stageData.subTitle.ToString();

        //このボタンを押したときの動きを追加する（効果音を鳴らし、押したボタンの情報をchooseStageWindowに送る）
        btnSelectStageDetail.onClick.AddListener(OnClickSelectCharaDetail);

        ChangeActiveButton(true);   //準備が完了したら押せるようにする
    }

    private void ChangeActiveButton(bool isSwitch)
    {
        btnSelectStageDetail.interactable = isSwitch;
    }


    /// <summary>
    /// ステージを選ぶボタン（このボタン）を押したときのメソッド
    /// </summary>
    private void OnClickSelectCharaDetail()
    {
        //ボタンを押したときの効果音を鳴らす
        chooseSceneManager.audioSource.PlayOneShot(chooseSceneManager.buttonSelectSE);

        //選んだステージの情報をchooseStageWindowに送る
        chooseStageWindow.SetSelectStageDetail(stageData);
    }


    /// <summary>
    /// ChooseSceneManagerを持ってくる
    /// </summary>
    /// <param name="chooseSceneManager"></param>
    public void sendChooseSceneManager(ChooseSceneManager chooseSceneManager)
    {
        this.chooseSceneManager = chooseSceneManager;
    }
}
