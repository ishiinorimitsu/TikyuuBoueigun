using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeaponDetail : MonoBehaviour    //�쐬���镐��̃{�^���B���̃X�N���v�g�ɂ́A���̃{�^���̐ݒ�i�������Ƃ��̑���Ȃǁj�������B���ۂɍ��̂�ChooseWeaponWindow�X�N���v�g�̒��B
{
    private WeaponData weaponData;   //�쐬����{�^���̃f�[�^������B

    private ChooseWeaponWindow chooseWeaponInfo;  //���̃{�^����ChooseWeaponWindow�̒��ɂ���B

    [SerializeField]
    private Image charaImage;   //�����摜�f�[�^���u�����ɕ\�������܂���v�ƁA���g��Image�R���|�[�l���g��������B

    [SerializeField]
    private Button btnSelectCharaDetail;  //�̂��Ɂu���̃{�^�����������Ƃ��̃��\�b�h�v��ǉ����邽�߁A���g��Button�R���|�[�l���g��������B

    public void SetUpSelectCharaDetail(ChooseWeaponWindow chooseWeaponWindow,WeaponData weaponData)
    {
        this.chooseWeaponInfo = chooseWeaponWindow;   //�����Ŏ����Ă������̂�������

        this.weaponData = weaponData;   //�����Ŏ����Ă������̂�������

        ChangeActiveButton(false);      //�{�^���Ƀ��\�b�h��ǉ����郁�\�b�h�̏������ł���܂Ń{�^���������Ȃ�����

        charaImage.sprite = this.weaponData.charaSprite;   //Image�ɂ���摜���{�^���ɕ\������B

        //btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);   //�{�^�����������Ƃ��̏���

        ChangeActiveButton(true);       //�{�^���Ƀ��\�b�h��ǉ����郁�\�b�h�̏������ł�����{�^����������悤�ɂ���
    }

    //private void OnClickSelectCharaDetail()
    //{
    //    WeaponInfo.SetSelectCharaDetail(charaData);    //chooseWeaponWindow���̏����𔭓��B�������{�^���̏����Z�b�g����B
    //}

    private void ChangeActiveButton(bool isSwitch)
    {
        btnSelectCharaDetail.interactable = isSwitch;   //�����邩�����Ȃ��������Ƃ��͂����ɍ�p������B
    }
}
