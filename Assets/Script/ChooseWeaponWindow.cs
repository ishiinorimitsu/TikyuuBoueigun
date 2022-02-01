using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponWindow : MonoBehaviour�@�@//���̃X�N���v�g�ł̓{�^���̍쐬�A�����ꂽ�{�^���̓�����\�����郁�\�b�h�Ȃǂ������B
{
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
    private Transform selectWeaponDetailTran;   //�L�����̃{�^���𐶐�����ʒu���A�T�C������B

    [SerializeField]
    private WeaponData chooseWeaponData;   //���ݑI�����Ă���L�����̏����Ǘ�����

    [SerializeField]
    private ChooseSceneManager chooseWeaponManager;    //ChooseWeaponManager������

    /// <summary>
    /// ���̃X�N���v�g��Start���\�b�h�ւ��
    /// </summary>
    /// <param name="chooseSceneManager"></param>
    public void SetUpChooseWeaponWindow(ChooseSceneManager chooseSceneManager)   //����̃{�^���̐���
    {
        this.chooseWeaponManager = chooseSceneManager;

        chooseWeaponData = null;     //�ŏ��́u���ݑI��ł��镐��v����ɂ���

        btnSubmit.interactable = false;    //���������I��ł��Ȃ���Ԃł́u����v�{�^���������Ȃ�

        //weapoList�̌������ƂɃ{�^�������B
        for (int i = 0; i < DataBaseManager.instance.weaponDataSO.weaponDataList.Count; i++)
        {
            //�{�^���̃Q�[���I�u�W�F�N�g�𐶐�����
            SelectWeaponDetail selectWeaponDetail = Instantiate(selectWeaponDetailPrefab, selectWeaponDetailTran, false);
            
            //�{�^���ɏ�����ǉ�����
            selectWeaponDetail.SetUpSelectWeaponDetail(this,DataBaseManager.instance.weaponDataSO.weaponDataList[i]);
        }

        btnSubmit.onClick.AddListener(() => chooseSceneManager.SubmitWeapon(chooseWeaponData));   //AddListener�͈���������Ƃ��͂��̌`�ɂ���B
    }

    /// <summary>
    /// �I�񂾕���̏������Ƃɐ�������ς��郁�\�b�h
    /// </summary>
    /// <param name="weaponData"></param>
    public void SetSelectWeaponDetail(WeaponData weaponData)
    {
        chooseWeaponData = weaponData;     //���I�����Ă��镐��ɁA�N���b�N���ꂽ�{�^�������͂����B

        imgPickupWeapon.sprite = weaponData.weaponSprite;     //���I�����Ă��镐��̉摜�ɁA�N���b�N���ꂽ�{�^���̉摜�����͂����B

        txtPickupWeaponName.text = weaponData.weaponName;     //���I�����Ă��镐��̂ɁA���O�N���b�N���ꂽ�{�^���̖��O�����͂����B

        txtPickupWeaponAttackPower.text = weaponData.weaponAttackPower.ToString();     //���I�����Ă��镐��̉摜�ɁA�N���b�N���ꂽ�{�^���̉摜�����͂����B

        txtPickupWeaponAttackRangeType.text = weaponData.attackRange.ToString();     //���I�����Ă��镐��̉摜�ɁA�N���b�N���ꂽ�{�^���̉摜�����͂����B

        txtPickupWeaponMaxShot.text = weaponData.maxBullet.ToString();     //���I�����Ă��镐��̍ő�e���ɁA�N���b�N���ꂽ�{�^���̍ő�e�������͂����B

        txtWeaponDescription.text = weaponData.discription;     //���I�����Ă��镐��̐����ɁA�N���b�N���ꂽ�{�^���̐��������͂����B

        btnSubmit.interactable = true;    //�������炪�����ꂽ�Ƃ������ƂȂ̂ŁA�u����v�{�^����������悤�ɂ���
    }
}
