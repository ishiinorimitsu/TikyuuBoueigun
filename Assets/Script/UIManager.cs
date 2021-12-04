using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Slider energySlider;     //�G�l���M�[�Q�[�W������

    [SerializeField]
    private Slider bulletSlider;     //�c��e���Q�[�W������

    [SerializeField]
    private Text lastBullet;    //�c���Ă���e��������B

    [SerializeField]
    private Text maxBullet;    //�ő�e��������B



    //----------------------------------------�G�l���M�[�Q�[�W�̏���------------------------------------------------------//
    public void SetEnergySliderValue(float maxEnergy)
    {
        energySlider.maxValue = maxEnergy;     //�X���C�_�[�̒���maxValue��maxEnergy�ƈꏏ�ɂ���B

        UpdateDisplayEnergy(maxEnergy);    //�܂��ŏ���value�̒l��maxEnergy�Ɠ����ł����B
    }

    public void UpdateDisplayEnergy(float currentEnergy)
    {
        Debug.Log(currentEnergy);

        energySlider.DOValue(currentEnergy, 1.0f);  //currentEnergy�܂�1.0�b�����ē������i�ŏ��̈����̒l��maxEnergy�ł����B�j
    }


    //------------------------------------------����̒e�����̏���---------------------------------------------------------//

    /// <summary>
    /// �X���C�_�[�̃Q�[�W�Ɋւ��鏈��
    /// </summary>
    /// <param name="maxBulletCount"></param>
    public void SetWeaponSliderValue(float maxBulletCount)     //�܂�����
    {
        bulletSlider.maxValue = maxBulletCount;     //�X���C�_�[�̒���maxValue��maxBulletCount�ƈꏏ�ɂ���B

        UpdateDisplayBullet(maxBulletCount);    //�܂��ŏ���value�̒l��maxEnergy�Ɠ����ł����B

        maxBullet.text = maxBulletCount.ToString();    //UI�̍ő�e���𑕔����Ă��镐��̍ő�e���ɂ���B
    }

    /// <summary>
    /// �X���C�_�[�ɕύX���������ꍇ
    /// </summary>
    /// <param name="currentBulletCount"></param>
    public void UpdateDisplayBullet(float currentBulletCount)�@�@�@//�ω����������Ƃ��̏���
    {
        bulletSlider.DOValue(currentBulletCount, 1.0f);  //currentBulletCount�܂�1.0�b�����ē������i�ŏ��̈����̒l��maxBulletCount�ł����B�j

        lastBullet.text = currentBulletCount.ToString();   //���̋����𔽉f������

        bulletSlider.value = currentBulletCount;    //�e���̃Q�[�W���X�V����
    }
}
