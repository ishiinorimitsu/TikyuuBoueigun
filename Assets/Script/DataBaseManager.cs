using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour  //二つ以上のシーンに存在したときは、後から来た方が破壊される。
{
    public static DataBaseManager instance;　　  //このスクリプト内の情報をどこからでも使えるようにする　　

    [SerializeField]
    public WeaponDataSO weaponDataSO;   //ここにWeaponDataSOの情報を入れる。

    private void Awake()
    {
        if (instance == null)                    //初期値がないので最初はいつも空である。だから最初の一回目はこれが作動する。
        {
            instance = this;                    //instanceの中身にはDataBaseManagerを入れる。
            DontDestroyOnLoad(gameObject);      //シーンが変わってもinstanceの中身は削除されたりしないということ
        }
        else
        {
            Destroy(gameObject);                //2回目からはこちらが行われ、２回目以降に代入されたDataBaseManagerは削除される。
        }
    }
}
