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



    //----------------------------------------エネルギーゲージの処理------------------------------------------------------//
    public void SetEnergySliderValue(float maxEnergy)
    {
        energySlider.maxValue = maxEnergy;     //スライダーの中のmaxValueをmaxEnergyと一緒にする。

        UpdateDisplayEnergy(maxEnergy);    //まず最初はvalueの値はmaxEnergyと同じでいい。
    }

    public void UpdateDisplayEnergy(float currentEnergy)
    {
        Debug.Log(currentEnergy);

        energySlider.DOValue(currentEnergy, 1.0f);  //currentEnergyまで1.0秒かけて動かす（最初の引数の値はmaxEnergyでいい。）
    }


    //------------------------------------------武器の弾数等の処理---------------------------------------------------------//

    /// <summary>
    /// スライダーのゲージに関する処理
    /// </summary>
    /// <param name="maxBulletCount"></param>
    public void SetWeaponSliderValue(float maxBulletCount)     //まず準備
    {
        bulletSlider.maxValue = maxBulletCount;     //スライダーの中のmaxValueをmaxBulletCountと一緒にする。

        UpdateDisplayBullet(maxBulletCount);    //まず最初はvalueの値はmaxEnergyと同じでいい。

        maxBullet.text = maxBulletCount.ToString();    //UIの最大弾数を装備している武器の最大弾数にする。
    }

    /// <summary>
    /// スライダーに変更があった場合
    /// </summary>
    /// <param name="currentBulletCount"></param>
    public void UpdateDisplayBullet(float currentBulletCount)　　　//変化があったときの処理
    {
        bulletSlider.DOValue(currentBulletCount, 1.0f);  //currentBulletCountまで1.0秒かけて動かす（最初の引数の値はmaxBulletCountでいい。）

        lastBullet.text = currentBulletCount.ToString();   //今の球数を反映させる

        bulletSlider.value = currentBulletCount;    //弾数のゲージを更新する
    }
}
