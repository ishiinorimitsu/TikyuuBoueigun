using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Slider energySlider;     //エネルギーゲージを入れる

    [SerializeField]
    private Slider bulletSlider;     //残り弾数ゲージを入れる

    [SerializeField]
    private Text lastBullet;    //残っている弾数を入れる。

    [SerializeField]
    private Text maxBullet;    //最大弾数を入れる。

    [SerializeField]
    private Image selectedWeapon;  //選ばれている武器の画像を入れる

    [SerializeField]
    private Text selectedWeaponText;  //選ばれている武器のテキストを入れる


    //----------------------------------------エネルギーゲージの処理------------------------------------------------------//
    public void SetEnergySliderValue(float maxEnergy)
    {
        energySlider.maxValue = maxEnergy;     //スライダーの中のmaxValueをmaxEnergyと一緒にする。

        UpdateDisplayEnergy(maxEnergy);    //まず最初はvalueの値はmaxEnergyと同じでいい。
    }

    public void UpdateDisplayEnergy(float currentEnergy)
    {
        //Debug.Log(currentEnergy);

        energySlider.DOValue(currentEnergy, 1.0f);  //currentEnergyまで1.0秒かけて動かす（最初の引数の値はmaxEnergyでいい。）
    }


    //------------------------------------------武器の弾数等の処理---------------------------------------------------------//

    /// <summary>
    /// スライダーのゲージに関する処理
    /// </summary>
    /// <param name="maxBullet"></param>
    public void SetWeaponSliderValue(int maxBullet,int currentBullet)     //まず準備
    {
        bulletSlider.maxValue = maxBullet;     //スライダーの中のmaxValueをmaxBulletと一緒にする。

        UpdateDisplayBullet(currentBullet);    //まず最初はvalueの値はmaxEnergyと同じでいい。

        this.maxBullet.text = maxBullet.ToString();    //UIの最大弾数を装備している武器の最大弾数にする。
    }

    /// <summary>
    /// スライダーに変更があった場合
    /// </summary>
    /// <param name="currentBulletCount"></param>
    public void UpdateDisplayBullet(int currentBulletCount)　　　//変化があったときの処理
    {
        bulletSlider.value=currentBulletCount;  //currentBulletCountまでvalueを動かす（最初の引数の値はmaxBulletCountでいい。）

        lastBullet.text = currentBulletCount.ToString();   //今の球数を反映させる。数字の更新

        bulletSlider.value = currentBulletCount;    //弾数のゲージを更新する
    }

    //------------------------------------------今の武器の名前と画像に関する処理-------------------------------------------------//
    public void SetSelectedWeapon()
    {
        selectedWeapon.sprite = GameData.instance.equipWeaponData.weaponSprite;    //現在選ばれている武器の画像が入る

        selectedWeaponText.text = GameData.instance.equipWeaponData.weaponName;    //現在選ばれている武器の名前が入る
    }
}
