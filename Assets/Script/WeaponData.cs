using UnityEngine;

[System.Serializable]
public class WeaponData 
{
    public string weaponName;   //武器の名前
    public int weaponNo;   //武器の番号
    public Sprite charaSprite;   //武器の見た目

    public int weaponAttackPower;   //武器の一発撃った時のダメージ（ダメージ）
    public int attackRange;   //武器の射程（ｍ）
    public int maxAttackCount;   //武器に一度のリロードで何発まで打てるか（発）
    public BulletController bulletPrefab;   //撃つ弾
    public int fuelEnergy;  //消耗するエネルギー（％）
    public float bulletSpeed;  //弾の速度

    [Multiline]
    public string discription;   //武器の説明
}
