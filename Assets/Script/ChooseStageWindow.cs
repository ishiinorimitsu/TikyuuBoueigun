using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseStageWindow : MonoBehaviour
{
    public Button btnSubmit;�@�@//����{�^��������B

    [SerializeField]
    private SelectStageDetail selectWeaponDetailPrefab;    //���ۂɉ����{�^��������

    [SerializeField]
    private Transform selectStageDetailTran;

    [SerializeField]
    private ChooseSceneManager chooseSceneManager;

    [SerializeField]
    private StageData chooseStageData;

    [SerializeField]
    private Text stageName;    //�X�e�[�W�̖��O������

    [SerializeField]
    private Text dinosaurCount;    //�o�Ă��鋰���̐�������

    [SerializeField]
    private Text insectCount;�@�@//�o�Ă��鍩���̐�������

    public void SetUpChooseStageWindow(ChooseSceneManager chooseSceneManager)   //����̃{�^���̐���
    {
        this.chooseSceneManager = chooseSceneManager;

        for (int i = 0; i < DataBaseManager.instance.stageDataSO.StageDataList.Count; i++)   
        {
            //�{�^���̃Q�[���I�u�W�F�N�g�𐶐�����
            SelectStageDetail selectWeaponDetail = Instantiate(selectWeaponDetailPrefab, selectStageDetailTran, false);

            //�{�^���ɏ�����ǉ�����
            selectWeaponDetail.SetUpSelectStageDetail(this, DataBaseManager.instance.stageDataSO.StageDataList[i]);
        }

        btnSubmit.onClick.AddListener(() => chooseSceneManager.SubmitStage(chooseStageData));   //AddListener�͈���������Ƃ��͂��̌`�ɂ���B
    }


    /// <summary>
    /// �I�񂾃{�^���̃X�e�[�W�̏������Ƃɐ������Ȃǂ�ς���
    /// </summary>
    public void SetSelectStageDetail(StageData stageData)
    {
        chooseStageData = stageData;

        stageName.text = stageData.subTitle.ToString();

        dinosaurCount.text = stageData.totalDinosaurCount.ToString();

        insectCount.text = stageData.totalInsectCount.ToString();
    }
}
