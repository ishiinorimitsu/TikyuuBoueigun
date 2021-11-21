using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Slider energySlider;     //エネルギーゲージを入れる

    public void SetSliderValue(float maxEnergy)
    {
        energySlider.maxValue = maxEnergy;     //スライダーの中のmaxValueをmaxEnergyと一緒にする。

        UpdateDisplayEnergy(maxEnergy);    //まず最初はvalueの値はmaxEnergyと同じでいい。
    }

    public void UpdateDisplayEnergy(float currentEnergy)
    {
        energySlider.DOValue(currentEnergy, 1.0f);  //currentEnergyまで1.0秒かけて動かす（最初の引数の値はmaxEnergyでいい。）
    }
}
