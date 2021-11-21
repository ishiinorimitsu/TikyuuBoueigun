using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Slider energySlider;     //�G�l���M�[�Q�[�W������

    public void SetSliderValue(float maxEnergy)
    {
        energySlider.maxValue = maxEnergy;     //�X���C�_�[�̒���maxValue��maxEnergy�ƈꏏ�ɂ���B

        UpdateDisplayEnergy(maxEnergy);    //�܂��ŏ���value�̒l��maxEnergy�Ɠ����ł����B
    }

    public void UpdateDisplayEnergy(float currentEnergy)
    {
        energySlider.DOValue(currentEnergy, 1.0f);  //currentEnergy�܂�1.0�b�����ē������i�ŏ��̈����̒l��maxEnergy�ł����B�j
    }
}
