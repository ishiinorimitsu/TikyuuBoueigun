using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseWeaponManager : MonoBehaviour
{
    [SerializeField]
    private Button weapon1;

    [SerializeField]
    private Text weapon1Text;

    [SerializeField]
    private Button weapon2;

    [SerializeField]
    private Text weapon2Text;

    [SerializeField]
    private WeaponData weaponData1;    //���I�΂�Ă��镐��P�̏�������i�I�Ԃ��тɏ㏑������B�o������Ƃ���GameData��WeaponDataList�ɓ����B�j

    [SerializeField]
    private WeaponData weaponData2;    //���I�΂�Ă��镐��Q�̏�������i�I�Ԃ��тɏ㏑������B�o������Ƃ���GameData��WeaponDataList�ɓ����B�j

    [SerializeField]
    private ChooseWeaponWindow weaponInfo;    //�\�������肷��p�ɍ���Ă���(chooseWeaponWindowPrefab���������肷��)

    [SerializeField]
    private ChooseWeaponWindow chooseWeaponWindowPrefab;   //��������|�b�v�A�b�v�̃v���t�@�u

    [SerializeField]
    private Transform canvasTran;    //�|�b�v�A�b�v�𐶐�����ʒu�iCanvas�̎q�I�u�W�F�N�g�ɂ���BTransform�^���Ɛe�q�֌W���ł���j

    [SerializeField]
    private Button gameStartButton;   //�o������{�^��

    public enum WeaponSlotType{
        slot1,
        slot2
    }

    public WeaponSlotType weaponSlotType;  //���͂ǂ����̃{�^�����痈����


    private void Start()
    {
        weapon1.onClick.AddListener(() => ChooseWeapon(WeaponSlotType.slot1));

        weapon2.onClick.AddListener(() => ChooseWeapon(WeaponSlotType.slot2));

        weaponData1 = null;
        weaponData2 = null;    //����2�s���Ȃ���[SerializedField]������ƁAnull�ɂȂ�Ȃ�

        gameStartButton.interactable = false;

        gameStartButton.onClick.AddListener(OnClickGameStart);
    }

    private void ChooseWeapon(WeaponSlotType chooseSlotType)
    {
        //�����I��Window���J���i�X�C�b�`�����āA�����Ȃ����Ă�����Window��������B�j
        if(weaponInfo == null)
        {
            //���񂾂��B�Q��ڂ���̓X�C�b�`����ꂽ���������ŏ�������B
            weaponInfo = Instantiate(chooseWeaponWindowPrefab,canvasTran,false);   //��O�����͉����̎q�I�u�W�F�N�g�̏ꍇ�A���ɍ����Canvas�̎q�I�u�W�F�N�g�Ȃ̂ŁAfalse�ɂ��Ȃ���worldspace�̍��W�Ő�������Ă��܂��B

            weaponInfo.SetUpChooseWeaponWindow(this);
        }
        else
        {
            weaponInfo.gameObject.SetActive(true);  //SetActive�̓Q�[���I�u�W�F�N�g�������Ă��郁�\�b�h�Ȃ̂ŁA���u.gameObject�v�����ޕK�v������B
        }

        //weapon1����������ꍇ��WeaponSlotType��slot1�ɂȂ�B
        weaponSlotType = chooseSlotType;   //enum�͌^���珑���B
    }

    /// <summary>
    /// �u����{�^�����v�������Ƃ��̏���
    /// </summary>
    public void SubmitWeapon(WeaponData chooseWeaponData)�@�@�@
    {
        //�u����{�^���v���������Ƃ��A���ꂪslot1���������̏�񂪁u����P�v�ɓ���
        switch (weaponSlotType)
        {
            case WeaponSlotType.slot1:
                weaponData1 = chooseWeaponData;   //�������Ă������̂�����

                weapon1Text.text = weaponData1.weaponName;   //����P�{�^���́u����P�v�Ƃ������O��I�񂾕���̖��̂ɕς���B

                break;

            case WeaponSlotType.slot2:
                weaponData2 = chooseWeaponData;   //�������Ă������̂�����

                weapon2Text.text = weaponData2.weaponName;   //����P�{�^���́u����P�v�Ƃ������O��I�񂾕���̖��̂ɕς���B

                break;
        }

        if (weaponData1 != null && weaponData2 != null)     //�ǂ�������������Ă��Ȃ��Əo���ł��Ȃ��悤�ɂ���
        {
            gameStartButton.interactable = true;
        }
        else
        {
            gameStartButton.interactable = false;
        }

        weaponInfo.gameObject.SetActive(false);
    }

    /// <summary>
    /// �o���{�^�����������Ƃ��̏���
    /// </summary>
    private void OnClickGameStart()
    {  
        GameData.instance.AddWeaponData(weaponData1);

        GameData.instance.AddWeaponData(weaponData2);

        SceneManager.LoadScene("GameScene");
    }
}