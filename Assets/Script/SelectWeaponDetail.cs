using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeaponDetail : MonoBehaviour    //�쐬���镐��̃{�^���B���̃X�N���v�g�ɂ́A���̃{�^���̐ݒ�i�������Ƃ��̑���Ȃǁj�������B���ۂɍ��̂�ChooseWeaponWindow�X�N���v�g�̒��B
{
    [SerializeField]
    private ChooseWeaponManager chooseWeaponManager;

    private WeaponData weaponData;   //�쐬����{�^���̃f�[�^������B

    private ChooseWeaponWindow chooseWeaponWindow;  //���̃{�^����ChooseWeaponWindow�̒��ɂ���B

    [SerializeField]
    private Image weaponImage;   //�����摜�f�[�^���u�����ɕ\�������܂���v�ƁA���g��Image�R���|�[�l���g��������B

    [SerializeField]
    private Button btnSelectWeaponDetail;  //�̂��Ɂu���̃{�^�����������Ƃ��̃��\�b�h�v��ǉ����邽�߁A���g��Button�R���|�[�l���g��������B

    public void SetUpSelectWeaponDetail(ChooseWeaponWindow chooseWeaponWindow,WeaponData weaponData)
    {
        Debug.Log("SetUpSelectWeaponDetail����܂��B");

        this.chooseWeaponWindow = chooseWeaponWindow;   //�����Ŏ����Ă������̂�������

        Debug.Log("�P");

        this.weaponData = weaponData;   //�����Ŏ����Ă������̂�������
        Debug.Log("�P");
        ChangeActiveButton(false);      //�{�^���Ƀ��\�b�h��ǉ����郁�\�b�h�̏������ł���܂Ń{�^���������Ȃ�����
        Debug.Log("�P");
        weaponImage.sprite = this.weaponData.weaponSprite;   //Image�ɂ���摜���{�^���ɕ\������B
        Debug.Log("�P");
        btnSelectWeaponDetail.onClick.AddListener(OnClickSelectCharaDetail);   //�{�^�����������Ƃ��̏���
        Debug.Log("�P");
        ChangeActiveButton(true);       //�{�^���Ƀ��\�b�h��ǉ����郁�\�b�h�̏������ł�����{�^����������悤�ɂ���
    }

    private void OnClickSelectCharaDetail()
    {
        chooseWeaponManager.audioSource.PlayOneShot(chooseWeaponManager.buttonSelectSE);

        chooseWeaponWindow.SetSelectWeaponDetail(weaponData);    //chooseWeaponWindow���̏����𔭓��B�������{�^���̏����Z�b�g����B
    }

    public void sendChooseWeaponManager(ChooseWeaponManager chooseWeaponManager)
    {
        this.chooseWeaponManager = chooseWeaponManager;
    }

    private void ChangeActiveButton(bool isSwitch)
    {
        btnSelectWeaponDetail.interactable = isSwitch;   //�����邩�����Ȃ��������Ƃ��͂����ɍ�p������B
    }
}
