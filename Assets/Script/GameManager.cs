using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour   //実際の開発現場では、GamaManagrにだけStartクラスを作るイメージ
{
    [SerializeField]
    private WeaponGenerator weaponGenerator;

    void Start()
    {
        //weaponGenerator.AddWeaponData();
    }
}
