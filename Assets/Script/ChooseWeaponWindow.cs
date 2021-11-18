using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponWindow : MonoBehaviour�@�@//���̃X�N���v�g�ł̓{�^���̍쐬�A�����ꂽ�{�^���̓�����\�����郁�\�b�h�Ȃǂ������B
{
    [SerializeField]
    public Button btnSubmit;�@�@//����{�^��������B

    [SerializeField]
    private CanvasGroup canvasGroup; �@ //����CanvasGroup��alpha�𒲐߂��邱�ƂŌ�����悤��/�����Ȃ��悤�ɂ���B

    [SerializeField]
    private Image imgPickupWeapon;�@�@//���ݑI�΂�Ă��镐��̉摜������

    [SerializeField]
    private Text txtPickupWeaponName;�@�@//���ݑI�΂�Ă��镐��̖��O������@

    [SerializeField]
    private Text txtPickupWeaponAttackPower;�@�@//���ݑI�΂�Ă��镐��̍U���͂�����

    [SerializeField]
    private Text txtPickupWeaponAttackRangeType;�@�@//���ݑI�΂�Ă��镐��̎˒�������

    [SerializeField]
    private Text txtPickupWeaponMaxShot;�@�@//���ݑI�΂�Ă��镐��̍ő�e��������

    [SerializeField]
    private Text txtWeaponDescription;   //���ݑI������Ă��镐��̐���������B

    [SerializeField]
    private SelectWeaponDetail selectWeaponDetailPrefab;    //�����I������{�^���p��Prefab���A�T�C������B

    [SerializeField]
    private Transform selectCharaDetailTran;   //�L�����̃{�^���𐶐�����ʒu���A�T�C������B

    //[SerializeField]
    //private List<SelectCharaDetail> selectCharaDetailslList = new List<SelectCharaDetail>();  //��������L�����̃{�^�����Ǘ�����

    [SerializeField]
    private WeaponData chooseWeaponData;   //���ݑI�����Ă���L�����̏����Ǘ�����

    //private CharaGenerator charaGenerator;

    [SerializeField]
    private ChooseWeaponManager chooseWeaponManager;    //ChooseWeaponManager������

    public void SetUpChooseWeaponWindow(ChooseWeaponManager chooseWeaponManager)   //����̃{�^���̐���
    {
        this.chooseWeaponManager = chooseWeaponManager;

        for(int i = 0; i < 5; i++)   //�܂�5�{�^��������Ă݂�B
        {
            //�{�^���̃Q�[���I�u�W�F�N�g�𐶐�����
            SelectWeaponDetail selectWeaponDetail = Instantiate(selectWeaponDetailPrefab, selectCharaDetailTran, false);

            //�{�^���ɏ�����ǉ�����
            selectWeaponDetail.SetUpSelectWeaponDetail(this,DataBaseManager.instance.weaponDataSO.weaponDataList[i]);
        }

        btnSubmit.onClick.AddListener(() => chooseWeaponManager.SubmitWeapon(chooseWeaponData));   //AddListener�͈���������Ƃ��͂��̌`�ɂ���B
    }

    public void SetSelectWeaponDetail(WeaponData weaponData)
    {
        chooseWeaponData = weaponData;     //���I�����Ă��镐��ɁA�N���b�N���ꂽ�{�^�������͂����B

        //imgPickupWeapon.sprite = weaponData.weaponSprite;     //���I�����Ă��镐��̉摜�ɁA�N���b�N���ꂽ�{�^���̉摜�����͂����B

        txtPickupWeaponName.text = weaponData.weaponName;     //���I�����Ă��镐��̂ɁA���O�N���b�N���ꂽ�{�^���̖��O�����͂����B

        txtPickupWeaponAttackPower.text = weaponData.weaponAttackPower.ToString();     //���I�����Ă��镐��̉摜�ɁA�N���b�N���ꂽ�{�^���̉摜�����͂����B

        txtPickupWeaponAttackRangeType.text = weaponData.attackRange.ToString();     //���I�����Ă��镐��̉摜�ɁA�N���b�N���ꂽ�{�^���̉摜�����͂����B

        txtPickupWeaponMaxShot.text = weaponData.maxAttackCount.ToString();     //���I�����Ă��镐��̍ő�e���ɁA�N���b�N���ꂽ�{�^���̍ő�e�������͂����B

        txtWeaponDescription.text = weaponData.discription;     //���I�����Ă��镐��̐����ɁA�N���b�N���ꂽ�{�^���̐��������͂����B
    }
}
