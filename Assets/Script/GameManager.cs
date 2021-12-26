using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour   //実際の開発現場では、GamaManagrにだけStartクラスを作るイメージ
{
    [SerializeField]
    private WeaponGenerator weaponGenerator;

    [SerializeField]
    private CharaController charaController;   //スタートメソッドすべてをここに書くために持ってくる

    public enum Wave   //ゲームは大体のステージでwave3までにする
    {
        wave1,     
        wave2,
        wave3
    }

    public Wave currentWave;     //現在のWaveを入れる。

    void Start()
    {
        //weaponGenerator.AddWeaponData();
        charaController.GameStart();
    }
}
