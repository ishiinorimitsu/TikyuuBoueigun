using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponManager : MonoBehaviour
{
    [SerializeField]
    private Button weapon1;

    [SerializeField]
    private Button weapon2;

    [SerializeField]
    private WeaponData weaponData1;    //���I�΂�Ă��镐��P�̏�������i�I�Ԃ��тɏ㏑������B�o������Ƃ���GameData��WeaponDataList�ɓ����B�j

    [SerializeField]
    private WeaponData weaponData2;    //���I�΂�Ă��镐��Q�̏�������i�I�Ԃ��тɏ㏑������B�o������Ƃ���GameData��WeaponDataList�ɓ����B�j

    [SerializeField]
    private WeaponData selectWeaponData;  //�ۗ����̃f�[�^(����������O�ɁA�I�Ԃ��тɏ�񂪍X�V����Ă���)

    [SerializeField]
    private ChooseWeaponWindow weaponInfo;    //�\�������肷��p�ɍ���Ă���

    public enum WeaponSlotType{
        slot1,
        slot2
    }

    public WeaponSlotType weaponSlotType;  //���͂ǂ����̃{�^�����痈����


    private void Start()
    {
        weapon1.onClick.AddListener(ChooseWeapon);


    }

    private void ChooseWeapon()
    {
        //�����I��Window���J���i�X�C�b�`�����āA�����Ȃ����Ă�����Window��������B�j
        //���̒����畐���I�����A����{�^����������weapon1�̂Ƃ����Text�ɑI�񂾕���̖��O������B
        //weapon1����������ꍇ��WeaponSlotType��slot1�ɂȂ�B
    }

    private void SubmitWeapon()
    {
        //�u����{�^���v���������Ƃ��A���ꂪslot1���������̏�񂪁u����P�v�ɓ���
        //�u����{�^���v���������Ƃ��A���ꂪslot2���������̏�񂪁u����2�v�ɓ���
    }
}