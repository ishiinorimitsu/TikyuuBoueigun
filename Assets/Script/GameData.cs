using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̃X�N���v�g�́AGameScene�Ɏ����Ă������́i������������A�I�񂾃X�e�[�W�Ȃǁj
public class GameData : MonoBehaviour�@�@�@//���̃X�N���v�g�̓V�[���𒴂��Ă����Ȃ��悤�Ȃ��́A�Ⴆ�΃o�g���X�e�[�W�ɓ���O�ɕ����I��Ŏ����Ă��������A���邢�̓o�g�����I����ĐV�����������ɓ���鏈���Ȃǂ������B
{
    public static GameData instance;   //�����̃X�N���v�g���ǂ�����ł��g����悤�ɂ���B

    public StageData currentStageData;    //�I�����ꂽ�X�e�[�W������B

    public List<WeaponData> chooseWeaponData = new List<WeaponData>();    //�I�񂾕��������

    public WeaponData equipWeaponData;   //�������Ă��镐��i�������畐��̏��𓾂�j

    public int currentEquipWeaponNo;   //���ݑ������Ă��镐���No,�؂�ւ���Ƃ��Ɏg���B

    private List<float> currentBulletList = new List<float>();

    //private float currentBullet;    //���̒e��������

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //private void Start()
    //{
    //    chooseWeaponData = DataBaseManager.instance.weaponDataSO.weaponDataList;

    //    equipWeaponData = chooseWeaponData[1];
    //}


    /// <summary>
    /// �I�񂾕�������̃V�[���Ɏ����Ă������߂Ɉ�񂱂̃X�N���v�g�Ɏ����Ă���
    /// </summary>
    public void AddWeaponData(WeaponData weaponData)
    {
        chooseWeaponData.Add(weaponData);

        equipWeaponData = chooseWeaponData[0];
    }

    /// <summary>
    /// �X�e�[�W�̃f�[�^�����̃X�N���v�g�Ɏ����Ă���
    /// </summary>
    /// <param name="stageData"></param>
    public void AddStageData(StageData stageData)
    {
        this.currentStageData = stageData;
    }

    public void ChangeWeapon()
    {
        currentEquipWeaponNo++;  //���݂̕����No���P�����₷�B

        currentEquipWeaponNo = currentEquipWeaponNo % chooseWeaponData.Count;�@�@//��������X�g�̍ő�l�Ŋ���(���̏ꍇ0��1�ɂȂ�)

        equipWeaponData = chooseWeaponData[currentEquipWeaponNo];�@�@//����No�����X�g���̂��̂Əƍ�����B

        Debug.Log(equipWeaponData.maxBullet);
    }
}
