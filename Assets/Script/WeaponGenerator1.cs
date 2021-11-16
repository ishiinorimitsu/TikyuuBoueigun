using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator1 : MonoBehaviour
{
    [SerializeField]
    private List<WeaponData> weaponDataList = new List<WeaponData>();      //����̃f�[�^���X�g�B�����ɃX�N���v�^���I�u�W�F�N�g�̂��̂�����B


    //-----------------------------���̏��������Ȃ�厖�I������SO���̂��̂������Ă��Ă���B------------------------//
    private void CreateHaveWeaponDataList()   //���̃��\�b�h�ŃL�����̃f�[�^�����̃X�N���v�g���Ɏ�荞��ł���B
    {
        for(int i = 0; i < DataBaseManager.instance.weaponDataSO.weaponDataList.Count; i++)
        {
            weaponDataList.Add(DataBaseManager.instance.weaponDataSO.weaponDataList[i]);
        }
    }
}
