using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseStageWindow : MonoBehaviour
{
    public Button btnSubmit;　　//決定ボタンを入れる。

    [SerializeField]
    private SelectStageDetail selectWeaponDetailPrefab;    //実際に押すボタンを入れる

    [SerializeField]
    private Transform selectStageDetailTran;

    [SerializeField]
    private ChooseSceneManager chooseSceneManager;

    [SerializeField]
    private StageData chooseStageData;

    [SerializeField]
    private Text stageName;    //ステージの名前を入れる

    [SerializeField]
    private Text dinosaurCount;    //出てくる恐竜の数を入れる

    [SerializeField]
    private Text insectCount;　　//出てくる昆虫の数を入れる

    public void SetUpChooseStageWindow(ChooseSceneManager chooseSceneManager)   //武器のボタンの生成
    {
        this.chooseSceneManager = chooseSceneManager;

        for (int i = 0; i < DataBaseManager.instance.stageDataSO.StageDataList.Count; i++)   
        {
            //ボタンのゲームオブジェクトを生成する
            SelectStageDetail selectWeaponDetail = Instantiate(selectWeaponDetailPrefab, selectStageDetailTran, false);

            //ボタンに処理を追加する
            selectWeaponDetail.SetUpSelectStageDetail(this, DataBaseManager.instance.stageDataSO.StageDataList[i]);
        }

        btnSubmit.onClick.AddListener(() => chooseSceneManager.SubmitStage(chooseStageData));   //AddListenerは引数があるときはこの形にする。
    }


    /// <summary>
    /// 選んだボタンのステージの情報をもとに説明欄などを変える
    /// </summary>
    public void SetSelectStageDetail(StageData stageData)
    {
        chooseStageData = stageData;

        stageName.text = stageData.subTitle.ToString();

        dinosaurCount.text = stageData.totalDinosaurCount.ToString();

        insectCount.text = stageData.totalInsectCount.ToString();
    }
}
