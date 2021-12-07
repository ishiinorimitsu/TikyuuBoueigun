using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour　　　//このスクリプトはシーンを超えても壊れないようなもの、例えばバトルステージに入る前に武器を選んで持っていく処理、あるいはバトルが終わって新しい武器を手に入れる処理などを書く。
{
    public static GameData instance;   //ここのスクリプトをどこからでも使えるようにする。

    public List<WeaponData> chooseWeaponData = new List<WeaponData>();    //選んだ武器を入れる

    public WeaponData equipWeaponData;   //装備している武器（ここから武器の情報を得る）

    public int currentEquipWeaponNo;   //現在装備している武器のNo,切り替えるときに使う。

    private List<float> currentBulletList = new List<float>();

    private float currentBullet;    //今の弾数を入れる

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
    /// 選んだ武器を次のシーンに持っていくために一回このスクリプトに持ってくる
    /// </summary>
    public void AddWeaponData(WeaponData weaponData)
    {
        chooseWeaponData.Add(weaponData);

        equipWeaponData = chooseWeaponData[0];
    }

    public void ChangeWeapon()
    {
        currentEquipWeaponNo++;  //現在の武器のNoを１ずつ増やす。

        currentEquipWeaponNo = currentEquipWeaponNo % chooseWeaponData.Count;　　//それをリストの最大値で割る(この場合0か1になる)

        equipWeaponData = chooseWeaponData[currentEquipWeaponNo];　　//そのNoをリスト内のものと照合する。

        Debug.Log(equipWeaponData.maxBullet);
    }
}
