using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour�@�@�@//���̃X�N���v�g�̓V�[���𒴂��Ă����Ȃ��悤�Ȃ��́A�Ⴆ�΃o�g���X�e�[�W�ɓ���O�ɕ����I��Ŏ����Ă��������A���邢�̓o�g�����I����ĐV�����������ɓ���鏈���Ȃǂ������B
{
    public static GameData instance;   //�����̃X�N���v�g���ǂ�����ł��g����悤�ɂ���B

    public List<WeaponData> chooseWeaponData = new List<WeaponData>();    //�I�񂾕��������

    public WeaponData equipWeaponData;   //�������Ă��镐��i�������畐��̏��𓾂�j

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

    public void AddWeaponData(WeaponData weaponData)
    {
        chooseWeaponData.Add(weaponData);
    }
}
