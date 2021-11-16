using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator1 : MonoBehaviour
{
    [SerializeField]
    private List<WeaponData> weaponDataList = new List<WeaponData>();      //武器のデータリスト。ここにスクリプタルオブジェクトのものを入れる。


    //-----------------------------この処理もかなり大事！ここでSO内のものを持ってきている。------------------------//
    private void CreateHaveWeaponDataList()   //このメソッドでキャラのデータをこのスクリプト内に取り込んでいる。
    {
        for(int i = 0; i < DataBaseManager.instance.weaponDataSO.weaponDataList.Count; i++)
        {
            weaponDataList.Add(DataBaseManager.instance.weaponDataSO.weaponDataList[i]);
        }
    }
}
