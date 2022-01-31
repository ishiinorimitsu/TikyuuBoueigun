using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStageDetail : MonoBehaviour
{
    [SerializeField]    //����serializeField���Ȃ��ƃG���[�ɂȂ�
    private ChooseSceneManager chooseSceneManager;

    private ChooseStageWindow chooseStageWindow;

    private StageData stageData;

    [SerializeField]
    private Button btnSelectStageDetail;    //�������g������i�̂��Ɂu���̃{�^�����������Ƃ��v�Ƃ������\�b�h�Ŏg���j

    [SerializeField]
    private Text btnSelectStageDetailText;    //���̃{�^���̃e�L�X�g�B�X�e�[�W��������B

    public void SetUpSelectStageDetail(ChooseStageWindow chooseStageWindow,StageData stageData)
    {
        this.chooseStageWindow = chooseStageWindow;

        this.stageData = stageData;

        ChangeActiveButton(false);   //��������������܂ŉ����Ȃ��悤�ɂ���

        //�{�^���̃X�e�[�W����ς���
        btnSelectStageDetailText.text = this.stageData.subTitle.ToString();

        //���̃{�^�����������Ƃ��̓�����ǉ�����i���ʉ���炵�A�������{�^���̏���chooseStageWindow�ɑ���j
        btnSelectStageDetail.onClick.AddListener(OnClickSelectCharaDetail);

        ChangeActiveButton(true);   //���������������牟����悤�ɂ���
    }

    private void ChangeActiveButton(bool isSwitch)
    {
        btnSelectStageDetail.interactable = isSwitch;
    }


    /// <summary>
    /// �X�e�[�W��I�ԃ{�^���i���̃{�^���j���������Ƃ��̃��\�b�h
    /// </summary>
    private void OnClickSelectCharaDetail()
    {
        //�{�^�����������Ƃ��̌��ʉ���炷
        chooseSceneManager.audioSource.PlayOneShot(chooseSceneManager.buttonSelectSE);

        //�I�񂾃X�e�[�W�̏���chooseStageWindow�ɑ���
        chooseStageWindow.SetSelectStageDetail(stageData);
    }


    /// <summary>
    /// ChooseSceneManager�������Ă���
    /// </summary>
    /// <param name="chooseSceneManager"></param>
    public void sendChooseSceneManager(ChooseSceneManager chooseSceneManager)
    {
        this.chooseSceneManager = chooseSceneManager;
    }
}
