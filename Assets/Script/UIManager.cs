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

    [SerializeField]
    private Image selectedWeapon;  //�I�΂�Ă��镐��̉摜������

    [SerializeField]
    private Text selectedWeaponText;  //�I�΂�Ă��镐��̃e�L�X�g������


    //----------------------------------------�G�l���M�[�Q�[�W�̏���------------------------------------------------------//
    public void SetEnergySliderValue(float maxEnergy)
    {
        energySlider.maxValue = maxEnergy;     //�X���C�_�[�̒���maxValue��maxEnergy�ƈꏏ�ɂ���B

        UpdateDisplayEnergy(maxEnergy);    //�܂��ŏ���value�̒l��maxEnergy�Ɠ����ł����B
    }

    public void UpdateDisplayEnergy(float currentEnergy)
    {
        //Debug.Log(currentEnergy);

        energySlider.DOValue(currentEnergy, 1.0f);  //currentEnergy�܂�1.0�b�����ē������i�ŏ��̈����̒l��maxEnergy�ł����B�j
    }


    //------------------------------------------����̒e�����̏���---------------------------------------------------------//

    /// <summary>
    /// �X���C�_�[�̃Q�[�W�Ɋւ��鏈��
    /// </summary>
    /// <param name="maxBullet"></param>
    public void SetWeaponSliderValue(int maxBullet,int currentBullet)     //�܂�����
    {
        bulletSlider.maxValue = maxBullet;     //�X���C�_�[�̒���maxValue��maxBullet�ƈꏏ�ɂ���B

        UpdateDisplayBullet(currentBullet);    //�܂��ŏ���value�̒l��maxEnergy�Ɠ����ł����B

        this.maxBullet.text = maxBullet.ToString();    //UI�̍ő�e���𑕔����Ă��镐��̍ő�e���ɂ���B
    }

    /// <summary>
    /// �X���C�_�[�ɕύX���������ꍇ
    /// </summary>
    /// <param name="currentBulletCount"></param>
    public void UpdateDisplayBullet(int currentBulletCount)�@�@�@//�ω����������Ƃ��̏���
    {
        bulletSlider.value=currentBulletCount;  //currentBulletCount�܂�value�𓮂����i�ŏ��̈����̒l��maxBulletCount�ł����B�j

        lastBullet.text = currentBulletCount.ToString();   //���̋����𔽉f������B�����̍X�V

        bulletSlider.value = currentBulletCount;    //�e���̃Q�[�W���X�V����
    }

    //------------------------------------------���̕���̖��O�Ɖ摜�Ɋւ��鏈��-------------------------------------------------//
    public void SetSelectedWeapon()
    {
        selectedWeapon.sprite = GameData.instance.equipWeaponData.weaponSprite;    //���ݑI�΂�Ă��镐��̉摜������

        selectedWeaponText.text = GameData.instance.equipWeaponData.weaponName;    //���ݑI�΂�Ă��镐��̖��O������
    }
}
