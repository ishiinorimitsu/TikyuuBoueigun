using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image energySlider;     //�G�l���M�[�Q�[�W������

    private float maxEnergyCount;    //�ő�G�l���M�[�ʂ�����

    [SerializeField]
    private Image bulletSlider;     //�c��e���Q�[�W������

    [SerializeField]
    private Text lastBullet;    //�c���Ă���e��������B

    [SerializeField]
    private Text maxBullet;    //�ő�e��������B(Text)

    private float maxBulletCount;   //�ő�e��������(float)

    [SerializeField]
    private Image selectedWeapon;  //�I�΂�Ă��镐��̉摜������

    [SerializeField]
    private Text selectedWeaponText;  //�I�΂�Ă��镐��̃e�L�X�g������


    [SerializeField]
    private Image HPSlider;    //HP�Q�[�W������


    [SerializeField]
    private Text lastHP;    //�c���Ă���HP������B

    [SerializeField]
    private Text maxHP;    //�ő�HP������B(Text)

    private float maxHPCount;    //�ő�HP������(float)


    //----------------------------------------�G�l���M�[�Q�[�W�̏���------------------------------------------------------//
    /// <summary>
    /// �G�l���M�[�Q�[�W�̏���
    /// </summary>
    /// <param name="maxEnergy"></param>
    public void SetEnergySliderValue(float maxEnergy)
    {
        this.maxEnergyCount = maxEnergy;

        energySlider.fillAmount = maxEnergy;     //�X���C�_�[�̒���maxValue��maxEnergy�ƈꏏ�ɂ���B

        UpdateDisplayEnergy(maxEnergy);    //�܂��ŏ���value�̒l��maxEnergy�Ɠ����ł����B
    }

    public void UpdateDisplayEnergy(float currentEnergy)
    {
        //Debug.Log(currentEnergy);

        energySlider.fillAmount = currentEnergy/maxEnergyCount;  //currentEnergy�܂�1.0�b�����ē������i�ŏ��̈����̒l��maxEnergy�ł����B�j

        energySlider.DOFillAmount(currentEnergy / maxEnergyCount, 1.0f);
    }


    //------------------------------------------����̒e�����̏���---------------------------------------------------------//

    /// <summary>
    /// �X���C�_�[�̃Q�[�W�Ɋւ��鏈��
    /// </summary>
    /// <param name="maxBullet"></param>
    public void SetWeaponSliderValue(int maxBullet,int currentBullet)     //�܂�����
    {
        this.maxBulletCount = maxBullet;

        bulletSlider.fillAmount = maxBullet;     //�X���C�_�[�̒���maxValue��maxBullet�ƈꏏ�ɂ���B

        UpdateDisplayBullet(currentBullet);    //�܂��ŏ���value�̒l��maxEnergy�Ɠ����ł����B

        this.maxBullet.text = maxBullet.ToString();    //UI�̍ő�e���𑕔����Ă��镐��̍ő�e���ɂ���B
    }

    /// <summary>
    /// �X���C�_�[�ɕύX���������ꍇ
    /// </summary>
    /// <param name="currentBulletCount"></param>
    public void UpdateDisplayBullet(int currentBulletCount)�@�@�@//�ω����������Ƃ��̏���
    {
        bulletSlider.fillAmount�@=�@currentBulletCount/maxBulletCount;  //currentBulletCount�܂�value�𓮂����i�ŏ��̈����̒l��maxBulletCount�ł����B�j

        lastBullet.text = currentBulletCount.ToString();   //���̋����𔽉f������B�����̍X�V
    }

    //------------------------------------------���̕���̖��O�Ɖ摜�Ɋւ��鏈��-------------------------------------------------//
    public void SetSelectedWeapon()
    {
        selectedWeapon.sprite = GameData.instance.equipWeaponData.weaponSprite;    //���ݑI�΂�Ă��镐��̉摜������

        selectedWeaponText.text = GameData.instance.equipWeaponData.weaponName;    //���ݑI�΂�Ă��镐��̖��O������
    }

    //------------------------------------------------------HP�Q�[�W�̏���--------------------------------------------------------//
    public void SetHpSliderValue(int maxHp)
    {
        this.maxHPCount = maxHp;

        HPSlider.fillAmount = maxHp;     //�X���C�_�[�̒���maxValue��maxHp�ƈꏏ�ɂ���B

        UpdateDisplayHp(maxHp);    //�܂��ŏ���value�̒l��maxHp�Ɠ����ł����B

        maxHP.text = maxHp.ToString();    //UI�̍ő�HP��\������B
    }

    public void UpdateDisplayHp(int currentHp)
    {
        HPSlider.fillAmount = currentHp/maxHPCount; //currentHp�܂�1.0�b�����ē������i�ŏ��̈����̒l��maxHp�ł����B�j

        lastHP.text = currentHp.ToString();   //����HP�𔽉f������B�����̍X�V
    }
}
