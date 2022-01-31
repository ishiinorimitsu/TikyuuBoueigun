using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseSceneManager : MonoBehaviour
{

    [SerializeField]
    private Transform canvasTran;    //�|�b�v�A�b�v�𐶐�����ʒu�iCanvas�̎q�I�u�W�F�N�g�ɂ���BTransform�^���Ɛe�q�֌W���ł���j

    //-----------------------------------�����I�ԃE�B���h�E�̏���----------------------------------------//

    [SerializeField]
    private SelectWeaponDetail selectWeaponDetail;    //���ۂɉ�������̃{�^��������

    [SerializeField]
    private Button weapon1;�@�@�@//�������������畐���I�ԃE�B���h�E���J���i����P�j

    [SerializeField]
    private Text weapon1Text;

    [SerializeField]
    private Button weapon2;�@�@�@//�������������畐���I�ԃE�B���h�E���J���i����Q�j

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
    private Button gameStartButton;   //�o������{�^��

    public enum WeaponSlotType
    {
        slot1,
        slot2
    }

    public WeaponSlotType weaponSlotType;  //���͂ǂ����̃{�^�����痈����

    //------------------------------------------�X�e�[�W��I�Ԏ��̏���------------------------------------------------//

    [SerializeField]
    private SelectStageDetail selectStageDetail;    //���ۂɉ����X�e�[�W�̃{�^��������

    [SerializeField]
    private Button stageButton;�@�@�@//��������������X�e�[�W��I�ԃE�B���h�E���J��

    [SerializeField]
    private Text stageButtonText;     //���stageButton�̃e�L�X�g�����i�f�t�H���g�ł́u�X�e�[�W��I�����Ă��������v�ƂȂ��Ă���j

    [SerializeField]
    private ChooseStageWindow chooseStageWindowPrefab;     //�X�e�[�W�̃|�b�v�A�b�v������

    [SerializeField]
    private ChooseStageWindow stageInfo;    //���chooseStageWindowPrefab������B���ۂɑ��삷��̂͂����ɓ��ꂽ�����i�̕�

    [SerializeField]
    private StageData stageData;   //�I�΂ꂽ�X�e�[�W�̏�������

    [SerializeField]
    private Text totalDinosaurCount;    //�o�����鋰���̐�(�X�e�[�W�̉��̐�����)

    [SerializeField]
    private Text totalInsectCount;    //�o�����鍩���̐�(�X�e�[�W�̉��̐�����)



    //------------------------------------------���ʉ��Ɋւ��鏈��-----------------------------------------------//

    public AudioSource audioSource;    //�I�[�f�B�I�\�[�X������B

    public AudioClip buttonSelectSE;     //�m�[�}���̃{�^���ȊO��SE

    [SerializeField]
    private AudioClip openMenu;     //����̃��j���[���J����

    [SerializeField]
    private AudioClip syutugekiButtonSE;      //�o���{�^����SE



    private void Start()
    {
        //---------------------------------����-----------------------------------------//

        weapon1.onClick.AddListener(() => ChooseWeapon(WeaponSlotType.slot1));

        weapon2.onClick.AddListener(() => ChooseWeapon(WeaponSlotType.slot2));

        weaponData1 = null;
        weaponData2 = null;    //����2�s���Ȃ���[SerializedField]������ƁAnull�ɂȂ�Ȃ�

        //----------------------------------�X�e�[�W-----------------------------------//

        stageButton.onClick.AddListener(ChooseStage);

        gameStartButton.interactable = false;

        gameStartButton.onClick.AddListener(OnClickGameStart);

        selectWeaponDetail.sendChooseWeaponManager(this);      //SelectWeaponDetail�ɂ��̏��𑗂�

        selectStageDetail.sendChooseSceneManager(this);     //SelectStageDetail�ɂ�����
    }


    //--------------------------------------����̃E�B���h�E�ɑ΂��Ă̏���---------------------------------------------------//

    /// <summary>
    /// ���j���[���J��
    /// </summary>
    /// <param name="chooseSlotType"></param>
    private void ChooseWeapon(WeaponSlotType chooseSlotType)
    {
        Debug.Log("ChooseWeapon�n�܂�܂����B");

        //���j���[���J���Ƃ��̉���炷
        audioSource.PlayOneShot(openMenu);

        //�����I��Window���J���i�X�C�b�`�����āA�����Ȃ����Ă�����Window��������B�j
        if(weaponInfo == null)
        {
            //���񂾂��B�Q��ڂ���̓X�C�b�`����ꂽ���������ŏ�������B
            //��O�����͉����̎q�I�u�W�F�N�g�̏ꍇ�A���ɍ����Canvas�̎q�I�u�W�F�N�g�Ȃ̂ŁAfalse�ɂ��Ȃ���worldspace�̍��W�Ő�������Ă��܂��B
            weaponInfo = Instantiate(chooseWeaponWindowPrefab,canvasTran,false);

            Debug.Log("SetUpChooseWeaponWindow���݂܂�");

            weaponInfo.SetUpChooseWeaponWindow(this);
        }
        else
        {
            //SetActive�̓Q�[���I�u�W�F�N�g�������Ă��郁�\�b�h�Ȃ̂ŁA���u.gameObject�v�����ޕK�v������B
            weaponInfo.gameObject.SetActive(true);
        }

        //weapon1����������ꍇ��WeaponSlotType��slot1�ɂȂ�B
        weaponSlotType = chooseSlotType;   //enum�͌^���珑���B
    }

    /// <summary>
    /// �u����{�^�����v�������Ƃ��̏���
    /// </summary>
    public void SubmitWeapon(WeaponData chooseWeaponData)�@�@�@
    {
        //�{�^����I�������Ƃ��̉���炷
        audioSource.PlayOneShot(buttonSelectSE);

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


    //--------------------------------------�X�e�[�W�̃E�B���h�E�ɑ΂��Ă̏���---------------------------------------------------//

    /// <summary>
    /// ���j���[���J��
    /// </summary>
    private void ChooseStage()
    {
        Debug.Log("ChooseStage�n�܂�܂����B");

        //���j���[���J���Ƃ��̉���炷
        audioSource.PlayOneShot(openMenu);

        //�����I��Window���J���i�X�C�b�`�����āA�����Ȃ����Ă�����Window��������B�j
        if (stageInfo == null)
        {
            //���񂾂��B�Q��ڂ���̓X�C�b�`����ꂽ���������ŏ�������B
            stageInfo = Instantiate(chooseStageWindowPrefab, canvasTran, false);   //��O�����͉����̎q�I�u�W�F�N�g�̏ꍇ�A���ɍ����Canvas�̎q�I�u�W�F�N�g�Ȃ̂ŁAfalse�ɂ��Ȃ���worldspace�̍��W�Ő�������Ă��܂��B

            Debug.Log("SetUpChooseWeaponWindow���݂܂�");

            stageInfo.SetUpChooseStageWindow(this);
        }
        else
        {
            stageInfo.gameObject.SetActive(true);  //SetActive�̓Q�[���I�u�W�F�N�g�������Ă��郁�\�b�h�Ȃ̂ŁA���u.gameObject�v�����ޕK�v������B
        }
    }

    /// <summary>
    /// ����{�^�����������Ƃ��̏���
    /// </summary>
    public void SubmitStage(StageData chooseStageData)
    {
        //�{�^����I�������Ƃ��̉���炷
        audioSource.PlayOneShot(buttonSelectSE);

        //�I�΂ꂽ�X�e�[�W�̏�������
        stageData = chooseStageData;

        //�u�X�e�[�W��I�����Ă��������v���A���I�񂾃X�e�[�W�̃T�u�^�C�g���ɕύX����
        stageButtonText.text = stageData.subTitle.ToString();

        //�X�e�[�W�̉��̐������̏o�Ă��鐔���X�V����
        totalDinosaurCount.text = stageData.totalDinosaurCount.ToString();

        //�X�e�[�W�̉��̐������̏o�Ă��鐔���X�V����
        totalInsectCount.text = stageData.totalInsectCount.ToString();

        //���̑I����ʂ������Ȃ�����
        stageInfo.gameObject.SetActive(false);
    }


    /// <summary>
    /// �o���{�^�����������Ƃ��̏���
    /// </summary>
    private void OnClickGameStart()
    {
        //�o���{�^����I�������Ƃ��̉���炷
        audioSource.PlayOneShot(syutugekiButtonSE);

        GameData.instance.AddWeaponData(weaponData1);

        GameData.instance.AddWeaponData(weaponData2);

        GameData.instance.AddStageData(stageData);

        SceneManager.LoadScene("GameScene");
    }
}