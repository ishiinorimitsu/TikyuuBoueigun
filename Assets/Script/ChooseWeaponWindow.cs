using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponWindow : MonoBehaviour�@�@//���̃X�N���v�g�ł̓{�^���̍쐬�A�����ꂽ�{�^���̓�����\�����郁�\�b�h�Ȃǂ������B
{
    [SerializeField]
    private Button btnSubmit;�@�@//����{�^��������B

    [SerializeField]
    private CanvasGroup canvasGroup; �@ //����CanvasGroup��alpha�𒲐߂��邱�ƂŌ�����悤��/�����Ȃ��悤�ɂ���B

    [SerializeField]
    private Image imgPickupChara;�@�@//���ݑI�΂�Ă��镐��̉摜������

    [SerializeField]
    private Text txtPickupWeaponName;�@�@//���ݑI�΂�Ă��镐��̖��O������@

    [SerializeField]
    private Text txtPickupWeaponAttackPower;�@�@//���ݑI�΂�Ă��镐��̍U���͂�����

    [SerializeField]
    private Text txtPickupWeaponAttackRangeType;�@�@//���ݑI�΂�Ă��镐��̉摜�˒�������

    [SerializeField]
    private Text txtPickupWeaponMaxShot;�@�@//���ݑI�΂�Ă��镐��̍ő�e��������

    [SerializeField]
    private Text txtWeaponDescription;   //���ݑI������Ă��镐��̐���������B

    //[SerializeField]
    //private SelectCharaDetail selectCharaDetailPrefab;    //�L������I������{�^���p��Prefab���A�T�C������B

    //[SerializeField]
    //private Transform selectCharaDetailTran;   //�L�����̃{�^���𐶐�����ʒu���A�T�C������B

    //[SerializeField]
    //private List<SelectCharaDetail> selectCharaDetailslList = new List<SelectCharaDetail>();  //��������L�����̃{�^�����Ǘ�����

    //[SerializeField]
    //private CharaData chooseCharaData;   //���ݑI�����Ă���L�����̏����Ǘ�����

    //private CharaGenerator charaGenerator;

    public void SetUpChooseWeaponWindow()   //����̃{�^���̐���
    {

    }
}
