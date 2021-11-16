using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponManager : MonoBehaviour
{
    [SerializeField]
    private Button weapon1;

    [SerializeField]
    private Button weapon2;

    [SerializeField]
    private WeaponData weaponData1;    //今選ばれている武器１の情報を入れる（選ぶたびに上書きする。出撃するときにGameDataのWeaponDataListに入れる。）

    [SerializeField]
    private WeaponData weaponData2;    //今選ばれている武器２の情報を入れる（選ぶたびに上書きする。出撃するときにGameDataのWeaponDataListに入れる。）

    [SerializeField]
    private WeaponData selectWeaponData;  //保留中のデータ(決定を押す前に、選ぶたびに情報が更新されていく)

    [SerializeField]
    private ChooseWeaponWindow weaponInfo;    //表示したりする用に作っておく

    public enum WeaponSlotType{
        slot1,
        slot2
    }

    public WeaponSlotType weaponSlotType;  //今はどっちのボタンから来たか


    private void Start()
    {
        weapon1.onClick.AddListener(ChooseWeapon);


    }

    private void ChooseWeapon()
    {
        //武器を選ぶWindowを開く（スイッチを入れて、見えなくしておいたWindowを見せる。）
        //その中から武器を選択し、決定ボタンを押すとweapon1のところのTextに選んだ武器の名前が入る。
        //weapon1から入った場合はWeaponSlotTypeがslot1になる。
    }

    private void SubmitWeapon()
    {
        //「決定ボタン」を押したとき、それがslot1だったその情報が「武器１」に入る
        //「決定ボタン」を押したとき、それがslot2だったその情報が「武器2」に入る
    }
}