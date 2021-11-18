using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour  //��ȏ�̃V�[���ɑ��݂����Ƃ��́A�ォ�痈�������j�󂳂��B
{
    public static DataBaseManager instance;�@�@  //���̃X�N���v�g���̏����ǂ�����ł��g����悤�ɂ���@�@

    [SerializeField]
    public WeaponDataSO weaponDataSO;   //������WeaponDataSO�̏�������B

    private void Awake()
    {
        if (instance == null)                    //�����l���Ȃ��̂ōŏ��͂�����ł���B������ŏ��̈��ڂ͂��ꂪ�쓮����B
        {
            instance = this;                    //instance�̒��g�ɂ�DataBaseManager������B
            DontDestroyOnLoad(gameObject);      //�V�[�����ς���Ă�instance�̒��g�͍폜���ꂽ�肵�Ȃ��Ƃ�������
        }
        else
        {
            Destroy(gameObject);                //2��ڂ���͂����炪�s���A�Q��ڈȍ~�ɑ�����ꂽDataBaseManager�͍폜�����B
        }
    }
}
