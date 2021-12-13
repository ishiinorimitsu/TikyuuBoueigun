using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image energySlider;     //エネルギーゲージを入れる

    private float maxEnergyCount;    //最大エネルギー量を入れる

    [SerializeField]
    private Image bulletSlider;     //残り弾数ゲージを入れる

    [SerializeField]
    private Text lastBullet;    //残っている弾数を入れる。

    [SerializeField]
    private Text maxBullet;    //最大弾数を入れる。(Text)

    private float maxBulletCount;   //最大弾数を入れる(float)

    [SerializeField]
    private Image selectedWeapon;  //選ばれている武器の画像を入れる

    [SerializeField]
    private Text selectedWeaponText;  //選ばれている武器のテキストを入れる


    [SerializeField]
    private Image HPSlider;    //HPゲージを入れる


    [SerializeField]
    private Text lastHP;    //残っているHPを入れる。

    [SerializeField]
    private Text maxHP;    //最大HPを入れる。(Text)

    private float maxHPCount;    //最大HPを入れる(float)


    //----------------------------------------エネルギーゲージの処理------------------------------------------------------//
    /// <summary>
    /// エネルギーゲージの処理
    /// </summary>
    /// <param name="maxEnergy"></param>
    public void SetEnergySliderValue(float maxEnergy)
    {
        this.maxEnergyCount = maxEnergy;

        energySlider.fillAmount = maxEnergy;     //スライダーの中のmaxValueをmaxEnergyと一緒にする。

        UpdateDisplayEnergy(maxEnergy);    //まず最初はvalueの値はmaxEnergyと同じでいい。
    }

    public void UpdateDisplayEnergy(float currentEnergy)
    {
        //Debug.Log(currentEnergy);

        energySlider.fillAmount = currentEnergy/maxEnergyCount;  //currentEnergyまで1.0秒かけて動かす（最初の引数の値はmaxEnergyでいい。）

        energySlider.DOFillAmount(currentEnergy / maxEnergyCount, 1.0f);
    }


    //------------------------------------------武器の弾数等の処理---------------------------------------------------------//

    /// <summary>
    /// スライダーのゲージに関する処理
    /// </summary>
    /// <param name="maxBullet"></param>
    public void SetWeaponSliderValue(int maxBullet,int currentBullet)     //まず準備
    {
        this.maxBulletCount = maxBullet;

        bulletSlider.fillAmount = maxBullet;     //スライダーの中のmaxValueをmaxBulletと一緒にする。

        UpdateDisplayBullet(currentBullet);    //まず最初はvalueの値はmaxEnergyと同じでいい。

        this.maxBullet.text = maxBullet.ToString();    //UIの最大弾数を装備している武器の最大弾数にする。
    }

    /// <summary>
    /// スライダーに変更があった場合
    /// </summary>
    /// <param name="currentBulletCount"></param>
    public void UpdateDisplayBullet(int currentBulletCount)　　　//変化があったときの処理
    {
        bulletSlider.fillAmount　=　currentBulletCount/maxBulletCount;  //currentBulletCountまでvalueを動かす（最初の引数の値はmaxBulletCountでいい。）

        lastBullet.text = currentBulletCount.ToString();   //今の球数を反映させる。数字の更新
    }

    //------------------------------------------今の武器の名前と画像に関する処理-------------------------------------------------//
    public void SetSelectedWeapon()
    {
        selectedWeapon.sprite = GameData.instance.equipWeaponData.weaponSprite;    //現在選ばれている武器の画像が入る

        selectedWeaponText.text = GameData.instance.equipWeaponData.weaponName;    //現在選ばれている武器の名前が入る
    }

    //------------------------------------------------------HPゲージの処理--------------------------------------------------------//
    public void SetHpSliderValue(int maxHp)
    {
        this.maxHPCount = maxHp;

        HPSlider.fillAmount = maxHp;     //スライダーの中のmaxValueをmaxHpと一緒にする。

        UpdateDisplayHp(maxHp);    //まず最初はvalueの値はmaxHpと同じでいい。

        maxHP.text = maxHp.ToString();    //UIの最大HPを表示する。
    }

    public void UpdateDisplayHp(int currentHp)
    {
        HPSlider.fillAmount = currentHp/maxHPCount; //currentHpまで1.0秒かけて動かす（最初の引数の値はmaxHpでいい。）

        lastHP.text = currentHp.ToString();   //今のHPを反映させる。数字の更新
    }
}
